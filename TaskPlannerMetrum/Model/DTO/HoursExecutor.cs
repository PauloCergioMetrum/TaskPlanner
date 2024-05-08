using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class HoursExecutor
    {    
        public string Project { get; set; }
        public DateTime ScheduledDate { get; set; }
        public double PlannedManHour { get; set; }
        public string Executor { get; set; }
        public double TotalManHour { get; set; }    


    }
}
