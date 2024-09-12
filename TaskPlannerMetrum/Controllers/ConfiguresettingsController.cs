using Microsoft.AspNetCore.Mvc;
using System;
using TaskPlannerMetrum.Business.Implementations;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Controllers
{
   

    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
   

    public class ConfiguresettingsController : ControllerBase
    {
        private IConfigureSettingsBusinesss _configureSettingsBusinesss;

        public ConfiguresettingsController(IConfigureSettingsBusinesss configureSettingsBusinesss)
        {
            _configureSettingsBusinesss = configureSettingsBusinesss;
        }

        [HttpPost("UpdateConectionString")]

        public IActionResult UpdateConectionString(ConfigureSettingsDTO sourceID)
        {
            try
            {
                var response = _configureSettingsBusinesss.UpdateConectionString(sourceID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        
    }
}
