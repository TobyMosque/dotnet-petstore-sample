using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Services.Abstractions.Requests.Store;

namespace PetStore.Services.Commands.Store
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderRequest, bool>
    {
        private readonly PetStoreContext _db;

        public DeleteOrderHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
            if (order == null) return false;

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
