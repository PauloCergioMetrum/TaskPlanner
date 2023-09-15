using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Model;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class RatingDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int ProjectID { get; set; }
        public bool TeckLeader { get; set; }
        public List<Rating> Rating { get; set; }
    }

    public class Rating
    {
        public string RatingName { get; set;}

        public string  UserName { get; set; }

        public int RatingValue { get; set; }
        
                             
    }

   
}



