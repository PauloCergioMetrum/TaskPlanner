using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Controllers
{

    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;

        // Declaration of the service used
        private IClientsBusiness _clientsBusiness;

        public ClientsController(ILogger<ClientsController> logger, IClientsBusiness clientsBusiness)
        {
            _logger = logger;
            _clientsBusiness = clientsBusiness;

        }

        // Maps GET requests to https://localhost:{port}/api/person
        // Get no parameters for FindAll -> Search All
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<Clients>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            try
            {
                return Ok(_clientsBusiness.FindAll());

            }catch(Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);  
            }
        }
    }
}
