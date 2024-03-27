using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Business.Implementations;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Controllers
{


    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ProjectManagementController : ControllerBase
    {

        private readonly IProjectManagementBusiness _projectManagementBusiness;

        public ProjectManagementController(IProjectManagementBusiness projectManagementBusiness)
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

        [HttpGet("GetPositionsByGrup")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetPositionsByGrup()
        {
            try
            {
                return Ok(_projectManagementBusiness.GetPositionsByGrup());

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateAcquisitionsPlanned")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateAcquisitionsPlanned(AcquisitionsDTO acquisitions)
        {
            try
            {
                return Ok(_projectManagementBusiness.CreateAcquisitionsPlanned(acquisitions));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAcquisitions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAcquisitions(int ContractID)
        {
            try
            {
                return Ok(_projectManagementBusiness.GetAcquisitions(ContractID));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteAcquisition")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteAcquisition(string ID)
        {
            try
            {
                return Ok(_projectManagementBusiness.DeleteAcquisition(ID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateAcquisitionsMadeItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateAcquisitionsMade(AcquisitionMadeDTO acquisitionMade)
        {
            try
            {
                return Ok(_projectManagementBusiness.CreateAcquisitionsMade(acquisitionMade));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAcquisitionsMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAcquisitionsMade(string AquisitionPlannedID)
        {
            try
            {
                return Ok(_projectManagementBusiness.GetAcquisitionsMade(AquisitionPlannedID));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteAcquisitionsMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteAcquisitionMade(string ID, string AquisitionPlannedID)
        {
            try
            {
                return Ok(_projectManagementBusiness.DeleteAcquisitionMade(ID, AquisitionPlannedID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateTypeOfCost")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult CreateTypeOfCost(PmTypeCost pmTypeCost)
        {
            try
            {

                return Ok(_projectManagementBusiness.CreateTypeOfCost(pmTypeCost));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
