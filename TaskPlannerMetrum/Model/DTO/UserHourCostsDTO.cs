using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class UserHourCostsDTO
    {
        public string ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double HourCost { get; set; }
        public int UserID { get; set; }

        public string FunctionName { get; set; }
    }
}
