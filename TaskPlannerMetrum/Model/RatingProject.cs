using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class RatingProject
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; } 
        
       public int ProjectID { get; set; }
    }
}
