using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Controllers
{

    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "1,2,3,4,DEPCNT")]
    public class ActivityPlanController : ControllerBase
    {
        private readonly IActivityPlanBusiness _activityPlanBusiness;
        private readonly ILogger<ActivityPlanController> _logger;
        public ActivityPlanController(IActivityPlanBusiness activityPlanBusiness, ILogger<ActivityPlanController> logger)
        {
            _logger = logger;
            _activityPlanBusiness = activityPlanBusiness;
        }


        [HttpGet("ExecutorPlannerList")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        [ProducesResponseType(401)]

        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetExecutorPlanner(string projectId)
        {

            return Ok(_activityPlanBusiness.GetExecutorPlan(projectId));
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Create(ActivePlanList activityPlan)

        {
            try
            {
                return Ok(_activityPlanBusiness.Create(activityPlan));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }
        [HttpGet("TasksByProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult FindTask(string projectId, int? page, int? size, string searchExecutor)
        {
            int pageSize = (size ?? 10);
            int pageNumber = (page ?? 1);

            return Ok(_activityPlanBusiness.TasksByProject(projectId, pageNumber, pageSize, searchExecutor));
        }
        [HttpGet("TasksByUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult FindTaskByUser(string userId, int? page, int? size, string searchExecutor)
        {
            int pageSize = (size ?? 10);
            int pageNumber = (page ?? 1);

            return Ok(_activityPlanBusiness.TasksByUser(userId, pageNumber, pageSize, searchExecutor));
        }

        [HttpGet("LoadTaskUsers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult TaskByUsers(string userId, string searchExecutor)
        {
            try
            {
                return Ok(_activityPlanBusiness.LoadTaskUsers(userId, searchExecutor));

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("ActivityPlanByID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult FindActivityPlan(string activityId)
        {
            try
            {
                return Ok(_activityPlanBusiness.GetActivityPlan(activityId));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("UpdateTaskExecutor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult UpadteTaskExecutor(vActivityPlan activityPlan)
        {

            try
            {
                return Ok(_activityPlanBusiness.UpdateActivityPlan(activityPlan));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTaskExecutor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DeletID(int id)
        {
            try
            {
                return Ok(_activityPlanBusiness.DeleteId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetNameProjectByid")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetNameProjectByid(int id)
        {
            try
            {
                return Ok(_activityPlanBusiness.GetNamePorject(id));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpGet("GetDepartamentProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetDepartamentProject(int id)
        {
            try
            {
                return Ok(_activityPlanBusiness.GetDepartamentProject(id));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetUserTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetUserTask(int id)
        {
            try
            {
                return Ok(_activityPlanBusiness.GetUserTask(id));

            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPost("GetDepTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetDepTask(UserDepForTask usertask)
        {
            try
            {
                return Ok(_activityPlanBusiness.GetDepTask(usertask));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("DuplicateTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DuplicateTask(Model.DuplicatTask task)
        {
            try
            {
                return Ok(_activityPlanBusiness.DuplicateTask(task));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updatRating")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult updatRating(Model.RatingUpdate updatRating)
        {
            try
            {
                return Ok(_activityPlanBusiness.UpdateRating(updatRating));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserforTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetUserforTask(int id)
        {
            try
            {
                return Ok(_activityPlanBusiness.GetUserforTask(id));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateNotes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdateNotes(int taskID, string notesExecut, string notesPlanned, string identifier)
        {
            try
            {
                return Ok(_activityPlanBusiness.UpdateNotes(taskID, notesExecut, notesPlanned, identifier));

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateTaskDescription")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult UpdateTaskDescription(int taskID, string taskDescription)
        {
            try
            {
                return Ok(_activityPlanBusiness.UpdateTaskDescription(taskID, taskDescription));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBusinessUnitByContract")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetBusinessUnitByContract(int ContractID)
        {
            try
            {
                return Ok(_activityPlanBusiness.GetBusinessUnitByContract(ContractID));
            }
            catch (Exception ex) {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }

        }





    }
}
