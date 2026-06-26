using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Domain.Entities;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Pet;
using RT.Comb;

namespace PetStore.Services.Commands.Pet
{
    public class AddPetHandler : IRequestHandler<AddPetRequest, PetDto>
    {
        private readonly PetStoreContext _db;
        private readonly ICombProvider _comb;

        public AddPetHandler(PetStoreContext db, ICombProvider comb)
        {
            _db = db;
            _comb = comb;
        }

        public async Task<PetDto> Handle(AddPetRequest request, CancellationToken cancellationToken)
        {
            var tags = request.TagIds != null && request.TagIds.Count > 0
                ? await _db.Tags.Where(t => request.TagIds.Contains(t.Id)).ToListAsync(cancellationToken)
                : new List<Domain.Entities.Tag>();

            var pet = new Domain.Entities.Pet
            {
                Id = _comb.Create(),
                Name = request.Name,
                Status = request.Status,
                CategoryId = request.CategoryId,
                Tags = tags,
                PhotoUrls = request.PhotoUrls?.Select(url => new PetPhoto
                {
                    Id = _comb.Create(),
                    Url = url
                }).ToList() ?? new List<PetPhoto>()
            };

            _db.Pets.Add(pet);
            await _db.SaveChangesAsync(cancellationToken);

            await _db.Entry(pet).Reference(p => p.Category).LoadAsync(cancellationToken);

            return new PetDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Status = pet.Status,
                Category = pet.Category == null ? null : new CategoryDto { Id = pet.Category.Id, Name = pet.Category.Name },
                PhotoUrls = pet.PhotoUrls.Select(ph => ph.Url).ToList(),
                Tags = pet.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList()
            };
        }
    }
}
