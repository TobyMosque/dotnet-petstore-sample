using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.User;

namespace PetStore.Services.Commands.User
{
    public class GetUserByNameHandler : IRequestHandler<GetUserByNameRequest, UserDto>
    {
        private readonly PetStoreContext _db;

        public GetUserByNameHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async ValueTask<UserDto> Handle(GetUserByNameRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Where(u => u.Username == request.Username)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null) return null;

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
