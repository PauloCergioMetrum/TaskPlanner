using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using System;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vUsersView
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string UserEmail { get; set; }

        public string? PhoneNumber { get; set; }

        public int? DepartmentId { get; set; }
        public int? WorkspaceID { get; set; }

        public bool IsActive { get; set; }

        public int? functionID { get; set; }
        public string? FunctionHH { get; set; }
        public int? ManagementID { get; set; }
        public DateTime? CreationDate { get; set; }

        public string ManagementName { get; set; }

        public int?  role { get; set; }

        public string? PermissionName { get; set; }
        public string? DepartamentName { get; set; }

        public int? PermissionID { get; set; }



    }
}


 