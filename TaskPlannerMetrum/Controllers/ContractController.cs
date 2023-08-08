using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.Contracts;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ContractController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;

        // Declaration of the service used
        private IContractBusiness _contractBusiness;

        public ContractController(ILogger<ProjectsController> logger, IContractBusiness contractBusiness)
        {
            _logger = logger;
            _contractBusiness = contractBusiness;

        }

        [HttpGet("GetAllContracts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllContracts()
        {
            try
            {
                return Ok(_contractBusiness.GetAllContracts());

            }catch (Exception ex) 
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }

        [HttpPut("UpdateContract")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdateContract(Contracts contract)
        {
            try
            {
                return Ok(_contractBusiness.UpdateContract(contract));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetFiscGest")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetFiscGest()
        {
            try
            {
                return Ok(_contractBusiness.GetFiscGest());

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }

        [HttpGet("GetSeller")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetSeller()
        {
            try
            {
                return Ok(_contractBusiness.GetSeller());

            }catch( Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }

        [HttpPost("CreateProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Create(Model.DTO.ContractDTO newcontract)
        {
            try
            {
                return Ok(_contractBusiness.Create(newcontract));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }


        [HttpGet("GetAllWorkSpace")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllWorkSpace()
        {
            try
            {
                return Ok(_contractBusiness.GetAllWorkSpace());

            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetContractForProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetContractForProject()
        {
            try
            {
                return Ok(_contractBusiness.GetContractForProject());

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("DesableProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult DesableProject(int id)
        {
            try
            {
                return Ok(_contractBusiness.DesableProject(id));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetAllProjectsContracts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult DesablePrGetAllProjectsContractsoject()
        {
            try
            {
                return Ok(_contractBusiness.GetAllProjectsContracts());


            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);


            }
        }

        [HttpGet("ContractDashboard")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult ContractDashboard(string year, float value)
        {
            try
            {
                return Ok(_contractBusiness.ContractDashboard(year, value));

            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UpdateObservation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]

        public IActionResult UpdateObservation(int ID, string Observation)
        {
            try
            {
                return Ok(_contractBusiness.UpdateObservation(ID, Observation));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }

        [HttpGet("ContractDasboardDate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]

        public IActionResult ContractDasboardDate(DateTime data)
        {
            try
            {
                return Ok(_contractBusiness.ContractDasboardDate(data));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }



      

       





    }
}
