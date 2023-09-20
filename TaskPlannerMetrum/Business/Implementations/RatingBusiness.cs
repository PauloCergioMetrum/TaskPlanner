using TaskPlannerMetrum.Model;
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


        public dynamic GetAllRatingProject(int projectID)
        {
            return _repository.GetAllRatingProject(projectID);  
        }


        public object UpdateRating(RatingDTOAll ratings)
        {
            return _repository.UpdateRating(ratings);
        }



    }
}
