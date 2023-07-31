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


namespace TaskPlannerMetrum.Controllers 
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class DashboardController: ControllerBase

    {
        private readonly IDashboardBusiness _projectprogress;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(IDashboardBusiness projecprogress, ILogger<DashboardController> logger)
        {
            _logger = logger;
            _projectprogress = projecprogress;
       
        }
        [HttpGet("StatusPercentage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult StatusPercentage()
        {
            try
            {
                return Ok(_projectprogress.GetStatus());

            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message); 
            }
        }
    }

}
