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


        public dynamic GetAllRatingProjectExecutor(int MilestonesID, int UserID ,int ContractID)
        {
            var UsersTasks = _activityPlanRepository.FindAllTaskByProject(MilestonesID, ContractID).ToList();
            foreach (var user in UsersTasks)
            {
                bool isTechLeader = _repository.IsTechLeader(user.ExecutorTeamID, MilestonesID);
                _repository.CreateRatingProjects(user.ExecutorTeamID, MilestonesID);
                _repository.CreateRatings(isTechLeader, user.ExecutorTeamID, MilestonesID);
            }
            return _repository.GetAllRatingProject(MilestonesID, UserID);
        }

        public dynamic GetAllRatingProjectExecutor(int projectID, int userID)
        {
            throw new System.NotImplementedException();
        }

        public dynamic GetAllRatingProjectLeader(int MilestonesID, int userID ,int ContractID)
        {

            _repository.ExistLeaderID(MilestonesID);

            var UsersTasks = _activityPlanRepository.FindAllTaskByProject(MilestonesID , ContractID).ToList();


            foreach (var user in UsersTasks)
            {
                bool isTechLeader = _repository.IsTechLeader(user.ExecutorTeamID, MilestonesID);
                _repository.CreateRatingProjects(user.ExecutorTeamID, MilestonesID);
                _repository.CreateRatings(isTechLeader, user.ExecutorTeamID, MilestonesID);
            }
            return _repository.GetAllRatingProject(MilestonesID, userID);
        }

        public dynamic GetAllRatingProjectLeader(int projectID, int userID)
        {
            throw new System.NotImplementedException();
        }

        public dynamic GetFiscalRatingProject(int contractID)
        {
            _repository.UpdateManager(contractID);
            return _repository.GetRatingManager(contractID);
        }

        public void RatingRetroactive(int ContractID ,int MilestonesID)
        {

            //  BUSCAR NA TABELA DEPARTMENTPROJECTS TODOS OS TECHLEADER DO CONTRACTID (ID DO CONTRATO)
            _repository.ExistLeaderID(ContractID);

            var usersListIDs = _activityPlanRepository.FindAllTaskByProject(MilestonesID, ContractID).Select(s => s.ExecutorTeamID).Distinct().ToList();
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
