using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class UserProjects
    {
        [Key]
        public int ID { get; set; }
        public int ContractID { get; set; }
        public int UserID { get; set; }
    }
}
