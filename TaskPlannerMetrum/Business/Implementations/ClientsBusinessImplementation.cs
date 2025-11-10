using System.Linq;
using ClientEntity = TaskPlannerMetrum.Model.Clients;
using TaskPlannerMetrum.Repository.Clients;
using TaskPlannerMetrum.Repository.Generic;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ClientsBusinessImplementation : IClientsBusiness
    {
        private readonly IRepository<ClientEntity> _repository;
        private readonly IClientsRepository _repositoryClients;

        public ClientsBusinessImplementation(IRepository<ClientEntity> repository, IClientsRepository repositoryClients)
        {
            _repository = repository;
            _repositoryClients = repositoryClients;
        }

        public bool CreateClients(ClientEntity clients) => _repositoryClients.CreateClients(clients);

        public bool ExistCnpj(ClientEntity clients) => _repositoryClients.ExistCnpj(clients);

        public dynamic FindAll() => _repository.FindAll().OrderBy(n => n.Name);

        public object FindById(int id) => _repositoryClients.GetById(id);

        public bool UpdateClients(ClientEntity clients) => _repositoryClients.UpdateClients(clients);

        public bool DeleteClients(int id) => _repositoryClients.DeleteClients(id);
    }
}