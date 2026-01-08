using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.Clients;
using TaskPlannerMetrum.Repository.Generic;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ClientsBusinessImplementation : IClientsBusiness
    {
        private readonly IRepository<Clients> _repository;
        private readonly IClientsRepository _repositoryClients;

        public ClientsBusinessImplementation(IRepository<Clients> repository, IClientsRepository repositoryClients)
        {
            _repository = repository;
            _repositoryClients = repositoryClients;
        }

        public dynamic FindAll()
        {
            return _repository
                .FindAll()
                .Where(c => c.SoftDelete == null || c.SoftDelete == false)
                .OrderBy(c => c.Name);
        }

        public string NormalizeCnpj(string value)
        {
            return _repositoryClients.NormalizeCnpj(value);
        }

        public Clients GetByCnpjIncludingSoftDeleted(string cnpj)
        {
            var normalized = _repositoryClients.NormalizeCnpj(cnpj);
            return _repositoryClients.GetByCnpjIncludingSoftDeleted(normalized);
        }

        public bool CreateClients(ClientCreateDto dto)
        {
            return _repositoryClients.CreateClients(dto);
        }

        public bool ReactivateClient(Clients existing, ClientCreateDto dto)
        {
            return _repositoryClients.ReactivateClient(existing, dto);
        }

        public bool ExistClientIdInContracts(int id)
        {
            return _repositoryClients.ExistClientIdInContracts(id);
        }

        public bool DeleteClientById(int id)
        {
            return _repositoryClients.DeleteClientById(id);
        }

        public bool UpdateClients(Clients clients)
        {
            return _repositoryClients.UpdateClients(clients);
        }
    }
}
