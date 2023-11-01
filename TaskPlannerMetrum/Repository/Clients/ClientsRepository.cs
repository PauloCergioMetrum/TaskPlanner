using System.Linq;
using TaskPlannerMetrum.Model.Context;

namespace TaskPlannerMetrum.Repository.Clients
{
    public class ClientsRepository : IClientsRepository
    {
        private MSSQLContext _context;

        public ClientsRepository(MSSQLContext context) { 
            _context = context;
        }
            

        public bool CreateClients(Model.Clients clients)
        {
             _context.Clients.Add(clients);
            _context.SaveChanges();
             return true;
        }


        public bool ExistCnpj(Model.Clients clients)
        {
            var exist = _context.Clients.Where(c => c.Cnpj == clients.Cnpj).FirstOrDefault();

            if (exist != null) return true; return false;



        }







    }
}
