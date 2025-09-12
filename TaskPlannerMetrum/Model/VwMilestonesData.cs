using Microsoft.EntityFrameworkCore;

namespace TaskPlannerMetrum.Model
{
    [Keyless]
    public class VwMilestonesData
    {
        public int ContractID { get; set; }
        public int MilestonesID { get; set; }
        public string MilestonesName { get; set; } = string.Empty;

        // Hours
        public double ExpectedHours { get; set; }
        public double PlannedHours { get; set; }
        public double ExecutedHours { get; set; }

        // Costs
        public double ExpectedCost { get; set; }
        public decimal PlannedCost { get; set; }
        public decimal ExecutedCost { get; set; }

        public string CalculatedMilestoneStatus { get; set; } = string.Empty;
    }


}

