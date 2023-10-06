namespace TaskPlannerMetrum.Business
{
    public interface IRatingBusiness
    {

        public dynamic GetAllRatingProject(int projectID);

        public dynamic UpdateRating(Model.RatingDTOAll ratings);

        public void RatingRetroactive(int ContractID);
    }


}
