using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PetStore.Services.Abstractions.Services
{
    public interface IDataService
    {
        Task<string> GenerateAsync(CancellationToken ct);
        Task<byte[]> ExportAsync(CancellationToken ct);
        Task<string> ImportAsync(Stream archive, CancellationToken ct);
        Task<string> SeedAsync(CancellationToken ct);
    }
}
