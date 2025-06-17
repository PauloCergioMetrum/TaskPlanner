using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPlannerMetrum.Model
{
    public class MilestoneStatusManual
    {
        [Key]
   
        public int ContractID { get; set; }

      
        public int MilestoneID { get; set; }

        public bool IsFinalized { get; set; }

        public DateTime? FinalizedDate { get; set; }


        public string FinalizedBy { get; set; }
    }
}
