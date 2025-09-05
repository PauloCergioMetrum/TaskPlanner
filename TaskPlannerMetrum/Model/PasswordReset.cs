using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPlannerMetrum.Model
{
    public class PasswordReset
    {
        [Key]
        public int Id { get; set; }

        [Column("ChangedBy_Id")]
        public int ChangedById { get; set; }

        [Column("ChangedBy_FullName")]
        public string ChangedByFullName { get; set; } = null!;

        [Column("ChangedBy_Email")]
        public string ChangedByEmail { get; set; } = null!;

        [Column("ChangedBy_PermissionId")]
        public int ChangedByPermissionId { get; set; }

        [Column("TargetUser_Id")]
        public int TargetUserId { get; set; }

        [Column("TargetUser_FullName")]
        public string TargetUserFullName { get; set; } = null!;

        [Column("TargetUser_Email")]
        public string TargetUserEmail { get; set; } = null!;

        [Column("TargetUser_PermissionId")]
        public int TargetUserPermissionId { get; set; }

        [NotMapped]
        public string NewPasswordReset { get; set; } = null!;

        [Column("ChangedAt")]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}
