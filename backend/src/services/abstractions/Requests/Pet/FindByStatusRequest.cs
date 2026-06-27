using System.Collections.Generic;
using Mediator;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.Pet
{
    public class FindByStatusRequest : IRequest<IList<PetDto>>
    {
        public string Status { get; set; }
    }
}
