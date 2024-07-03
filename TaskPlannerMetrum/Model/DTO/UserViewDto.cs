using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class UserViewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartamentName { get; set; }
        public string Role { get; set; }
        public int? PermissionId { get; set; }
        public string PhoneNumber { get; set; }
        public int? WorkspaceID { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? PermissionName { get; set; }
        public int? ManagementID { get; set; }
        public string ManagementName { get; set; }

    }
}
