using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.Executive;
namespace TaskPlannerMetrum.Controllers
{
   

    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize]


    public class ExecutiveController : ControllerBase
    {
        private readonly ILogger<ExecutiveController> _logger;
        private readonly IExecutiveBussines _executiveBussines;

        public ExecutiveController(ILogger<ExecutiveController> logger, IExecutiveBussines executiveBussines)
        {
            _logger = logger;
            _executiveBussines = executiveBussines;
        }

        [HttpPost("GetExecutiveData")]
        public IActionResult GetExecutiveData([FromBody] ExecutiveRequestDto request)
        {
            try
            {
                var result = _executiveBussines.CreateExecutiveDtoAll(request.InspectorIDs, request.StartDate, request.EndDate);
                var openCloseResults = _executiveBussines.GetExecutivePvGraphicsOpenClose(request.Closed, request.Open);

          
                var response = new
                {
                    ExecutiveData = result,
                    OpenCloseGraphics = openCloseResults
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

}
