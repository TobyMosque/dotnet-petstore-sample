using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PetStore.Domain;
using PetStore.Domain.Entities;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Store;
using RT.Comb;

namespace PetStore.Services.Commands.Store
{
    public class PlaceOrderHandler : IRequestHandler<PlaceOrderRequest, OrderDto>
    {
        private readonly PetStoreContext _db;
        private readonly ICombProvider _comb;

        public PlaceOrderHandler(PetStoreContext db, ICombProvider comb)
        {
            _db = db;
            _comb = comb;
        }

        public async Task<OrderDto> Handle(PlaceOrderRequest request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = _comb.Create(),
                PetId = request.PetId,
                Quantity = request.Quantity,
                ShipDate = request.ShipDate,
                Status = request.Status,
                Complete = request.Complete
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync(cancellationToken);

            return new OrderDto
            {
                Id = order.Id,
                PetId = order.PetId,
                Quantity = order.Quantity,
                ShipDate = order.ShipDate,
                Status = order.Status,
                Complete = order.Complete
            };
        }
    }
}
