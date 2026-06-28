using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Store;

namespace PetStore.Services.Commands.Store
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersRequest, IList<OrderDto>>
    {
        private readonly PetStoreContext _db;

        public GetAllOrdersHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async ValueTask<IList<OrderDto>> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
        {
            var orders = await _db.Orders.ToListAsync(cancellationToken);
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                PetId = o.PetId,
                Quantity = o.Quantity,
                ShipDate = o.ShipDate,
                Status = o.Status,
                Complete = o.Complete
            }).ToList();
        }
    }
}
