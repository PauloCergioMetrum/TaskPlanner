using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Business
{
    public interface IClientsBusiness
    {
        Clients Create(Clients department);
        //UserVO FindByID(long id);        
        dynamic FindAll();
    }
}
