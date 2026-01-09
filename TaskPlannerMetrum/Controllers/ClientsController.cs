using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
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
        [ProducesResponseType(200, Type = typeof(List<ClientEntity>))]
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
        public IActionResult CreateClients([FromBody] ClientCreateDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Cnpj))
                    return BadRequest(new { message = "Cnpj é obrigatório." });

                var normalized = _clientsBusiness.NormalizeCnpj(dto.Cnpj);
                var existing = _clientsBusiness.GetByCnpjIncludingSoftDeleted(normalized);

                if (existing != null)
                {
                    var wasSoftDeleted = existing.SoftDelete == true;

                    var okSave = _clientsBusiness.ReactivateClient(existing, dto);
                    if (!okSave)
                        return BadRequest(new { message = "Não foi possível salvar o cliente." });

                    if (wasSoftDeleted)
                        return Ok(new { message = "Cliente reativado com sucesso." });

                    return Ok(new { message = "Cliente atualizado com sucesso." });
                }

                var okCreate = _clientsBusiness.CreateClients(dto);
                if (!okCreate)
                    return BadRequest(new { message = "Não foi possível cadastrar o cliente." });

                return Ok(new { message = "Cliente cadastrado com sucesso." });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                Logger.Log(msg, ELoggerType.Debug);
                return BadRequest(new { message = msg });

            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200, Type = typeof(ClientEntity))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public IActionResult UpdateClients(int id, ClientEntity clients)
        {
            try
            {
                if (id != clients.Id) return BadRequestMsg("O Id do caminho difere do corpo.");


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
                    return Conflict(new { message = "Não é possível remover o cliente: há pedidos de venda vinculados. Em Pedidos de Venda, troque o cliente  para outro e tente novamente." });

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
    }
}

