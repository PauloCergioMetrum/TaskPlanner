using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vMilestoneStatusCalculation
    {

        [Key]
        [Column("ContractID")]
        public int ContractId { get; set; }

        [Column("MilestonesID")]
        public int MilestoneId { get; set; }

        [Column("MilestonesName")]
        public string MilestoneName { get; set; }

        [Column("ScheduledDate")]
        public DateTime? ScheduledDate { get; set; }

        [Column("RescheduledDate")]
        public DateTime? RescheduledDate { get; set; }

        [Column("ExecutedDate")]
        public DateTime? ExecutedDate { get; set; }

        [Column("EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [Column("CalculatedMilestoneStatus")]
        public string CalculatedMilestoneStatus { get; set; }
    }
}
