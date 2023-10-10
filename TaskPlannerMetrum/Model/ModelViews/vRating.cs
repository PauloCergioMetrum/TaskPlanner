using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TaskPlannerMetrum.Model.Base;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vRating

    {
        public string RatingName { get; set; }
        public string Type { get; set; }
        public int RatingProjectID { get; set; }
        public int RatingDescriptionID { get; set; }
        public int RatingValue { get; set; }

        public string UserName { get; set; }

        public int ProjectID { get; set; }
        public int UserID { get; set; }

        [Key]
        public int RatingID { get; set; }
    }
}
