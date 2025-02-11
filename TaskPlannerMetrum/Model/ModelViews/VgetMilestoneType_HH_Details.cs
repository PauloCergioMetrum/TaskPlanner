using System;
using System.Text.Json.Serialization;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class VgetMilestoneType_HH_Details
    {
        public int? ID { get; set; }
        public int? ContractID { get; set; }
        
        public int? ExecutorTeamID { get; set; }
        public double? ExecutedManHour { get; set; }
        public int? DepartamentID { get; set; }       
        public string? DepartmentName {  get; set; }
        public string? UserName { get; set; }

        public int? MilestonesID { get; set; } 
        public int? TeamUserID { get; set; } 
        public double ?HourCost { get; set; }
        public int? UHC_UserID { get; set; } 
        public string ?FunctionName { get; set; }
        public DateTime ?UHC_CreationDate { get; set; }
    }
}
