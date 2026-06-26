using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStore.Services.Abstractions.Services;

namespace PetStore.Api.Controllers
{
    [ApiController]
    [Route("data")]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateData(CancellationToken ct)
            => Ok(await _dataService.GenerateAsync(ct));

        [HttpGet("export")]
        public async Task<IActionResult> ExportData(CancellationToken ct)
        {
            var bytes = await _dataService.ExportAsync(ct);
            return File(bytes, "application/zip", "petstore-export.zip");
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedData(CancellationToken ct)
            => Ok(await _dataService.SeedAsync(ct));

        [HttpPost("import")]
        public async Task<IActionResult> ImportData(IFormFile file, CancellationToken ct)
            => Ok(await _dataService.ImportAsync(file.OpenReadStream(), ct));
    }
}
