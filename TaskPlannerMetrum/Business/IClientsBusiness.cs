using TaskPlannerMetrum.Model;
using ClientEntity = TaskPlannerMetrum.Model.Clients;

namespace TaskPlannerMetrum.Business
{
    public interface IClientsBusiness
    {
        dynamic FindAll();
        object FindById(int id);
        bool CreateClients(ClientEntity clients);
        bool UpdateClients(ClientEntity clients);
        bool DeleteClients(int id);
        bool ExistCnpj(ClientEntity clients);
    }
}