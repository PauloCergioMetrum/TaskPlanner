namespace TaskPlannerMetrum.Repository.Rating
{
    public interface IRatingRepository 
    {

        public dynamic GetAllRatingProject(int projectID);


        public bool UpdateRating(Model.RatingDTOAll reatings);



    }
}
