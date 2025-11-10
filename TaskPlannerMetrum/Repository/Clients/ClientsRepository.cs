using System;
using System.Linq;
using TaskPlannerMetrum.Model.Context;
using ClientEntity = TaskPlannerMetrum.Model.Clients;

namespace TaskPlannerMetrum.Repository.Clients
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly MSSQLContext _context;

        public ClientsRepository(MSSQLContext context)
        {
            _context = context;
        }

        public bool CreateClients(ClientEntity clients)
        {
            var duplicate = _context.Clients.FirstOrDefault(c => c.Cnpj == clients.Cnpj && c.Id != clients.Id);
            if (duplicate != null) throw new InvalidOperationException("DUPLICATE_CNPJ");
            _context.Clients.Add(clients);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateClients(ClientEntity clients)
        {
            var current = _context.Clients.FirstOrDefault(c => c.Id == clients.Id);
            if (current == null) return false;
            var duplicate = _context.Clients.FirstOrDefault(c => c.Cnpj == clients.Cnpj && c.Id != clients.Id);
            if (duplicate != null) throw new InvalidOperationException("DUPLICATE_CNPJ");
            _context.Entry(current).CurrentValues.SetValues(clients);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteClients(int id)
        {
            var current = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (current == null) return false;


            var hasContracts = _context.Contracts.Any(c => c.ClientID == id);

            if (hasContracts)
                throw new InvalidOperationException("HAS_CONTRACTS");

            _context.Clients.Remove(current);
            _context.SaveChanges();
            return true;
        }

        public ClientEntity GetById(int id) => _context.Clients.FirstOrDefault(c => c.Id == id);

        public bool ExistCnpj(ClientEntity clients)
        {
            var exist = _context.Clients.FirstOrDefault(c => c.Cnpj == clients.Cnpj && c.Id != clients.Id);
            return exist != null;
        }
    }
}