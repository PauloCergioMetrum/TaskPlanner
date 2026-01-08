using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Business;
using ClientEntity = TaskPlannerMetrum.Model.Clients;

namespace TaskPlannerMetrum.Controllers
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "4,1,DEPCNT")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientsBusiness _clientsBusiness;

        private class ApiResponse<T>
        {
            public bool Success { get; init; }
            public string Message { get; init; }
            public T Data { get; init; }
            public static ApiResponse<T> Ok(T data, string message) => new() { Success = true, Message = message, Data = data };
            public static ApiResponse<T> Fail(string message) => new() { Success = false, Message = message, Data = default };
        }
        private IActionResult OkMsg<T>(T data, string message) => Ok(ApiResponse<T>.Ok(data, message));
        private IActionResult CreatedAtRouteMsg<T>(string routeName, object routeValues, T data, string message)
            => CreatedAtRoute(routeName, routeValues, ApiResponse<T>.Ok(data, message));
        private IActionResult ConflictMsg(string message) => Conflict(ApiResponse<object>.Fail(message));
        private IActionResult BadRequestMsg(string message) => BadRequest(ApiResponse<object>.Fail(message));
        private IActionResult NotFoundMsg(string message) => NotFound(ApiResponse<object>.Fail(message));


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
                var list = _clientsBusiness.FindAll();
                if (list == null) return NoContent();
                return OkMsg(list, "Clientes obtidos com sucesso.");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequestMsg("Erro ao obter clientes.");
            }
        }

        [HttpGet("{id:int}", Name = "GetClientById")]
        [ProducesResponseType(200, Type = typeof(ClientEntity))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetById(int id)
        {
            try
            {
                var entity = _clientsBusiness.FindById(id) as ClientEntity;
                if (entity == null) return NotFoundMsg("Cliente não encontrado.");
                return OkMsg(entity, "Cliente obtido com sucesso.");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequestMsg("Erro ao obter cliente.");
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ClientEntity))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        public IActionResult CreateClients(ClientEntity clients)
        {
            try
            {
                if (_clientsBusiness.ExistCnpj(clients))
                    return ConflictMsg("Já existe cliente com este CNPJ.");

                var ok = _clientsBusiness.CreateClients(clients);
                if (!ok) return BadRequestMsg("Erro ao criar cliente.");

                return CreatedAtRouteMsg(
                    "GetClientById",
                    new { id = clients.Id, version = HttpContext.GetRouteValue("version")! },
                    clients,
                    "Cliente criado com sucesso."
                );
            }
            catch (InvalidOperationException ioe)
            {
                var message = ioe.Message switch
                {
                    "DUPLICATE_CNPJ" => "Já existe cliente com este CNPJ.",
                    _ => "Erro ao criar cliente."
                };
                return ConflictMsg(message);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx &&
                    (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    return ConflictMsg("Já existe cliente com este CNPJ.");
                }
                return ConflictMsg("Erro ao criar cliente por conflito de dados.");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequestMsg("Erro ao criar cliente.");
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

                var current = _clientsBusiness.FindById(id) as ClientEntity;
                if (current == null) return NotFoundMsg("Cliente não encontrado.");

                if (_clientsBusiness.ExistCnpj(clients))
                    return ConflictMsg("Já existe cliente com este CNPJ.");

                var ok = _clientsBusiness.UpdateClients(clients);
                if (!ok) return BadRequestMsg("Erro ao atualizar cliente.");

                return OkMsg(clients, "Cliente atualizado com sucesso.");
            }
            catch (InvalidOperationException ioe)
            {
                var message = ioe.Message switch
                {
                    "DUPLICATE_CNPJ" => "Já existe cliente com este CNPJ.",
                    _ => "Erro ao atualizar cliente."
                };
                return ConflictMsg(message);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx &&
                    (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    return ConflictMsg("Já existe cliente com este CNPJ.");
                }
                return ConflictMsg("Erro ao atualizar cliente por conflito de dados.");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequestMsg("Erro ao atualizar cliente.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public IActionResult DeleteClients(int id)
        {
            try
            {
                var current = _clientsBusiness.FindById(id) as ClientEntity;
                if (current == null) return NotFoundMsg("Cliente não encontrado.");

                var ok = _clientsBusiness.DeleteClients(id);
                if (!ok) return BadRequestMsg("Não foi possível deletar o cliente.");


                return OkMsg<object>(null, "Cliente deletado com sucesso.");
            }
            catch (InvalidOperationException ioe)
            {
                string userMessage = ioe.Message switch
                {
                    "HAS_SALES_ORDERS_AND_CONTRACTS" => "Não é possível excluir: existem Pedidos de Venda e Contratos vinculados a este cliente.",
                    "HAS_SALES_ORDERS" => "Não é possível excluir: existem Pedidos de Venda vinculados a este cliente.",
                    "HAS_CONTRACTS" => "Não é possível excluir: existem Contratos vinculados a este cliente.",
                    "DUPLICATE_CNPJ" => "Já existe cliente com este CNPJ.",
                    _ => "Não é possível excluir. Existem registros vinculados a este cliente."
                };
                return ConflictMsg(userMessage);
            }
            catch (DbUpdateException)
            {
                return ConflictMsg("Não é possível excluir. Existem registros vinculados a este cliente.");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequestMsg("Erro ao deletar cliente.");
            }
        }
    }
}