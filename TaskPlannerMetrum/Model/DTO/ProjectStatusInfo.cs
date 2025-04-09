using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ProjectStatusInfo
    {
        public int id { get; set; }
        public string InternalCode { get; set; }
        public int Status { get; set; }
        public DateTime? DateRetroactive { get; set; }
        public bool IsBlocked { get; set; }
    }
}

