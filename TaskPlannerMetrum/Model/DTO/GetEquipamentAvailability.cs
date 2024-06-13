using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetEquipamentAvailability
    {
        public string EquipamentName { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string ProjectName { get; set; }
    }

}
