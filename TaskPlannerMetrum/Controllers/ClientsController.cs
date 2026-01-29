using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "4,1,DEPCNT")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientsBusiness _clientsBusiness;

        public ClientsController(ILogger<ClientsController> logger, IClientsBusiness clientsBusiness)
        {
            _logger = logger;
            _clientsBusiness = clientsBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<Clients>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_clientsBusiness.FindAll());
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        public IActionResult CreateClients([FromBody] ClientCreateDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Cnpj))
                    return BadRequest(new { message = "Cnpj é obrigatório." });

                var result = _clientsBusiness.UpsertClientByCnpj(dto);

                if (result == null)
                    return BadRequest(new { message = "Não foi possível salvar o cliente." });

                if (result == "exists")
                    return Conflict(new { message = "Cliente já cadastrado." });

                if (result == "reactivated")
                    return Ok(new { message = "Cliente reativado com sucesso." });

                return Ok(new { message = "Cliente cadastrado com sucesso." });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                Logger.Log(msg, ELoggerType.Debug);
                return BadRequest(new { message = msg });
            }
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public IActionResult DeleteClientById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "Id é obrigatório." });

                if (_clientsBusiness.ExistClientIdInContracts(id))
                    return Conflict(new
                    {
                        message = "Não é possível excluir este cliente porque existem pedidos de venda vinculados a ele. Para excluir, acesse a tela de Pedido de Venda, altere o cliente desses pedidos para outro cliente e, em seguida, tente excluir este cliente novamente."
                    });

                var ok = _clientsBusiness.DeleteClientById(id);
                if (!ok)
                    return NotFound(new { message = "Cliente não encontrado ou já foi excluído." });

                return Ok(new { message = "Cliente excluído com sucesso." });
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClientById(int id, [FromBody] Clients clients)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "Id é obrigatório." });

                if (clients == null)
                    return BadRequest(new { message = "Payload inválido." });

                clients.Id = id;

                if (!string.IsNullOrWhiteSpace(clients.Cnpj))
                    clients.Cnpj = _clientsBusiness.NormalizeCnpj(clients.Cnpj);

                var ok = _clientsBusiness.UpdateClients(clients);

                if (!ok)
                    return NotFound(new { message = "Cliente não encontrado." });

                return Ok(new { message = "Cliente atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
