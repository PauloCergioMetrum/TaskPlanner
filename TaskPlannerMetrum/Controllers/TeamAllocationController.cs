using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "4,1")]
    public class TeamAllocationController : ControllerBase
    {
        private readonly ITeamAllocationBusiness _teamAllocationBusiness;

        public TeamAllocationController(ITeamAllocationBusiness teamAllocationBusiness)
        {
            _teamAllocationBusiness = teamAllocationBusiness;
        }

        [HttpGet("GetTeamAllocationReport")]
        [ProducesResponseType(typeof(TeamAllocationResultDTO), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetTeamAllocationReport(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] string functionName = null,
            [FromQuery] string departmentName = null)
        {
            try
            {
                var result = _teamAllocationBusiness.GetTeamAllocationReport(startDate, endDate, functionName, departmentName);

                if (result == null || result.TaskExecutionMetrics == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Erro ao buscar relatório de alocação de equipe.",
                    error = ex.Message
                });
            }
        }
    }
}
