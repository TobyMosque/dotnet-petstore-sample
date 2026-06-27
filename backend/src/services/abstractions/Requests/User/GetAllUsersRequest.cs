using System.Collections.Generic;
using Mediator;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.User
{
    public class GetAllUsersRequest : IRequest<IList<UserDto>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
    }
}
