using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class HoursExecutor
    {
        public string Project { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string PlannedManHours { get; set; }
        public string Executor { get; set; }
        public string TotalManHour { get; set; }
        public string IsOverAllocated { get; set; }


    }
}
