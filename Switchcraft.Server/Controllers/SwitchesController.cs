using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Switchcraft.Server.Controllers.Dtos;
using Switchcraft.Server.Controllers.Services;

namespace Switchcraft.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchesController : ControllerBase
    {
        private readonly ISwitchService _service;
        private readonly ILogger<SwitchesController> _logger;

        public SwitchesController(ISwitchService service, ILogger<SwitchesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SwitchResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(SwitchRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateSwitchAsync(request);

            return CreatedAtAction(
                actionName: "Get",
                controllerName: "Switches",
                routeValues: new { id = result.Id },
                value: result
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SwitchResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(int id, SwitchRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateSwitchAsync(id, request);

            if (result != null)
            {
                return Ok(result);
            }
            
            return NotFound(); 
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SwitchResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _service.GetSwitchesAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SwitchResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetSwitchAsync(id);

            if (result != null)
            {
                return Ok(result);
            }
            
            return NotFound();
        }
    }
}
