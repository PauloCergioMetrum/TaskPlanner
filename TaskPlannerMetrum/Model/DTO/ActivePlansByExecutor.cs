using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ActivePlansByExecutor
    {
        public int ActivitiesScopeListID { get; set; }
        public string Description { get; set; }
        public double ExecutedManHour { get; set; }
        public string ClientName { get; set; }
        public string ExecutorName { get; set; }
        public int UserID { get; set; }
        public string NotesFromExecutor { get; set; }
        public string NotesFromPlanner { get; set; }
        public string TaskDescription { get; set; }
        public double PlannedManHour { get; set; }
        public int PlannerTeamID { get; set; }
        public int ContractID { get; set; }
        public string ProjectName { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; }
        public int ID { get; set; }
        public string StatusName { get; set; }
        public bool IsRework { get; set; }
        public string BusinessUnit { get; set; }
     

    }
}
