using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

 namespace TaskPlannerMetrum.Model
{
    public class ActivePlanList
    {
        [Key]
        public int ID { get; set; }

        public int WorkspaceID { get; set; }

        public int ContractID { get; set; }


        public int ActivitiesScopeListID { get; set; }

        public DateTime ScheduledDate { get; set; }

        public double PlannedManHour { get; set; }

        public int PlannerTeamID { get; set; }

        public string NotesFromPlanner { get; set; }

        public List<int> ExecutorTeamID { get; set; }

        public char Status { get; set; }

        public int ExecutedManHour { get; set; }

        public string NotesFromExecutor { get; set; }

        public bool? IsRework { get; set; }

        public int DepartamentID { get; set; }

        public string TaskDescription { get; set; }

        public string BusinessUnit {  get; set; }

    }

}
