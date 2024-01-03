using TaskPlannerMetrum.Model;
using Pomelo.EntityFrameworkCore.MySql.Metadata.Internal;
namespace TaskPlannerMetrum.Business
{
    public interface IClientsBusiness
    {


        
        dynamic FindAll();
        object FindById(int id);
        bool CreateClients(Model.Clients clients);
      
        bool ExistCnpj(Model.Clients clients);



    }
}
