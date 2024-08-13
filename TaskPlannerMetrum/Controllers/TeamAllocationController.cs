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

        [HttpGet("GetAllTeamAllocation")]
        public IActionResult GetTeamAllocation(string businessUnit = null, DateTime? startDate = null, DateTime? endDate = null, string function = null, string project = null)
        {
            try
            {
          
                var result = _teamAllocationBusiness.GetTeamAllocation(businessUnit, startDate, endDate, function, project);

                if (result == null || (result.TeamAllocations.Count == 0 && result.TeamAllocationGraphics.Count == 0))
                {
                    return NoContent(); 
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