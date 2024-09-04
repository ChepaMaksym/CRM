using CRM.DTO.Harvest;
using CRM.Models;
using CRM.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarvestController(IHarvestService harvestService, ILogger<HarvestController> logger) : BaseController<Harvest>(logger)
    {
        private readonly IHarvestService _harvestService = harvestService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var harvest = await _harvestService.GetAllAsync();
                return Ok(harvest);
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
                var harvest = await _harvestService.GetByIdAsync(id);
                if (harvest == null)
                    return NotFound();

                return Ok(harvest);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddHarvestDTO harvest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int id = await _harvestService.AddAsync(harvest);
                return CreatedAtAction(nameof(GetById), new { id }, new Harvest(id, harvest));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateHarvestDTO harvest)
        {
            if (id != harvest.HarvestId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _harvestService.UpdateAsync(harvest);
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
                await _harvestService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
