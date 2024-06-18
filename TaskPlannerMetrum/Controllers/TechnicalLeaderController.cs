using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.CompilerServices;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Business.Implementations;

namespace TaskPlannerMetrum.Controllers
{
    public class TechnicalLeaderController :ControllerBase
    {


     

       private readonly ITechnicalLeaderBusiness _technicalLeaderBusiness;


        public  TechnicalLeaderController(ITechnicalLeaderBusiness technicalLeaderBusiness)
        {
            _technicalLeaderBusiness = technicalLeaderBusiness; 
        }




        [HttpGet("GetAllTechnicalLeader")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllTechnicalLeader()
        {
            try
            {
                return Ok(_technicalLeaderBusiness .GetAllTechnicalLeader());

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

    }
}
