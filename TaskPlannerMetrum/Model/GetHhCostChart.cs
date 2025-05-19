using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class GetHhCostChart
    {
        [Key]
        public double TotalPlannedManHour { get; set; }
        public double TotalExecutedManHour { get; set; }
        public double TotalPlannedCost { get; set; }
        public double TotalExecutedCost { get; set; }

        public string MilestonesName { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? RescheduledDate { get; set; }
        public DateTime? ExecutedDate { get; set; }
    }
}
