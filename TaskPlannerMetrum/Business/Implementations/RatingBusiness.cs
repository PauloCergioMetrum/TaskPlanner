using System.Diagnostics.Contracts;
using System.Linq;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Repository.ActivityPlan;
using TaskPlannerMetrum.Repository.Rating;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class RatingBusiness : IRatingBusiness
    {

        private readonly IRatingRepository _repository;

        private readonly IActivityPlanRepository _activityPlanRepository;

        public RatingBusiness(IRatingRepository repository, IActivityPlanRepository activityPlanRepository)
        {
            _repository = repository;
            _activityPlanRepository = activityPlanRepository;
        }


        public dynamic GetAllRatingProject(int projectID)
        {
            return _repository.GetAllRatingProject(projectID);
        }

        public void RatingRetroactive(int ContractID)
        {

            //  BUSCAR NA TABELA DEPARTMENTPROJECTS TODOS OS TECHLEADER DO CONTRACTID (ID DO CONTRATO)
            _repository.ExistLeaderID(ContractID);

            var usersListIDs = _activityPlanRepository.FindAllTaskByProject(ContractID.ToString()).Select(s => s.UserID).Distinct().ToList();
            foreach (var userid in usersListIDs)
            {
                if (_repository.ExistRating(userid, ContractID) == false)
                {
                    var TechLeader = _repository.IsTechLeader(userid, ContractID);
                    _repository.CreateRatingProjects(userid, ContractID);
                    _repository.CreateRatings(TechLeader, userid, ContractID);
                }
            }



        }

        public object UpdateRating(RatingDTOAll ratings)
        {
            return _repository.UpdateRating(ratings);
        }



    }
}
