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
    public class UpdatePetHandler : IRequestHandler<UpdatePetRequest, PetDto>
    {
        private readonly PetStoreContext _db;
        private readonly ICombProvider _comb;

        public UpdatePetHandler(PetStoreContext db, ICombProvider comb)
        {
            _db = db;
            _comb = comb;
        }

        public async Task<PetDto> Handle(UpdatePetRequest request, CancellationToken cancellationToken)
        {
            var pet = await _db.Pets
                .Include(p => p.PhotoUrls)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (pet == null) return null;

            pet.Name = request.Name;
            pet.Status = request.Status;
            pet.CategoryId = request.CategoryId;

            _db.PetPhotos.RemoveRange(pet.PhotoUrls);
            pet.PhotoUrls = request.PhotoUrls?.Select(url => new PetPhoto
            {
                Id = _comb.Create(),
                PetId = pet.Id,
                Url = url
            }).ToList() ?? new List<PetPhoto>();

            var tags = request.TagIds != null && request.TagIds.Count > 0
                ? await _db.Tags.Where(t => request.TagIds.Contains(t.Id)).ToListAsync(cancellationToken)
                : new List<Domain.Entities.Tag>();
            pet.Tags = tags;

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
