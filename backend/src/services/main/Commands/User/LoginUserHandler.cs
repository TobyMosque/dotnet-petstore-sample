using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Services.Abstractions.Requests.User;

namespace PetStore.Services.Commands.User
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, string>
    {
        private readonly PetStoreContext _db;

        public LoginUserHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async Task<string> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(
                u => u.Username == request.Username && u.Password == request.Password,
                cancellationToken);

            if (user == null) return null;

            return $"logged-in-{user.Username}";
        }
    }
}
