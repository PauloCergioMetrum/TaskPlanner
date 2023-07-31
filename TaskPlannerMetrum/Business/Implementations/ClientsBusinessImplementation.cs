using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Repository.Generic;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ClientsBusinessImplementation : IClientsBusiness
    {
        private readonly IRepository<Clients> _repository;

        public ClientsBusinessImplementation(IRepository<Clients> repository)
        {
            _repository = repository;
        }
        public Clients Create(Clients department)
        {
            throw new System.NotImplementedException();
        }

        public dynamic FindAll()
        {
            var clients = _repository.FindAll().Select(s => new { s.Id, s.Name,s.Cnpj}).OrderBy(n => n.Name);
            return clients;
        }
    }
}
