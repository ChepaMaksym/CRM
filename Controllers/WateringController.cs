using CRM.DTO.Watering;
using CRM.Models;
using CRM.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WateringController(IWateringService wateringService, ILogger<WateringController> logger) : BaseController<Watering>(logger)
    {
        private readonly IWateringService _wateringService = wateringService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var watering = await _wateringService.GetAllAsync();
                return Ok(watering);
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
                var watering = await _wateringService.GetByIdAsync(id);
                if (watering == null)
                    return NotFound();

                return Ok(watering);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWateringDTO watering)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int id = await _wateringService.AddAsync(watering);
                return CreatedAtAction(nameof(GetById), id, watering);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWateringDTO watering)
        {
            if (id != watering.WateringId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _wateringService.UpdateAsync(watering);
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
                await _wateringService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
