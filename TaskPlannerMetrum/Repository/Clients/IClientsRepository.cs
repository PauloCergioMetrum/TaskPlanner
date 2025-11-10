using ClientEntity = TaskPlannerMetrum.Model.Clients;

namespace TaskPlannerMetrum.Repository.Clients
{
    public interface IClientsRepository
    {
        bool CreateClients(ClientEntity clients);
        bool UpdateClients(ClientEntity clients);
        bool DeleteClients(int id);
        ClientEntity GetById(int id);
        bool ExistCnpj(ClientEntity clients);
    }
}