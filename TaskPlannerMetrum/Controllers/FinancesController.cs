using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskPlannerMetrum.Business;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Collections.Generic;
using System.Drawing;

using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using Memt.Logger;
using TaskPlannerMetrum.Data.VO;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "4,1,DEPCNT")]

    public class FinancesController:ControllerBase
    {
  

     
        private IFinancesBusiness _financestBusiness;

        public FinancesController(ILogger<ProjectsController> logger, IFinancesBusiness financeBusiness)
        {
          
            _financestBusiness = financeBusiness;

        }
        
        [HttpGet("GetAllFinances")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
       
        public IActionResult GetAllContracts()
        {
            try
            {
                return Ok(_financestBusiness.GetAllFinances());

            }catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetFinancesById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetFinancesById(int contractId)
        {
            try
            {
                return Ok(_financestBusiness.GetFinancesById(contractId));

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Create(Model.DTO.financeDTO newfinance)
        {
            try
            {
                return Ok(_financestBusiness.Create(newfinance));

            }catch(Exception ex) { 
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut("UpdateFinances")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdateFinances(Model.Finances newfinance)
        {
            try
            {
                return Ok(_financestBusiness.UpdateFinances(newfinance));

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("DeleteFinance")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteFinance(int id)
        {
            try
            {
                return Ok(_financestBusiness.DeleteFinance(id));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetContractInfo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetContractInfo(int contractid)
        {
            try
            {
                return Ok(_financestBusiness.GetContractInfo(contractid));

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
        [HttpGet("getAllServices")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult getAllServices(string type)
        {
            try
            {
                return Ok(_financestBusiness.getAllServices(type));
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest( ex.Message);
            }
        }

        [HttpPost("CreateService")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreateService(Service serviceRequest)
        {
            try
            {
        
                var result = _financestBusiness.CreateAllService(serviceRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("DeleteAllService")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeleteAllService(int ID)
        {
            try
            {

                var result = _financestBusiness.DeleteAllService(ID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPost("DuplicateFinance")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DuplicateFinance(DuplicateFinanceDTO Finance)
        {
            try
            {
                return Ok(_financestBusiness.DuplicateFinance(Finance));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }
    }
}
