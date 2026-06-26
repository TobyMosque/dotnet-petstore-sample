using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Pet;
using PetStore.Services.Abstractions.Services;

namespace PetStore.Services
{
    public class PetService : IPetService
    {
        private readonly IMediator _mediator;

        public PetService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<PetDto> GetPetByIdAsync(Guid id, CancellationToken ct)
            => _mediator.Send(new GetPetByIdRequest { Id = id }, ct).AsTask();

        public Task<IList<PetDto>> FindByStatusAsync(string status, CancellationToken ct)
            => _mediator.Send(new FindByStatusRequest { Status = status }, ct).AsTask();

        public Task<IList<PetDto>> FindByTagsAsync(IList<string> tags, CancellationToken ct)
            => _mediator.Send(new FindByTagsRequest { Tags = tags }, ct).AsTask();

        public Task<PetDto> AddPetAsync(AddPetRequest request, CancellationToken ct)
            => _mediator.Send(request, ct).AsTask();

        public Task<PetDto> UpdatePetAsync(UpdatePetRequest request, CancellationToken ct)
            => _mediator.Send(request, ct).AsTask();

        public Task<bool> DeletePetAsync(Guid id, CancellationToken ct)
            => _mediator.Send(new DeletePetRequest { Id = id }, ct).AsTask();
    }
}
