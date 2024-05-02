using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using TaskPlannerMetrum.Business;

using TaskPlannerMetrum.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlannerMetrum.Controllers
{


    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "1,4,DEPCNT")]
    public class CalenderController: ControllerBase
    {

        private readonly ICalendarBusiness _calenderBusiness;
        private readonly ILogger<CalenderController> _logger;
        public CalenderController(ICalendarBusiness calenderBusiness, ILogger<CalenderController> logger)
        {
            _logger = logger;
            _calenderBusiness = calenderBusiness;

        }

        [HttpGet("UsersCalender")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult UsersCalender (string depid)
        {
            try
            {
                return Ok(_calenderBusiness.UsersCalender(depid));

            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }

        [HttpGet("TaskforUsers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]

   
        public IActionResult TaskforUsers(int userid, string name)
        {
            try
            {
                var userID = new AllUserCalendar
                {
                    DepName = name,
                    userID=userid

                };
                return Ok(_calenderBusiness.TaskforUsers(userID));
            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message); 
            }
        }

        [HttpGet("UsersForProjects")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]


        public IActionResult UsersForProjects(int contractID)
        {
            try
            {
                
                return Ok(_calenderBusiness.UsersForProjects(contractID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
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
                return Ok(_calenderBusiness.GetAllContracts());

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }








    }
}
