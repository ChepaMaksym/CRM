using CRM.DTO.Plot;
using CRM.Models;
using CRM.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlotController(IPlotService plotService, ILogger<PlotController> logger) : BaseController<Plot>(logger)
    {
        private readonly IPlotService _plotService = plotService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var plots = await _plotService.GetAllAsync();
                return Ok(plots);
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
                var plot = await _plotService.GetByIdAsync(id);
                if (plot == null)
                    return NotFound();

                return Ok(plot);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPlotDTO plot)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int id = await _plotService.AddAsync(plot);
                return CreatedAtAction(nameof(GetById), id, plot);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePlotDTO plot)
        {
            if (id != plot.PlotId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _plotService.UpdateAsync(plot);
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
                await _plotService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
