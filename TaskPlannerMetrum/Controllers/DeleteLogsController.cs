using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteLogsController : ControllerBase
    {
        private readonly ILogBusinesssImplementation _logBusiness;

        public DeleteLogsController(ILogBusinesssImplementation logBusiness)
        {
            _logBusiness = logBusiness;
        }

        [HttpDelete("DeleteLogs")]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)] 
        [ProducesResponseType(500)] 
        public IActionResult DeleteLogs([FromBody] List<Model.Log> logs)
        {
            try
            {
                
                if (logs == null || logs.Count == 0)
                {
                    return BadRequest(new { Message = "A lista de logs não pode estar vazia." });
                }

              
                var userId = 1;
                var userName = "Admin"; 

          
                _logBusiness.DeleteLogs(logs, userId, userName);

                return Ok(new { Message = "Logs deletados com sucesso." });
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Erro ao deletar logs: {ex.Message}");
                return StatusCode(500, new { Message = "Ocorreu um erro ao processar sua solicitação.", Details = ex.Message });
            }
        }
    }
}
