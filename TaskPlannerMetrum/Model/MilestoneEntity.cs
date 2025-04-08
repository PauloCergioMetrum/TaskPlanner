using System;

namespace TaskPlannerMetrum.Model
{
    public class MilestoneEntity
    {
        public int ContractID { get; set; }
        public int MilestonesID { get; set; }
        public string MilestoneName { get; set; }
        public int? TechLeadID { get; set; }
        public string TechLeadName { get; set; }
        public int? BusinessUnitID { get; set; }
        public string BusinessUnit { get; set; }

        public int? Delayed { get; set; }   

        public DateTime? ScheduledDate { get; set; }
        public DateTime? RescheduledDate { get; set; }

        public double? ExpectedHours { get; set; }
        public double? TotalPlannedManHour { get; set; }
        public double? TotalExecutedManHour { get; set; }
        public DateTime? DateRetroactive { get; internal set; }
        public int? Status { get; set; }

    }
}
