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
            return await _db.Users
                .Where(u => u.Username == request.Username)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Phone = u.Phone,
                    UserStatus = u.UserStatus
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
