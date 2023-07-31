using System.Collections.Generic;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Business
{
    public interface IDepartmentBusiness
    {
        Department Create(Department department);
        //UserVO FindByID(long id);

        
        dynamic FindAll();
        //UserVO Update(UserVO person);
        //void Delete(long id);
    }
}
