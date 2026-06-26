using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Services.Abstractions.Requests.Pet;

namespace PetStore.Services.Commands.Pet
{
    public class DeletePetHandler : IRequestHandler<DeletePetRequest, bool>
    {
        private readonly PetStoreContext _db;

        public DeletePetHandler(PetStoreContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeletePetRequest request, CancellationToken cancellationToken)
        {
            var pet = await _db.Pets
                .Include(p => p.PhotoUrls)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (pet == null) return false;

            _db.PetPhotos.RemoveRange(pet.PhotoUrls);
            _db.Pets.Remove(pet);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
