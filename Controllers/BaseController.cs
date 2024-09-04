using CRM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase where TEntity : AgroBase
    {
        protected readonly ILogger _logger;

        protected BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleError(Exception ex)
        {
            _logger.LogError(ex, "An error occurred");
            IActionResult result = StatusCode(500, "Internal server error");
            switch (ex)
            {
                case InvalidOperationException invalid:
                    result = StatusCode(400, invalid.Message);
                    break;
                case ArgumentNullException argument:
                    result = StatusCode(400, argument.Message);
                    break;
            }
            return result;
        }
    }
}
