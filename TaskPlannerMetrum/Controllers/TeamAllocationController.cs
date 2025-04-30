using Microsoft.AspNetCore.Mvc;
using System;
using TaskPlannerMetrum.Business;

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
        public IActionResult GetExecutorHoursTable(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] string functionName = null,
            [FromQuery] string departmentName = null)
        {
            var result = _teamAllocationBusiness.GetExecutorHoursTable(startDate, endDate, functionName, departmentName);
            return result == null || result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("GetTeamAllocationReport")]
        public IActionResult GetTeamAllocationReport(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] string functionName = null,
            [FromQuery] string departmentName = null)
        {
            var result = _teamAllocationBusiness.GetTeamAllocationReport(startDate, endDate, functionName, departmentName);
            return result == null || result.ExecutorHoursTable == null || result.ExecutorHoursTable.Count == 0
                ? NoContent()
                : Ok(result);
        }
    }
}
