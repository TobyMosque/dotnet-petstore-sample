using System.Threading;
using System.Threading.Tasks;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.User;

namespace PetStore.Services.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByNameAsync(string username, CancellationToken ct);
        Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken ct);
        Task<UserDto> UpdateUserAsync(UpdateUserRequest request, CancellationToken ct);
        Task<bool> DeleteUserAsync(string username, CancellationToken ct);
        Task<string> LoginAsync(string username, string password, CancellationToken ct);
        Task LogoutAsync(CancellationToken ct);
    }
}
