using System.Collections.Generic;
using System;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Model
{
    public class ExecutorHourModel
    {
        public string Executor { get; set; }
        public DateTime ScheduledDate { get; set; }
        public List<HoursExecutor> Hours { get; set; }
        public string TotalManHour { get; set; }
    }
}
