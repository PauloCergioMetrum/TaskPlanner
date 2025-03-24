using DocumentFormat.OpenXml.Office2010.ExcelAc;
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
        [HttpPost("GetAllTeamAllocation")]
        public IActionResult GetTeamAllocation(TeamAlocationTableDTO teamAlocatTable)
        {
            try
            {
                var result = _teamAllocationBusiness.GetTeamAllocation(
                    teamAlocatTable.businessUnit,
                    teamAlocatTable.startDate,
                    teamAlocatTable.endDate,
                    teamAlocatTable.FunctionIds,
                    teamAlocatTable.project,
                    teamAlocatTable.TechLeadName
                );

                if (result == null || (result.TeamAllocations.Count == 0 && result.TeamAllocationGraphics.Count == 0))
                {
                    result = _teamAllocationBusiness.GetTeamAllocation(
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    );

                    if (result == null || (result.TeamAllocations.Count == 0 && result.TeamAllocationGraphics.Count == 0))
                    {
                        return NoContent();
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
