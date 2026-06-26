using Mediator;

namespace PetStore.Services.Abstractions.Requests.User
{
    public class DeleteUserRequest : IRequest<bool>
    {
        public string Username { get; set; }
    }
}
