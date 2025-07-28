


using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.Generic;
using TaskPlannerMetrum.Repository.ReportsViewer;
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
            catch (Exception ex)
            {
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

        [HttpPost("ContractGraphicRequest")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetContracts([FromBody] ContractGraphicRequest request)
        {
            var contractsResult = await _reportsPlannedExecutedViewerBusiness.GetContractsByRequestAsync(request);
            return contractsResult.Any() ? Ok(contractsResult) : NoContent();
        }


        [HttpGet("PlannedExecutionSummary/{internalCode}")]
        public async Task<IActionResult> GetPlannedExecutionSummaryController(string internalCode)
        {
            var result = await _reportsPlannedExecutedViewerBusiness.GetPlannedVsExecutedVsExpectedHoursAsync(internalCode);

            if (result == null)
                return NoContent();

            return Ok(result);
        }






        //Relatorio - Relatório Executivo de Projetos 
        [HttpPost("ProjectExecutiveReport")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ProjectExecutiveReport([FromBody] GetProjectExecutiveStatusDto dto)
        {
            try
            {
                var result = await _reportsPlannedExecutedViewerBusiness.GetProjectExecutiveCombinedReportAsync(dto);

                if ((result?.GetProjectExecutiveStatus == null || !result.GetProjectExecutiveStatus.Any()) &&
                    (result?.GetTableProjectExecutiveReport == null || !result.GetTableProjectExecutiveReport.Any() &&
                    result?.GetExecutiveProjectStatusPeriod == null || !result.GetExecutiveProjectStatusPeriod.Any()))
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }
    }

}
















