using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "4,1")]
    public class DepartmentController : ControllerBase
    {

        private readonly ILogger<DepartmentController> _logger;

        // Declaration of the service used
        private IDepartmentBusiness _departmentBusiness;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentBusiness departmentBusiness)
        {
            _logger = logger;
            _departmentBusiness = departmentBusiness;

        }

        // Maps GET requests to https://localhost:{port}/api/person
        // Get no parameters for FindAll -> Search All
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<Department>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            try
            {
                return Ok(_departmentBusiness.FindAll());

            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }

    }
}
