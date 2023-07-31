using TaskPlannerMetrum.Repository.DepartmentProjects;
using TaskPlannerMetrum.Repository.ProjectProgress;
using TaskPlannerMetrum.Repository.Projects;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class DashboardBusinessImplementation : IDashboardBusiness
    {
        private readonly IDashboardRepository _projectRepository;

        public DashboardBusinessImplementation(IDashboardRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public dynamic GetStatus()
        {
            return _projectRepository.GetAllStatus();  
        }
    }
}
