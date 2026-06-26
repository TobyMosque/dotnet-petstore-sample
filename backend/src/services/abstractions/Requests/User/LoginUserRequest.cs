using MediatR;

namespace PetStore.Services.Abstractions.Requests.User
{
    public class LoginUserRequest : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
