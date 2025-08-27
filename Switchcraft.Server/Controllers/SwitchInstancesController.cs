using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Switchcraft.Server.Controllers.Dtos;
using Switchcraft.Server.Controllers.Services;

namespace Switchcraft.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchInstancesController : ControllerBase
    {
        private readonly ISwitchInstanceService _service;
        private readonly ILogger<SwitchInstancesController> _logger;

        public SwitchInstancesController(ISwitchInstanceService service, ILogger<SwitchInstancesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SwitchInstanceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int environmentId, int applicationId)
        {
            var result = await _service.GetSwitchInstancesAsync(environmentId, applicationId);

            return Ok(result);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(SwitchInstanceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(string name)
        {
            var result = await _service.GetSwitchInstanceAsync(name);
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IEnumerable<SwitchInstanceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(int id, SwitchInstanceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateSwitchInstanceAsync(id, request);

            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
    }
}
