using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Repository.Clients;
using TaskPlannerMetrum.Repository.Generic;


namespace TaskPlannerMetrum.Business.Implementations
{
    public class ClientsBusinessImplementation : IClientsBusiness
    {
        private readonly IRepository<Clients> _repository;

        private readonly IClientsRepository _repositoryClients;

        public ClientsBusinessImplementation(IRepository<Clients> repository , IClientsRepository repositoryClients )
        {
            _repository = repository;
            _repositoryClients = repositoryClients;
        }

        public bool CheckCnpjExistence(string cnpj)
        {
            throw new System.NotImplementedException();
        }

        public Clients Create(Clients department)
        {
            throw new System.NotImplementedException();
        }



        public object Create(object clients)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateClients(Clients clients)
        {
             return _repositoryClients.CreateClients(clients);


         

        }

        public bool ExistCnpj(Clients clients)
        {
            return _repositoryClients.ExistCnpj(clients);

        }

        public dynamic FindAll()
        {
            var clients = _repository.FindAll().OrderBy(n => n.Name);
            return clients;
        }

        public object FindById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
