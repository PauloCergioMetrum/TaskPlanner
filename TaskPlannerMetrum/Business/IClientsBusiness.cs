using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface IClientsBusiness
    {
        dynamic FindAll();

        Clients GetByCnpjIncludingSoftDeleted(string cnpj);
        bool CreateClients(ClientCreateDto dto);
        bool ReactivateClient(Clients existing, ClientCreateDto dto);
        bool ExistClientIdInContracts(int id);
        bool DeleteClientById(int id);
        bool UpdateClients(Clients clients);
        string NormalizeCnpj(string value);

        string UpsertClientByCnpj(ClientCreateDto dto);
    }
}
