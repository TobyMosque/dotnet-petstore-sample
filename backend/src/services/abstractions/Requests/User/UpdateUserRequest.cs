using Mediator;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.User
{
    public class UpdateUserRequest : IRequest<UserDto>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int UserStatus { get; set; }
    }
}
