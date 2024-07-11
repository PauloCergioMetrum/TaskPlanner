using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPlannerMetrum.Model
{
    public class PMAcquisitionPlanned
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        public int TypeAcquisitionID { get; set; }

        public int? AmountPlanned { get; set; }

        [MaxLength]
        public string Description { get; set; }

        [Required]
        public int ContractID { get; set; }

        public double? AmountValue { get; set; }

        [Required]
        public double TotalCost { get; set; } 

      
    }
}
