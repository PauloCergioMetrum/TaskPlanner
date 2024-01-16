using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public interface IProjectManagementRepository
    {

        public Model.Contracts GetForecastByID(int id);

        public bool UpdateForecast(Model.Contracts forecast);

       

    }
}
