using MediatR;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.User
{
    public class GetUserByNameRequest : IRequest<UserDto>
    {
        public string Username { get; set; }
    }
}
