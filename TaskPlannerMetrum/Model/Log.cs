using System;

namespace TaskPlannerMetrum.Model
{
    public class Log
    {
        public int LogId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public string Status { get; set; }
        public string SeverityLevel { get; set; }
        public string ErrorDetails { get; set; }
    }
}
