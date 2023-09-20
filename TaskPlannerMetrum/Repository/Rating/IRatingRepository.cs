namespace TaskPlannerMetrum.Repository.Rating
{
    public interface IRatingRepository 
    {

        public dynamic GetAllRatingProject(int projectID);


        public dynamic UpdateRating(Model.RatingDTOAll reatings);



    }
}
