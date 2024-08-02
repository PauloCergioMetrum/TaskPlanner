using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vActivityPlan
    {
        [Key]
        public int ID { get; set; }

        public int ContractID { get; set; }
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public int ActivitiesScopeListID { get; set; }

        public DateTime ScheduledDate { get; set; }

        public double PlannedManHour { get; set; }

        public int PlannerTeamID { get; set; }

        public string NotesFromPlanner { get; set; }

        public int ExecutorTeamID { get; set; }

        public string Status { get; set; }

        public double ExecutedManHour { get; set; }

        public string NotesFromExecutor { get; set; }

        public int UserID { get; set; }

        public string ExecutorName { get; set; }

        public string statusName { get; set; }

        public bool? IsRework { get; set; }

        public string? TaskDescription { get; set; }

        public string? BusinessUnit {  get; set; }
        public int? MilestonesID { get; set; }
        public string ? MilestoneName {  get; set; }    

        public int ?EquipmentID { get; set; }    
    }
}
