using CRM.DTO.Garden;
using CRM.Models;
using CRM.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GardenController(IGardenService gardenService, ILogger<GardenController> logger) : BaseController<Garden>(logger)
    {
        private readonly IGardenService _gardenService = gardenService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var gardens = await _gardenService.GetAllAsync();
                return Ok(gardens);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var garden = await _gardenService.GetByIdAsync(id);
                if (garden == null)
                    return NotFound();

                return Ok(garden);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddGardenDTO garden)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int id = await _gardenService.AddAsync(garden);
                return CreatedAtAction(nameof(GetById), new { id }, new Garden { GardenId = id, Size = garden.Size });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGardenDTO garden)
        {
            if (id != garden.GardenId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _gardenService.UpdateAsync(garden);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _gardenService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}