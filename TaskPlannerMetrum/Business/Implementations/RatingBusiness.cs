using TaskPlannerMetrum.Repository.Rating;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class RatingBusiness : IRatingBusiness
    {

        private readonly IRatingRepository _repository;

        public RatingBusiness(IRatingRepository repository)
        {
            _repository = repository;   
        }

        public dynamic GetAllRatingforUser(int projectID)
        {
            return _repository.GetAllRatingforUser(projectID);  
        }
    }
}
