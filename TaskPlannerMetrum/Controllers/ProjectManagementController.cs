using Microsoft.AspNetCore.Mvc;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Business.Implementations;
using TaskPlannerMetrum.Model.DTO;

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



        [HttpPost("CreateMilesTones")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateMilesTones(MilesTonesDTO milesTones)
        {
            try
            {

                return Ok(_projectManagementBusiness.CreateMilesTones(milesTones));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("UpdateMilesTones")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdateMilesTones(MilesTonesDTO milesTones)
        {
            try
            {

                return Ok(_projectManagementBusiness.UpdateMilesTones(milesTones));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        [HttpGet("GetMilestonesNames")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMilestonesNames(int contractID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetMilestonesNames(contractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
