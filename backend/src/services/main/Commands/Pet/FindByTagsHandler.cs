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
    public class FindByTagsHandler : IRequestHandler<FindByTagsRequest, IList<PetDto>>
    {
        private readonly PetStoreContext _db;

        public FindByTagsHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async ValueTask<IList<PetDto>> Handle(FindByTagsRequest request, CancellationToken cancellationToken)
        {
            return await _db.Pets
                .Where(p => p.Tags.Any(t => request.Tags.Contains(t.Name)))
                .Select(p => new PetDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Status = p.Status,
                    PhotoUrls = p.PhotoUrls.Select(ph => ph.Url).ToList(),
                    Tags = p.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList(),
                    Category = p.Category == null ? null : new CategoryDto { Id = p.Category.Id, Name = p.Category.Name }
                })
                .ToListAsync(cancellationToken);
        }
    }
}
