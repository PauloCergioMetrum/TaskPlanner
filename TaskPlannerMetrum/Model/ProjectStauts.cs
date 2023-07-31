using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using TaskPlannerMetrum.Model.Base;

namespace TaskPlannerMetrum.Model
{
    public class ProjectStauts
    {
        public string projectName { get; set; }

        public string clientName { get; set; }
        public  int plannedManHour { get; set; }
        public DateTime startDate { get; set; }
        public int projectID { get; set; }
        public string full_name{ get; set; }

        public int executedManHour { get; set; }

        public int expectedManHour { get; set; }
        public DateTime contractDate { get; set; }

        public DateTime endDate { get; set; }

        public string status { get; set; }
    }
}
