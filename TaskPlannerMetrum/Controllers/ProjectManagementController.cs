using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpDelete("DeleteMilestones")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteMilestones(string ID, int MilestonesID)
        {
            try
            {


                return Ok(_projectManagementBusiness.DeleteMilestones(ID, MilestonesID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("MilesTonesDelete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult MilesTonesDelete(int ID)
        {
            try
            {


                return Ok(_projectManagementBusiness.MilesTonesDelete(ID));

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



        [HttpDelete("DeleteTypeOf")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteTypeOfCost(int ID)
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

        [HttpGet("GetOutsourcedServicesCombined")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetOutsourcedServicesCombined(int ContractID)
        {
            try
            {
                return Ok(_projectManagementBusiness.GetOutsourcedServicesCombined(ContractID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOutsourcedServicesCombinedMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetOutsourcedServicesCombinedMade(string ID_OutsourcedServices_Planned)
        {
            try
            {
                return Ok(_projectManagementBusiness.GetOutsourcedServicesCombinedMade(ID_OutsourcedServices_Planned));
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



        [HttpDelete("DeleteOutsourcedServicesPlanned")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteOutsourcedServicesPlanned(string ID)
        {

            try
            {

                return Ok(_projectManagementBusiness.DeleteOutsourcedServicesPlanned(ID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteOutsourcedServicesMade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteOutsourcedServicesMade(string ID)
        {

            try
            {

                return Ok(_projectManagementBusiness.DeleteOutsourcedServicesMade(ID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetpmTypeHH")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetpmTypeHH()
        {
            try
            {
                return Ok(_projectManagementBusiness.GetpmTypeHH());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPMFunctionHHs")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetPMFunctionHHs()
        {
            try
            {
                return Ok(_projectManagementBusiness.GetPMFunctionHHs());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateHH")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateHH(PM_Man_Hours createHH)
        {

            try
            {

                return Ok(_projectManagementBusiness.CreateHH(createHH));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteHH")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteHH(string ID)
        {

            try
            {

                return Ok(_projectManagementBusiness.DeleteHH(ID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetpmHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetpmHours(int ContractID)
        {

            try
            {

                return Ok(_projectManagementBusiness.GetpmHours(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("CreatScopeTraking")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreatScopeTraking(PM_Scope_Traking pMScopeTraking)
        {

            try
            {

                return Ok(_projectManagementBusiness.CreatScopeTraking(pMScopeTraking));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("UpdatepMScopeTraking")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdatepMScopeTraking(PM_Scope_Traking pMScopeTraking)
        {

            try
            {

                return Ok(_projectManagementBusiness.UpdatepMScopeTraking(pMScopeTraking));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeletepMScopeTraking")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeletepMScopeTraking(string ID)
        {

            try
            {

                return Ok(_projectManagementBusiness.DeletepMScopeTraking(ID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetScopeTrajing")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetScopeTrajing(int ContractID)
        {

            try
            {

                return Ok(_projectManagementBusiness.GetScopeTrajing(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("CreateScopeChange")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateScopeChange(PM_Scope_Change pM_Scope_Change)
        {

            try
            {

                return Ok(_projectManagementBusiness.CreateScopeChange(pM_Scope_Change));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteScopeChange")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteScopeChange(string ID)
        {

            try
            {

                return Ok(_projectManagementBusiness.DeleteScopeChange(ID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetScopeChanges")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetScopeChanges(int ContracID)
        {

            try
            {

                return Ok(_projectManagementBusiness.GetScopeChanges(ContracID));

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





        [HttpGet("GetAllFunctions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllFunctions()
        {
            try
            {

                return Ok(_projectManagementBusiness.GetAllFunctions());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("CreateFunctions_MilestoneType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateFunctions_MilestoneType(PM_Functions_MilestoneType dto)
        {
            try
            {

                return Ok(_projectManagementBusiness.CreateFunctions_MilestoneType(dto));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetAllFunctionsMilesstoneType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllFunctionsMilesstoneType(string MilesstoneTypeID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetAllFunctionsMilesstoneType(MilesstoneTypeID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteFunctionID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteFunctionID(string ID)
        {
            try
            {

                return Ok(_projectManagementBusiness.DeleteFunctionID(ID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetAllDisplacementServices")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllDisplacementServices()
        {
            try
            {

                return Ok(_projectManagementBusiness.GetAllDisplacementServices());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("CreatePM_MilestonesType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreatePM_MilestonesType(PM_MilestonesType dto)
        {
            try
            {

                return Ok(_projectManagementBusiness.CreatePM_MilestonesType(dto));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetAllPM_MilestonesType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllPM_MilestonesType(string MilestonesValueID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetAllPM_MilestonesType(MilestonesValueID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("DeletePM_MilestonesType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeletePM_MilestonesType(string ID)
        {
            try
            {

                return Ok(_projectManagementBusiness.DeletePM_MilestonesType(ID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetHHByID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetHHByID(string MilestonesValueID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetHHByID(MilestonesValueID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //[HttpGet("GetHHByIDTaskAsync")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(401)]
        //public IActionResult GetHHByIDTaskAsync(int ContractID)
        //{
        //    try
        //    {

        //        return Ok(_projectManagementBusiness.GetHHByIDTaskAsync(ContractID));

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpGet("GetAllMileStonesValue")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllMileStonesValue(int ContractID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetAllMileStonesValue(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetMilestoneStatusCalculation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMilestoneStatusCalculation(int ContractID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetMilestoneStatusCalculation(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("SetMilestoneFinalizedStatus")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SetMilestoneFinalizedStatus([FromBody] FinalizedStatusDto dto)
        {
            try
            {
                var result = _projectManagementBusiness.SetMilestoneFinalizedStatus(dto);

                if (result == "Não é possível finalizar: existem tarefas não concluídas.")
                    return BadRequest(new { error = result }); 

                return Ok(new { message = result }); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPut("UpdateInfoProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdateInfoProject([FromBody] ProjectInfoGeneralDTO infoGenral)
        {

            if (infoGenral == null)
            {
                return BadRequest("Payload is null");
            }
            try
            {

                return Ok(_projectManagementBusiness.UpdateInfoGeneral(infoGenral));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProjectInfoByContractId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetProjectInfoByContractId(int contractId)
        {




            return Ok(_projectManagementBusiness.GetProjectInfoByContractId(contractId));



        }






        [HttpGet("GetAllProjectCharter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult GetAllProjectCharter()
        {
            try
            {

                return Ok(_projectManagementBusiness.GetAllProjectCharter());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetAllUsersGercon")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult GetAllUsersGercon()
        {
            try
            {

                return Ok(_projectManagementBusiness.GetAllUsersGercon());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetAllBussinesUnit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult GetAllBussinesUnit(int ContractID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetAllBussinesUnit(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetMilestoneItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult GetMilestoneItem(int ContractID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetMilestoneItem(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetActivityPlanDetails")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult GetActivityPlanDetails(int ContractID)
        {
            try
            {

                return Ok(_projectManagementBusiness.GetActivityPlanDetails(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetOrderManagementInfo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult OrderManagementInfo(int ContractID)
        {
            try
            {

                return Ok(_projectManagementBusiness.OrderManagementInfo(ContractID));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetRightCardValues")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult RightCardValues(int ContractID)
        {
            try
            {

                return Ok(_projectManagementBusiness.RightCardValues(ContractID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("GetCombinedCharts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetCombinedCharts(int contractID)
        {
            try
            {
                return Ok(_projectManagementBusiness.GetCombinedCharts(contractID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("HhGraphicDetails")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult HhGraphicDetails(int contractID)
        {
            try
            {
                return Ok(_projectManagementBusiness.vMilestonesStatistics(contractID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("GetCombinedMilestones")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetCombinedMilestones(string milestoneId)
        {
            var combinedMilestones = await _projectManagementBusiness.GetCombinedMilestones(milestoneId);

            if (combinedMilestones == null || !combinedMilestones.Any())
            {
                return NoContent();
            }

            return Ok(combinedMilestones);
        }



        [HttpGet("GetCombinedMilestonesData")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult vCombinedMilestonesData(int contractID)
        {
            try
            {
                return Ok(_projectManagementBusiness.vCombinedMilestonesData(contractID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetCardsHHHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CardsHHHours(int contractID)
        {
            try
            {
                var result = _projectManagementBusiness.CardsHHHours(contractID);

                if (result == null)
                    return NoContent(); 

                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetHhCostChart")]
        public async Task<IActionResult> GetHhCostChart(int contractId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _projectManagementBusiness.GetHhCostChartAsync(contractId, startDate, endDate);
                if (result == null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter HH Cost Chart: {ex.Message}");
            }
        }



        [HttpGet("GetMilestoneFullReportByContract")]
        public async Task<IActionResult> GetMilestoneFullReportByContract(int contractId)
        {
            try
            {
                var result = await _projectManagementBusiness.GetMilestoneFullReportByContract(contractId);
                if (result == null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error ao buscar dados de GetMilestoneFullReportByContract: {ex.Message}");
            }
        }






    }

}





