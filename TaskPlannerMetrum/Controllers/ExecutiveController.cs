using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.Executive;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize]
    public class ExecutiveController : ControllerBase
    {
        private readonly ILogger<ExecutiveController> _logger;
        private readonly IExecutiveBussines _executiveBussines;
        private readonly IExecutiveRepository _executiveRepository;

        public ExecutiveController(ILogger<ExecutiveController> logger, IExecutiveBussines executiveBussines, IExecutiveRepository executiveRepository)
        {
            _logger = logger;
            _executiveBussines = executiveBussines;
            _executiveRepository = executiveRepository;
        }

        [HttpGet("GetPvExecutiveTable")]
        public IActionResult GetPvExecutiveTable([FromQuery] string inspectorIDs, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var result = _executiveRepository.GetPvExecutiveTable(inspectorIDs, startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
             
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
