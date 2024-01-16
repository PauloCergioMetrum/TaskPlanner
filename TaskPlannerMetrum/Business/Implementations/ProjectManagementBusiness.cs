using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.ProjectManagement;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ProjectManagementBusiness: IProjectManagementBusiness
    {
        private readonly IProjectManagementRepository _projectmanagementRepository;

        public ProjectManagementBusiness (IProjectManagementRepository projectmanagementBusiness)
        {
            _projectmanagementRepository = projectmanagementBusiness;
        }
   
        public bool UpdateForecast(ProjectManagementDTO forecast)
        {
            try
            {
                var UpdateForecast = _projectmanagementRepository.GetForecastID(forecast.id);
                UpdateForecast.ValidityEndDate = forecast.ValidityEndDate;
                UpdateForecast.ValidityStartDate = forecast.ValidityEndDate;
                UpdateForecast.PredictedMarkup = forecast.PredictedMarkup;
                UpdateForecast.PredictedSavings = forecast.PredictedSavings;
                return _projectmanagementRepository.UpdateForecast(UpdateForecast);
            }
            catch
            {
                return false;
            }


        }

        
    }
}
