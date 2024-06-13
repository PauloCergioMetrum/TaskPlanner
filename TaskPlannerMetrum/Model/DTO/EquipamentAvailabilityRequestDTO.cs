using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class EquipamentAvailabilityRequestDTO
    {
        public int EquipamentID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
