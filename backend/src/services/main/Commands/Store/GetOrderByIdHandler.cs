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
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdRequest, OrderDto>
    {
        private readonly PetStoreContext _db;

        public GetOrderByIdHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async ValueTask<OrderDto> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
        {
            return await _db.Orders
                .Where(o => o.Id == request.Id)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    PetId = o.PetId,
                    Quantity = o.Quantity,
                    ShipDate = o.ShipDate,
                    Status = o.Status,
                    Complete = o.Complete
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
