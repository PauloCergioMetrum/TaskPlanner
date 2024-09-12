


using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlannerMetrum.Controllers
{

    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "4,1")]

    public class ReportsController : ControllerBase
    {

        private readonly IReportsPlannedExecutedViewewBussines _reportsPlannedExecutedViewerBusiness;

        public ReportsController(IReportsPlannedExecutedViewewBussines reportsViewerBusiness)
        {
            _reportsPlannedExecutedViewerBusiness = reportsViewerBusiness;
        }

        [HttpPost("ReportsPlannedExecuted")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult ReportsPlannedExecuted(ReportPlannedExecutedDTO reportPlannedExecuted)
        {
            try
            {


                return Ok(_reportsPlannedExecutedViewerBusiness.GetPlannedExecuted(reportPlannedExecuted));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("OperationalProjectReport")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult OperationalProjectReport(OperationalReportReportDTO OperationalReportReportDTO)
        {
            try
            {
                return Ok(_reportsPlannedExecutedViewerBusiness.OperationalProjectReport(OperationalReportReportDTO));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("OptionsListFilter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult OptionsListFilter()
        {
            try
            {
                return Ok(_reportsPlannedExecutedViewerBusiness.OptionsListFilter());
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("InvoiceReport")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult InvoiceReport(ReportInvoice reportInvoice)
        {
            try
            {
                return Ok(_reportsPlannedExecutedViewerBusiness.InvoiceReport(reportInvoice));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }





    }
}




