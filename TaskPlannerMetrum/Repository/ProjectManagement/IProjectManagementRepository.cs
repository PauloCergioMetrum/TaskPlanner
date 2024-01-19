using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public interface IProjectManagementRepository
    {

        public Model.Contracts GetForecastByID(int id);

        public vContractList GetForecastByIDView(int id);

        public bool UpdateForecast(Model.Contracts forecast);


        public int CreateMilestonesItem(MilestonesItem milestones);

        public bool CreateMilestonesValue(MilestonesValue milestones);

        List<string> GetMilestonesNames(int contractID);





    }
}
