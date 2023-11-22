using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;


namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vReports_PlannedExecuted
    {
        [Key]
        public int ActivitiesScopeListID { get; set; }
        public string Description { get; set; }
        public int inspectorID { get; set; }
        public string InternalCode { get; set; }
        public DateTime ScheduledDate { get; set; }
        public double PlannedManHour { get; set; }
        public double ExecutedManHour { get; set; }

        public string InspectorName { get; set; }
        public string ExecutorName { get; set; }

        public int ExecutorID { get; set; }





    }
}


