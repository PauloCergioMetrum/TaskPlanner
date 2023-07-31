using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IActiviesScopeBusiness
    {
        public dynamic GetActivesScopeByProject(string projectId);

        public dynamic GetTaskById(string ProectID);

        public dynamic GetllTasks(int GrupId); 

        public dynamic GetTaskByGrup();
       
    }
}
