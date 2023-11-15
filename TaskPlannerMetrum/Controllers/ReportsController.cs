


using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlannerMetrum.Controllers
{

    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ReportsController : ControllerBase
    {

        private readonly IReportsPlannedExecutedViewewBussines _reportsPlannedExecutedViewerBusiness;

        public ReportsController(IReportsPlannedExecutedViewewBussines reportsViewerBusiness)
        {
            _reportsPlannedExecutedViewerBusiness = reportsViewerBusiness;
        }

        [HttpGet("ReportsPlannedExecuted")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult ReportsPlannedExecuted(DateTime startDate, DateTime endDate)
        {
            try
            {
                

                return Ok(_reportsPlannedExecutedViewerBusiness.ReportsPlannedExecuted(startDate, endDate));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }






    }
}




