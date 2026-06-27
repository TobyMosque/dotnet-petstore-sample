using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Store;

namespace PetStore.Services.Abstractions.Services
{
    public interface IStoreService
    {
        Task<IList<OrderDto>> GetAllOrdersAsync(CancellationToken ct);
        Task<IDictionary<string, int>> GetInventoryAsync(CancellationToken ct);
        Task<OrderDto> PlaceOrderAsync(PlaceOrderRequest request, CancellationToken ct);
        Task<OrderDto> GetOrderByIdAsync(Guid id, CancellationToken ct);
        Task<bool> DeleteOrderAsync(Guid id, CancellationToken ct);
    }
}
