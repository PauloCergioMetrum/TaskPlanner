using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class GetMilestoneFullReportByContract
    {

        [Key]
        public int ContractID { get; set; }
        public int MilestonesID { get; set; }
        public string MilestonesName { get; set; }

        public double PlannedHours { get; set; }
        public double ExecutedHours { get; set; }

        public double PlannedCost { get; set; }
        public double ExecutedCost { get; set; }

        public double FixedPlannedHours { get; set; }
        public double FixedEstimatedCost { get; set; }
    }
}



