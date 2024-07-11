using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using TaskPlannerMetrum.Model.Base;
using Castle.Components.DictionaryAdapter;
using System.Text.Json.Serialization;

namespace TaskPlannerMetrum.Model
{
    [Table("users")]
    public class User : BaseEntity
    {

    
        [Column("user_name")]
        public string UserName { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("user_email")]
        public string UserEmail { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("department_id")]
        public int DepartmentId { get; set; }
        [Column("WorkspaceID")]
        public int WorkspaceID { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("refresh_token")]
        public string RefreshToken { get; set; }

        [Column("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

        [Column("CreationDate")]
        public DateTime? CreationDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDarkMode { get; set; }

        public int? ManagementID { get; set; }

        public int? FunctionID { get; set; }

    }
}
