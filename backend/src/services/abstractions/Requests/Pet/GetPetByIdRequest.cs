using System;
using MediatR;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.Pet
{
    public class GetPetByIdRequest : IRequest<PetDto>
    {
        public Guid Id { get; set; }
    }
}
