using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class HoursDTO
    {

        public string Executor { get; set; }
        public string TotalPlannedHours { get; set; }
    
        public DateTime ScheduledDate { get; set; }

        public List<HoursByDay> Hours { get; set; }

        public bool IsOverAllocated { get; set; }


        public class HoursByDay
        {
            public string PlannedManHours { get; set; }
            public string Project { get; set; }

            public bool IsOverAllocated { get; set; }

        }
    }
}
