using System;

namespace TaskPlannerMetrum.Model
{
    public class GetFinanceMilestones
    {
        public string MilestonesName { get; set; }
        public int MilestonesID { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? RescheduledDate { get; set; }
        public DateTime? ExecutedDate { get; set; }
        public string Description { get; set; }
        public int? MilestonesTypeID { get; set; }
        public int? Baseline { get; set; }
        public string? ID { get; set; }
        public double? Value { get; set; }
        public int ContractID { get; set; }
    }
}
