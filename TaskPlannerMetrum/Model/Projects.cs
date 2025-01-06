using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using TaskPlannerMetrum.Model.Base;

namespace TaskPlannerMetrum.Model
{
#nullable enable
    public class Projects 
    {
       
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? InternalCode { get; set; }
        public int? ClientID { get; set; }
        public int? WorkspaceID { get; set; }
        public double? PlannedManHour { get; set; }
        public double? ExecutedManHour { get; set; }
        public DateTime? StartDate { get; set; }
        public double? ExpectedManHor { get; set; }
        public DateTime? ContractEndDate { get; set;}
        public DateTime? EndDate { get; set;}
        public int? PMTeamID { get; set; }

        public string? Status { get; set; }      
        public int? TechLeaderID { get;set; }
        public int? DepartamentID { get; set; }

  

    }
}
