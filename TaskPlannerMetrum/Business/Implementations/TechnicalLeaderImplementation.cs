using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.ActivityPlan;
using TaskPlannerMetrum.Repository.Calendar;
using TaskPlannerMetrum.Repository.Rating;
using TaskPlannerMetrum.Repository.ReportsViewer;
using TaskPlannerMetrum.Repository.TechnicialLeader;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class TechnicalLeaderImplementation : ITechnicalLeaderBusiness
    {

        private readonly ITechnicalLeaderRepository _repository;
        public TechnicalLeaderImplementation(ITechnicalLeaderRepository repository)
        {
            _repository = repository;
        }
        public List<UserDto> GetAllTechnicalLeader()
        {
            return _repository.GetAllTechnicalLeader();
        }


    }
}