namespace TaskPlannerMetrum.Business
{
    public interface IRatingBusiness
    {

        public dynamic GetAllRatingProjectExecutor(int projectID, int userID);

        public dynamic GetAllRatingProjectLeader(int projectID, int userID);

        public dynamic UpdateRating(Model.RatingDTOAll ratings);

        public void RatingRetroactive(int ContractID);


        public dynamic GetFiscalRatingProject(int ContractID);
        
    }


}

