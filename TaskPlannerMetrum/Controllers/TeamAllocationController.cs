using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            try
            {
                var result = _teamAllocationBusiness.GetExecutorHoursTable(startDate, endDate);

                if (result == null || result.Count == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao buscar dados de alocação de equipe.", error = ex.Message });
            }
        }
    }
}
