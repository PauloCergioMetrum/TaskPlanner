using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Repository.Rating
{
    public interface IRatingRepository 
    {

        public dynamic GetAllRatingProject(int projectID, int? userID);


        public bool UpdateRating(Model.RatingDTOAll reatings);

        public bool IsTechLeader(int userID , int contractId   );

        public void CreateRatings(bool techLeader, int userID, int contractID);

        public void CreateRatingProjects(int userId, int contractId );

        public bool ExistRating(int userID, int contractIdeaderID);

        public bool ExistLeaderID(int contractID);


        public void  UpdateTechLeader(int contractID);


        public void UpdateManager(int contractID);


        public dynamic GetRatingManager( int contractID);







    }
}
