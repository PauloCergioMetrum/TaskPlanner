using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class HoursCostModel
    {
        public int ContractID { get; set; }

        public int UserID { get; set; }

        public int ActivityID { get; set; }

        public DateTime ScheduleDate { get; set; }

        public double DayCost { get; set; }
    }
}
