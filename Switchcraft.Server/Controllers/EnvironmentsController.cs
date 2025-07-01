using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Switchcraft.Server.Controllers.Dtos;
using Switchcraft.Server.Controllers.Services;

namespace Switchcraft.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IEnvironmentService _service;
        private readonly ILogger<EnvironmentsController> _logger;

        public EnvironmentsController(IEnvironmentService service, ILogger<EnvironmentsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EnvironmentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(EnvironmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateEnvironmentAsync(request);

            return CreatedAtAction(
                actionName: "Get",
                controllerName: "Environments",
                routeValues: new { id = result.Id },
                value: result
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EnvironmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(int id, EnvironmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateEnvironmentAsync(id, request);

            if (result != null)
            {
                return Ok(result);
            }
            
            return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EnvironmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _service.GetEnvironmentsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnvironmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetEnvironmentAsync(id);

            if (result != null)
            {
                return Ok(result);
            }
            
            return NotFound();
        }
    }
}
