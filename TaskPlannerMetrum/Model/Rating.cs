using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }
        public string RatingProjectID { get; set; }    

        public int RatingDescriptionID { get; set; }
        public string Value { get; set;}
   

    }
}
