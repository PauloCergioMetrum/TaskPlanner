using System;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Repository.ActiviesScope;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ActivesScopeBusinessImplementation : IActiviesScopeBusiness
    {
        private readonly IActiviesRepository _activiesRepository;

        public ActivesScopeBusinessImplementation(IActiviesRepository activiesRepository)
        {
            _activiesRepository = activiesRepository;
        }
        public dynamic GetActivesScopeByProject(string projectId)
        {
           //return _activiesRepository.GetTaskById(projectId);
            return _activiesRepository.GetActiviesScopeByProject(projectId).Select(a => new { a.ID, a.Description});

        }
        public dynamic GetTaskById(string ProectID)
        {
            return _activiesRepository.GetTaskById(ProectID);
        }

        public dynamic GetllTasks(int GrupId)
        {
            return _activiesRepository.GetAllTasks(GrupId);       
        }

        public dynamic GetTaskByGrup()
        {
            return _activiesRepository.GetTaskByGrup();
        }

        public dynamic GetUserforTask(int id)
        {
            throw new NotImplementedException();
        }
    }
}
