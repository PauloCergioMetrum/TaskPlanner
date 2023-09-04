namespace TaskPlannerMetrum.Repository.Clients
{
    public interface IClientsRepository
    {
        bool CreateClients(Model.Clients clients);

        bool ExistCnpj(Model.Clients clients);



    }
}
