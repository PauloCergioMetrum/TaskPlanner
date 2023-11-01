using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }
        public int RatingProjectID { get; set; }    

        public int RatingDescriptionID { get; set; }
        public int Value { get; set;}





    }
}
