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
            var pets = await _db.Pets.ToListAsync(cancellationToken);
            return pets.GroupBy(p => p.Status)
                .ToDictionary(g => g.Key ?? "unknown", g => g.Count());
        }
    }
}
