using System;
using System.Collections.Generic;
using MediatR;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.Pet
{
    public class UpdatePetRequest : IRequest<PetDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public Guid? CategoryId { get; set; }
        public IList<string> PhotoUrls { get; set; }
        public IList<Guid> TagIds { get; set; }
    }
}
