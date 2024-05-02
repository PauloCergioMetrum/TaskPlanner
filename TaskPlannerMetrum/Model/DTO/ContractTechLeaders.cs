using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ContractTechLeaders
    {
        [Key]
        public int ID { get; set; }
        public int ContractID { get; set; }
        public int TechLeaderID { get; set; }
    }
}
