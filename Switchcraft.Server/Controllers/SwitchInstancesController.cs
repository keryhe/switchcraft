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
        [ProducesResponseType(typeof(ApplicationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int environmentId, int applicationId)
        {
            var result = await _service.GetSwitchInstancesAsync(environmentId, applicationId);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IEnumerable<SwitchResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(int id, SwitchInstanceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateSwitchInstanceAsync(id, request);

            if (result != null)
            {
                return Ok(result);
            }
            
            return NotFound();
        }
    }
}
