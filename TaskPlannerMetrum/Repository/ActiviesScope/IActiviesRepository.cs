using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ActiviesScope
{
    public interface IActiviesRepository
    {
        public List<ActiviesScopeList> GetActiviesScopeByProject(string ProectID);

        public List<vProjectList> GetTaskById(string ProectID);

        public dynamic GetAllTasks(int GrupId);

        public dynamic GetTaskByGrup();

    }
}
