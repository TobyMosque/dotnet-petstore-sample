using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.User;

namespace PetStore.Services.Commands.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UserDto>
    {
        private readonly PetStoreContext _db;

        public UpdateUserHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async Task<UserDto> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);
            if (user == null) return null;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = request.Password;
            user.Phone = request.Phone;
            user.UserStatus = request.UserStatus;

            await _db.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                UserStatus = user.UserStatus
            };
        }
    }
}
