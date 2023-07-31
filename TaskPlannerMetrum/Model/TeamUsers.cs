using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Policy;
using TaskPlannerMetrum.Model.Base;

namespace TaskPlannerMetrum.Model
{
    public class TeamUsers : BaseEntity
    {
 

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string UserEmail { get; set; }

        public string PhoneNumber { get; set; }
        public int WorkspaceID { get; set; }
        public int DepartmentId { get; set; }
        public int PermissionId { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? CreationDate { get; set; }

  
        public string SeniorityLevel { get; set; }

        public string WorkForceClass { get; set; }
        public string WorkForceType { get; set; }

        public decimal HoursAvailability { get; set; }

        public decimal ManHourCost { get; set; }

        public int UserID { get; set; }

        public bool isLeader { get; set; }
        public string DepartamentName { get; set; }

        public bool IsActive { get; set; }



    }
}
