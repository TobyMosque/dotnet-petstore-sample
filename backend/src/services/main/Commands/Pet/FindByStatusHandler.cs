using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Pet;

namespace PetStore.Services.Commands.Pet
{
    public class FindByStatusHandler : IRequestHandler<FindByStatusRequest, IList<PetDto>>
    {
        private readonly PetStoreContext _db;

        public FindByStatusHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async ValueTask<IList<PetDto>> Handle(FindByStatusRequest request, CancellationToken cancellationToken)
        {
            var pets = await _db.Pets
                .Where(p => p.Status == request.Status)
                .Include(p => p.PhotoUrls)
                .Include(p => p.Tags)
                .Include(p => p.Category)
                .ToListAsync(cancellationToken);

            return pets.Select(p => new PetDto
            {
                Id = p.Id,
                Name = p.Name,
                Status = p.Status,
                PhotoUrls = p.PhotoUrls.Select(ph => ph.Url).ToList(),
                Tags = p.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList(),
                Category = p.Category == null ? null : new CategoryDto { Id = p.Category.Id, Name = p.Category.Name }
            }).ToList();
        }
    }
}
