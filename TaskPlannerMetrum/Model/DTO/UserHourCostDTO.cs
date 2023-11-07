using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class UserHourCostDTO
    {
        public int ID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double HourCost { get; set; }
        public int UserID { get; set; }
    }
}
