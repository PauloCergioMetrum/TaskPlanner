using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Business.Implementations;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Controllers
{


    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "1,4,DEPCNT")]

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


        [HttpGet("GetAllMilestonesItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllMilestonesItem(int ContractID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetAllMilestonesItem(ContractID));

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

        [HttpDelete("DeleteMilestones")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteMilestones(string ID)
        {
            try
            {


                return Ok(_projectManagementBusiness.DeleteMilestones(ID));

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

        [HttpDelete("DeleteTypeOf")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteTypeOfCost(string ID)
        {
            try
            {
                return Ok(_projectManagementBusiness.DeleteTypeOfCost(ID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTypeOfCost")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetTypeOfCost(int ContractID)
        {

            try
            {

                return Ok(_projectManagementBusiness.GetTypeOfCost(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CreateOrUpdatePredictedCost")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned)
        {
            try
            {

                return Ok(_projectManagementBusiness.CreateOrUpdatePredictedCost(pmCostPlanned));



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeletePredictedCost")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeletePredictedCost(string ID)
        {
            try
            {
                return Ok(_projectManagementBusiness.DeletePredictedCost(ID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetPmCostPlanned")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetPmCostPlanned(int ContractID)
        {

            try
            {

                return Ok(_projectManagementBusiness.GetPmCostPlanned(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("CreatePMCostMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreatePMCostMade([FromBody] PmCostMade pmCostMade)
        {
            try
            {
                bool created = _projectManagementBusiness.CreateOrUpdateCostMade(pmCostMade);
                if (created)
                {
                    return Ok("Cadastrado.");
                }
                else
                {
                    return BadRequest("Erro ao Cadastrar.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCostMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteCostMade(string ID)
        {
            try
            {
                return Ok(_projectManagementBusiness.DeleteCostMade(ID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetPmCostMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetPmCostMade(string Pm_Cost_PlannedID)
        {

            try
            {

                return Ok(_projectManagementBusiness.GetPmCostMade(Pm_Cost_PlannedID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // MOBILIZAÇÃO

        [HttpGet("GetMobilization")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMobilization(int contractID)
        {

            try
            {

                return Ok(_projectManagementBusiness.GetMobilization(contractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMobilizationMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMobilizationMade(string mobilizationPlannedID)
        {
            try
            {
                return Ok(_projectManagementBusiness.GetMobilizationMade(mobilizationPlannedID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("CreateMobilizationPlanned")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateMobilizationPlanned(PM_Mobilization_Planned mobilizationPlanned)
        {

            try
            {

                return Ok(_projectManagementBusiness.CreateMobilizationPlanned(mobilizationPlanned));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CreateMobilizationMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateMobilizationMade(PM_Mobilization_Made mobilizationMade)
        {

            try
            {

                return Ok(_projectManagementBusiness.CreateMobilizationMade(mobilizationMade));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteMobilizationPlanned")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteMobilization(string ID)
        {
            try
            {
                return Ok(_projectManagementBusiness.DeleteMobilization(ID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteMobilizationMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteMobilizationMade(string ID)
        {
            try
            {
                return Ok(_projectManagementBusiness.DeleteMobilizationMade(ID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // SERVIÇO TERCERIZADO 


        [HttpGet("GetAllOutsourcedServiceNames")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllOutsourcedServiceNames()
        {
            try
            {
                return Ok(_projectManagementBusiness.GetAllOutsourcedServiceNames());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("CreateoutsourcedServicePlanned")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateoutsourcedServicePlanned(PM_OutsourcedServices_Planned createoutsourcedServicePlanned)
        {

            try
            {

                return Ok(_projectManagementBusiness.CreateoutsourcedServicePlanned(createoutsourcedServicePlanned));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("CreateoutsourcedServiceMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateoutsourcedServiceMade(PM_OutsourcedServices_Made createoutsourcedServiceMade)
        {

            try
            {

                return Ok(_projectManagementBusiness.CreateoutsourcedServiceMade(createoutsourcedServiceMade));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
