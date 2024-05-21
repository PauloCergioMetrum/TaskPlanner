using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlannerMetrum.Controllers
{

    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "4,1")]
    public class ContractViewerController:ControllerBase
    {

        private readonly IContractViewerBusiness _contractViewerBusiness;

        public ContractViewerController(IContractViewerBusiness contractViewerBusiness)
        {
            _contractViewerBusiness = contractViewerBusiness;
            
            // subindo
        }

        [HttpGet("GetAllContractsView")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]


        public IActionResult GetAllContractsView(string Date)
        {
            try
            {
                return Ok(_contractViewerBusiness.GetAllContractsView(Date));   

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }
    }
}
