using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class RatingDescription
    {
        [Key]
        public int ID { get; set; }
        public string  Description { get; set; }
        public string Type { get; set; }   
    }
}
