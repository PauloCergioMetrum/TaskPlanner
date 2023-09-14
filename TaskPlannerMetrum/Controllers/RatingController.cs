using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Data.VO;
using Microsoft.Extensions.Logging;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingBusiness _ratingBusiness;
        private readonly ILogger<RatingController> _logger; 

        public RatingController(IRatingBusiness ratingBusiness , ILogger<RatingController> logger)
        {
            _ratingBusiness=ratingBusiness;
            _logger = logger;
        }

        [HttpGet("GetAllUsersForProject")]
        [ProducesResponseType((200), Type = typeof(List<UserVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetAllUsersForProject( int projectID)
        {
            try
            {
                return Ok(_ratingBusiness.GetAllRatingforUser(projectID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

    }
}
