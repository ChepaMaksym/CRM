using CRM.DTO.SoilType;
using CRM.Models;
using CRM.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoilTypeController(ISoilTypeService soilTypeService, ILogger<SoilTypeController> logger) : BaseController<SoilType>(logger)
    {
        private readonly ISoilTypeService _soilTypeService = soilTypeService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var soilTypes = await _soilTypeService.GetAllAsync();
                return Ok(soilTypes);
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
                var soilType = await _soilTypeService.GetByIdAsync(id);
                if (soilType == null)
                    return NotFound();

                return Ok(soilType);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddSoilTypeDTO soilType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int id = await _soilTypeService.AddAsync(soilType);
                return CreatedAtAction(nameof(GetById), id, soilType);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSoilTypeDTO soilType)
        {
            if (id != soilType.SoilTypeId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _soilTypeService.UpdateAsync(soilType);
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
                await _soilTypeService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
