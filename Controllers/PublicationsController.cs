using FacebookAPI.Models;
using FacebookAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FacebookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicationsController : ControllerBase
    {
        private readonly PublicationsService _publicationsService;

        public PublicationsController(PublicationsService publicationsService) =>
            _publicationsService = publicationsService;

        [HttpGet]
        public async Task<List<Publications>> Get() =>
            await _publicationsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Publications>> Get(string id)
        {
            var publication = await _publicationsService.GetAsync(id);

            if (publication is null)
            {
                return NotFound();
            }

            return publication;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Publications newPublication)
        {
            newPublication.Id = ObjectId.GenerateNewId().ToString();

            await _publicationsService.CreateAsync(newPublication);

            return CreatedAtAction(nameof(Get), new { id = newPublication.Id }, newPublication);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Publications updatedPublication)
        {
            var publication = await _publicationsService.GetAsync(id);

            if (publication is null)
            {
                return NotFound();
            }

            updatedPublication.Id = publication.Id;

            await _publicationsService.UpdateAsync(id, updatedPublication);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var publication = await _publicationsService.GetAsync(id);

            if (publication is null)
            {
                return NotFound();
            }

            await _publicationsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
