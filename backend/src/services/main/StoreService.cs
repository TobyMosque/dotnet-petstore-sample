using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Store;
using PetStore.Services.Abstractions.Services;

namespace PetStore.Services
{
    public class StoreService : IStoreService
    {
        private readonly IMediator _mediator;

        public StoreService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IDictionary<string, int>> GetInventoryAsync(CancellationToken ct)
            => _mediator.Send(new GetInventoryRequest(), ct).AsTask();

        public Task<OrderDto> PlaceOrderAsync(PlaceOrderRequest request, CancellationToken ct)
            => _mediator.Send(request, ct).AsTask();

        public Task<OrderDto> GetOrderByIdAsync(Guid id, CancellationToken ct)
            => _mediator.Send(new GetOrderByIdRequest { Id = id }, ct).AsTask();

        public Task<bool> DeleteOrderAsync(Guid id, CancellationToken ct)
            => _mediator.Send(new DeleteOrderRequest { Id = id }, ct).AsTask();
    }
}
