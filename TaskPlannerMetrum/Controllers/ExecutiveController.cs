using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
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

        [HttpGet("GetAllExecutive")]
        public ActionResult<List<ExecutiveDto>> GetPvExecutiveTable([FromQuery] string inspectorIDs, [FromQuery] DateTime? StartDate, [FromQuery] DateTime? EndDate)
        {
            try
            {
                // Exemplo de validação simples
                if (StartDate.HasValue && EndDate.HasValue && StartDate > EndDate)
                {
                    return BadRequest("StartDate cannot be later than EndDate.");
                }

                var result = _executiveRepository.GetPvExecutiveTable(inspectorIDs, StartDate, EndDate);

                if (result == null || result.Count == 0)
                {
                    return NotFound("No records found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving PV executive table.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
