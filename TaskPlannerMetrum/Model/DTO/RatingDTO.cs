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

        public int RatingID { get; set; }

        public List<RatingModel> Rating { get; set; }
    }

    public class RatingModel
    {
        public string RatingName { get; set;}

       

        public int RatingValue { get; set; }

        public int RatingDescriptionID { get; set; }   
        
                             
    }

   
}



