using TaskPlannerMetrum.Model;
using Pomelo.EntityFrameworkCore.MySql.Metadata.Internal;
namespace TaskPlannerMetrum.Business
{
    public interface IClientsBusiness
    {


        Clients Create(Clients clients);
        dynamic FindAll();
        object FindById(int id);
        bool CreateClients(Model.Clients clients);
        object Create(object clients);
        bool ExistCnpj(Model.Clients clients);



    }
}
