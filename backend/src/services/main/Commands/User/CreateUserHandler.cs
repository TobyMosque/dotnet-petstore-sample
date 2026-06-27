using System.Threading;
using System.Threading.Tasks;
using Mediator;
using PetStore.Domain;
using PetStore.Domain.Entities;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.User;
using RT.Comb;

namespace PetStore.Services.Commands.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, UserDto>
    {
        private readonly PetStoreContext _db;
        private readonly ICombProvider _comb;

        public CreateUserHandler(PetStoreContext db, ICombProvider comb)
        {
            _db = db;
            _comb = comb;
        }

        public async ValueTask<UserDto> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User
            {
                Id = _comb.Create(),
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Phone = request.Phone,
                UserStatus = request.UserStatus
            };

            _db.Users.Add(user);
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
