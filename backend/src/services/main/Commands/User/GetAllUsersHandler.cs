using System.Collections.Generic;
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
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, IList<UserDto>>
    {
        private readonly PetStoreContext _db;

        public GetAllUsersHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async ValueTask<IList<UserDto>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _db.Users
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken);

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.Phone,
                UserStatus = u.UserStatus
            }).ToList();
        }
    }
}
