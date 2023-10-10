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


        public dynamic GetAllRatingProjectExecutor(int projectID, int UserID)
        {
            var UsersTasks = _activityPlanRepository.FindAllTaskByProject(projectID.ToString()).ToList();
            foreach (var user in UsersTasks)
            {
                bool isTechLeader = _repository.IsTechLeader(user.UserID, projectID);
                _repository.CreateRatingProjects(user.UserID, projectID);
                _repository.CreateRatings(isTechLeader, user.UserID, projectID);
            }
            return _repository.GetAllRatingProject(projectID, UserID);
        }

        public dynamic GetAllRatingProjectLeader(int projectID, int userID)
        {

            _repository.ExistLeaderID(projectID);

            var UsersTasks = _activityPlanRepository.FindAllTaskByProject(projectID.ToString()).ToList();


            foreach (var user in UsersTasks)
            {
                bool isTechLeader = _repository.IsTechLeader(user.UserID, projectID);
                _repository.CreateRatingProjects(user.UserID, projectID);
                _repository.CreateRatings(isTechLeader, user.UserID, projectID);
            }
            return _repository.GetAllRatingProject(projectID, userID);
        }

        public dynamic GetFiscalRatingProject(int contractID)
        {
            _repository.UpdateManager(contractID);
            return _repository.GetRatingManager(contractID);
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
