using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Services.Abstractions.Requests.Store;

namespace PetStore.Services.Commands.Store
{
    public class GetInventoryHandler : IRequestHandler<GetInventoryRequest, IDictionary<string, int>>
    {
        private readonly PetStoreContext _db;

        public GetInventoryHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async ValueTask<IDictionary<string, int>> Handle(GetInventoryRequest request, CancellationToken cancellationToken)
        {
            var groups = await _db.Pets
                .GroupBy(p => p.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync(cancellationToken);

            return groups.ToDictionary(
                g => g.Status ?? "unknown",
                g => g.Count
            );
        }
    }
}
