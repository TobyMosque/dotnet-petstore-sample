using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Pet;
using PetStore.Services.Abstractions.Services;

namespace PetStore.Api.Controllers
{
    [ApiController]
    [Route("pet")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet("{petId}")]
        public async Task<ActionResult<PetDto>> GetPetById(Guid petId, CancellationToken ct)
        {
            var pet = await _petService.GetPetByIdAsync(petId, ct);
            if (pet == null) return NotFound();
            return Ok(pet);
        }

        [HttpGet("findByStatus")]
        public async Task<ActionResult<IList<PetDto>>> FindByStatus([FromQuery] string status, CancellationToken ct)
            => Ok(await _petService.FindByStatusAsync(status, ct));

        [HttpGet("findByTags")]
        public async Task<ActionResult<IList<PetDto>>> FindByTags([FromQuery] IList<string> tags, CancellationToken ct)
            => Ok(await _petService.FindByTagsAsync(tags, ct));

        [HttpPost]
        public async Task<ActionResult<PetDto>> AddPet([FromBody] AddPetRequest request, CancellationToken ct)
            => Ok(await _petService.AddPetAsync(request, ct));

        [HttpPut]
        public async Task<ActionResult<PetDto>> UpdatePet([FromBody] UpdatePetRequest request, CancellationToken ct)
        {
            var pet = await _petService.UpdatePetAsync(request, ct);
            if (pet == null) return NotFound();
            return Ok(pet);
        }

        [HttpDelete("{petId}")]
        public async Task<IActionResult> DeletePet(Guid petId, CancellationToken ct)
        {
            var result = await _petService.DeletePetAsync(petId, ct);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
