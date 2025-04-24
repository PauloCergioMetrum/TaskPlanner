using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskPlannerMetrum.Repository.GetContractsByMonthRepository;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/")]
    [Authorize(Roles = "4,1")]
    [ApiVersion("1.0")]
    public class GetContractsByMonthController : ControllerBase
    {
        private readonly IGetContractsByMonthRepository _repository;

        public GetContractsByMonthController(IGetContractsByMonthRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var result = await _repository.GetAsync(startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Aqui você pode logar o erro se quiser, exemplo:
                // Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex.Message);
            }
        }
    }
}
