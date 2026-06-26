using System.Collections.Generic;
using MediatR;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.Pet
{
    public class FindByTagsRequest : IRequest<IList<PetDto>>
    {
        public IList<string> Tags { get; set; }
    }
}
