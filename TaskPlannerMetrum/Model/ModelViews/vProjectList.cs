using Microsoft.Extensions.Primitives;
using System;
using System.ComponentModel.DataAnnotations;


namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vProjectList
    {
        [Key]
        public int ProjectID { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public double? ExecutedManHour { get; set; }
        public double? PlannedManHour { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? ExpectedManHor { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int PMTeamID { get; set; }
        
        public string full_name { get; set; }

        public string Status { get; set; }

        public int TechLeaderID { get; set; }

        public string TechLeaderName { get; set; }





    } 
}
