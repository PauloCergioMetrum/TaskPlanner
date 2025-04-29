using Microsoft.AspNetCore.Mvc;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TeamAllocationController : ControllerBase
    {
        private readonly ITeamAllocationBusiness _teamAllocationBusiness;

        public TeamAllocationController(ITeamAllocationBusiness teamAllocationBusiness)
        {
            _teamAllocationBusiness = teamAllocationBusiness;
        }

        [HttpGet("GetExecutorHoursTable")]
        public IActionResult GetExecutorHoursTable([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = _teamAllocationBusiness.GetExecutorHoursTable(startDate, endDate);
            return result == null || result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("GetTeamAllocationReport")]
        public IActionResult GetTeamAllocationReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string functionName = null)
        {
            var result = _teamAllocationBusiness.GetTeamAllocationReport(startDate, endDate, functionName);
            return result == null || result.ExecutorHoursTable.Count == 0 ? NoContent() : Ok(result);
        }
    }
}
