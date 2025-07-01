using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Switchcraft.Server.Controllers.Dtos;
using Switchcraft.Server.Controllers.Services;

namespace Switchcraft.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _service;
        private readonly ILogger<ApplicationsController> _logger;

        public ApplicationsController(IApplicationService service, ILogger<ApplicationsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApplicationResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(ApplicationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateApplicationAsync(request);

            return CreatedAtAction(
                actionName: "Get",
                controllerName: "Applications",
                routeValues: new { id = result.Id },
                value: result
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApplicationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(int id, ApplicationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateApplicationAsync(id, request);

            if (result != null)
            {
                return Ok(result);
            }
            
            return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApplicationResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _service.GetApplicationsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApplicationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetApplicationAsync(id);

            if (result != null)
            {
                return Ok(result);
            }
            
            return NotFound();
        }

        [HttpGet("{id}/Switches")]
        [ProducesResponseType(typeof(IEnumerable<SwitchResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSwitchesAsync(int id)
        {
            var result = await _service.GetSwitchesAsync(id);

            return Ok(result);
        }
    }
}
