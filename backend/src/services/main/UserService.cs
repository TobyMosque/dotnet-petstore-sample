using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.User;
using PetStore.Services.Abstractions.Services;

namespace PetStore.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IList<UserDto>> GetAllUsersAsync(int skip, int take, CancellationToken ct)
            => _mediator.Send(new GetAllUsersRequest { Skip = skip, Take = take }, ct).AsTask();

        public Task<UserDto> GetUserByNameAsync(string username, CancellationToken ct)
            => _mediator.Send(new GetUserByNameRequest { Username = username }, ct).AsTask();

        public Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken ct)
            => _mediator.Send(request, ct).AsTask();

        public Task<UserDto> UpdateUserAsync(UpdateUserRequest request, CancellationToken ct)
            => _mediator.Send(request, ct).AsTask();

        public Task<bool> DeleteUserAsync(string username, CancellationToken ct)
            => _mediator.Send(new DeleteUserRequest { Username = username }, ct).AsTask();

        public Task<string> LoginAsync(string username, string password, CancellationToken ct)
            => _mediator.Send(new LoginUserRequest { Username = username, Password = password }, ct).AsTask();

        public Task LogoutAsync(CancellationToken ct)
            => Task.CompletedTask;
    }
}
