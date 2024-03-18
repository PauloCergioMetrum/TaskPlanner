using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Controllers
{
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class BusinessUnitController : ControllerBase
    {
        private readonly IBusinessUnitBusiness _businessUnitBusiness;

        public BusinessUnitController(IBusinessUnitBusiness businessUnitBusiness)
        {
            _businessUnitBusiness = businessUnitBusiness;
        }

        [HttpGet]
        [Route("BusinessUnit")]
        public IActionResult GetAllBuninesUnit()
        {
            try
            {
                var businessUnits = _businessUnitBusiness.GetAllBusinessUnits();
                return Ok(businessUnits);
            }
            catch (Exception ex)
            {
          
                return BadRequest(ex.Message);
            }
        }
    }
}
