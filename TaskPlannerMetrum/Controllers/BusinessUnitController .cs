using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Business.Implementations;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Controllers
{
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    [Authorize(Roles = "4,1")]
    public class BusinessUnitController : ControllerBase
    {
        private readonly IBusinessUnitBusiness _businessUnitBusiness;

        public BusinessUnitController(IBusinessUnitBusiness businessUnitBusiness)
        {
            _businessUnitBusiness = businessUnitBusiness;
        }


        [HttpGet("GetBusinessUnit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]


        public IActionResult GetBusinessUnit()
        {
            try
            {

                return Ok(_businessUnitBusiness.GetBusinessUnit());
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

    
    }
}
