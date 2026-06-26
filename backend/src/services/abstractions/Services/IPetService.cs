using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Pet;

namespace PetStore.Services.Abstractions.Services
{
    public interface IPetService
    {
        Task<PetDto> GetPetByIdAsync(Guid id, CancellationToken ct);
        Task<IList<PetDto>> FindByStatusAsync(string status, CancellationToken ct);
        Task<IList<PetDto>> FindByTagsAsync(IList<string> tags, CancellationToken ct);
        Task<PetDto> AddPetAsync(AddPetRequest request, CancellationToken ct);
        Task<PetDto> UpdatePetAsync(UpdatePetRequest request, CancellationToken ct);
        Task<bool> DeletePetAsync(Guid id, CancellationToken ct);
    }
}
