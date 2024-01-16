using Microsoft.AspNetCore.Mvc;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Business.Implementations;

namespace TaskPlannerMetrum.Controllers
{


    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ProjectManagementController:ControllerBase
    {

        private readonly IProjectManagementBusiness _projectManagementBusiness;

        public ProjectManagementController (IProjectManagementBusiness projectManagementBusiness)
        {
            _projectManagementBusiness = projectManagementBusiness;
        }

        [HttpPut("UpdateForCast")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdateForCast(Model.DTO.ProjectManagementDTO forcast)
        {

            try
            {
                
                return Ok(_projectManagementBusiness.UpdateForecast(forcast));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetOrderInfo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetOrderInfo(int id)
        {

            try
            {

                return Ok(_projectManagementBusiness.GetOrderInformation(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
