using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public interface IProjectManagementRepository
    {

        public Model.Contracts GetForecastByID(int id);

        public vContractList GetForecastByIDView(int id);

        public bool UpdateForecast(Model.Contracts forecast);

       

    }
}
