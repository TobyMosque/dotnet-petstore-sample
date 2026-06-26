using System;
using MediatR;

namespace PetStore.Services.Abstractions.Requests.Pet
{
    public class DeletePetRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
