using CRM.DTO.Plant;
using CRM.Models;
using CRM.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController(IPlantService plantService, ILogger<PlantController> logger) : BaseController<Plant>(logger)
    {
        private readonly IPlantService _plantService = plantService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var plants = await _plantService.GetAllAsync();
                return Ok(plants);
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
                var plant = await _plantService.GetByIdAsync(id);
                if (plant == null)
                    return NotFound();

                return Ok(plant);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPlantDTO plant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int id = await _plantService.AddAsync(plant);
                return CreatedAtAction(nameof(GetById), id, plant);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePlantDTO plant)
        {
            if (id != plant.PlantId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _plantService.UpdateAsync(plant);
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
                await _plantService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
