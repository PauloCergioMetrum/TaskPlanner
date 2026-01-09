
using TaskPlannerMetrum.Model.DTO;
using ClientsModel = TaskPlannerMetrum.Model.Clients;


namespace TaskPlannerMetrum.Repository.Clients
{
    public interface IClientsRepository
    {

        string NormalizeCnpj(string value);
        ClientsModel GetByCnpjIncludingSoftDeleted(string normalizedCnpj);
        bool CreateClients(ClientCreateDto dto);
        bool ReactivateClient(ClientsModel existing, ClientCreateDto dto);
        bool ExistClientIdInContracts(int id);
        bool DeleteClientById(int id);
        bool UpdateClients(ClientsModel clients);

    }
}