using System.ComponentModel.DataAnnotations;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Model
{
    public class PM_Information_General
    {
        public int ID { get; set; }

        public int Type { get; set; }
        public int ContractID {  get; set; }
        public string Name { get; set; }

        public string Role { get; set; }

        public string? Email { get; set; }

        public int? PhoneNumber { get; set; }

    }
}
