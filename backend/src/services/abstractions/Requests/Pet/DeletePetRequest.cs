using System;
using Mediator;

namespace PetStore.Services.Abstractions.Requests.Pet
{
    public class DeletePetRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
