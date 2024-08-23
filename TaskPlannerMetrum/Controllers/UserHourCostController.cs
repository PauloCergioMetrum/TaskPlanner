using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using Memt.Logger;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Http;
using CsvHelper;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "4,3")]

    public class UserHourCostController : ControllerBase
    {
        private readonly ILogger<UserHourCostController> _logger;
        private IUserHourCostBusiness _userHourCost;

        public UserHourCostController(ILogger<UserHourCostController> logger, IUserHourCostBusiness userHourCostBusiness)
        {
            _logger = logger;
            _userHourCost = userHourCostBusiness;
        }

        [HttpPost("CreateUserHourCost")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post(UserHourCosts userHourCost)
        {
            try
            {
                return Ok(_userHourCost.CreateHourCost(userHourCost));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUserHourCost/{ID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(string ID)
        {
            try
            {
                return Ok(_userHourCost.DeleteHourCost(ID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUserHourCost")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put(UserHourCosts userHourCost)
        {
            try
            {
                return Ok(_userHourCost.UpdateHourCost(userHourCost));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }

        //Type = typeof(List<UserVO>))]
        [HttpGet("ListUserHourCost/{userID}")]
        [ProducesResponseType(200, Type = typeof(List<UserHourCosts>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult ListUserHoursCost(int userID)
        {
            try
            {
                var userHourCostList = _userHourCost.ListUserHoursCost(userID);
                return Ok(userHourCostList);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("CreatHoursCostByExcel")]
        [ProducesResponseType(200, Type = typeof(List<UserHourCosts>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult CreatHoursCostByExcel([FromForm] IFormFile file, [FromForm] DateTime startDate, [FromForm] DateTime endDate)
        {
            try
            {
                var userHourCostList = _userHourCost.CreatHoursCostByExcel(file, startDate, endDate);

                if (userHourCostList.Result)
                {
                    return Ok("Horas Cadastradas");
                }
                else
                {
                    return BadRequest("Falha ao cadastrar horas.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("GetAllFunctions()")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAllFunctions()
        {
            try
            {
                return Ok( _userHourCost.GetAllFunctions());

                
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }





   
    }







}






