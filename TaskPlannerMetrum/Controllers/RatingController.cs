using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Data.VO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "4,1")]

    public class RatingController : ControllerBase
    {
        private readonly IRatingBusiness _ratingBusiness;
        private readonly ILogger<RatingController> _logger;

        public RatingController(IRatingBusiness ratingBusiness, ILogger<RatingController> logger)
        {
            _ratingBusiness = ratingBusiness;
            _logger = logger;
        }

        [HttpGet("GetAllRatingProjectExecutors")]
        [ProducesResponseType((200), Type = typeof(List<UserVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetAllRatingProjectExecutors(int projectID, int UserID)
        {
            try
            {
                return Ok(_ratingBusiness.GetAllRatingProjectExecutor(projectID, UserID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllRatingProjectLeader")]
        [ProducesResponseType((200), Type = typeof(List<UserVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetAllRatingProjectLeader(int projectID, int UserID)
        {
            try
            {
                return Ok(_ratingBusiness.GetAllRatingProjectLeader(projectID, UserID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetFiscalRatingProject")]
        [ProducesResponseType((200), Type = typeof(List<UserVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetFiscalRatingProject(int projectID)
        {
            try
            {
                return Ok(_ratingBusiness.GetFiscalRatingProject(projectID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }





        [HttpPut("RatingRetroactive")]
        [ProducesResponseType((200), Type = typeof(List<UserVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult RatingRetroactive(int ContractId , int MilestonesID)
        {
            try
            {
                _ratingBusiness.RatingRetroactive(ContractId , MilestonesID);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateRating")]
        [ProducesResponseType((200), Type = typeof(List<UserVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult UpdateRating(Model.RatingDTOAll ratings)
        {
            try
            {
                return Ok(_ratingBusiness.UpdateRating(ratings));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

    }

}

