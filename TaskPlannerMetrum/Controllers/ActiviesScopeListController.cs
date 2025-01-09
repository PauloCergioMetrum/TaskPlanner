using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using Memt.Logger;

namespace TaskPlannerMetrum.Controllers
{
   
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize]
    public class ActiviesScopeListController: ControllerBase
    {
        private readonly IActiviesScopeBusiness _activiesScopeBusiness;
        private readonly ILogger<ActiviesScopeListController> _logger;
        
        


        public ActiviesScopeListController(IActiviesScopeBusiness activiesScopeBusiness, ILogger<ActiviesScopeListController> logger ) 
        {
            _logger= logger;
            _activiesScopeBusiness = activiesScopeBusiness;

        }

      
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]       
        public IActionResult Get( string projectId)
        {
            try
            {
               
                return Ok(_activiesScopeBusiness.GetActivesScopeByProject(projectId));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTaskById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetTaskById(string projectId)
        {
            try
            {
                return Ok(_activiesScopeBusiness.GetTaskById(projectId));

            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllTasks")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllTasks(int GrupId)
        {
            try
            {
                return Ok(_activiesScopeBusiness.GetllTasks(GrupId));

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTaskByGrup")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetTaskByGrup()
        {
            try
            {
                return Ok(_activiesScopeBusiness.GetTaskByGrup());

            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

    }
}
