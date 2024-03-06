using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProjectTechnicialLeaderController : ControllerBase
    {
        private readonly IProjectTechnicialLeaderBussines _projectTechnicialLeaderBussines;

        public ProjectTechnicialLeaderController(IProjectTechnicialLeaderBussines projectTechnicialLeaderBussines)
        {
            _projectTechnicialLeaderBussines = projectTechnicialLeaderBussines;
        }

        [HttpGet("Getprojects")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetProjectsByTechLeader(int TechLeaderID)
        {
            try
            {
                var projects = _projectTechnicialLeaderBussines.GetProjectsByTechLeader(TechLeaderID);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }
    }
}
