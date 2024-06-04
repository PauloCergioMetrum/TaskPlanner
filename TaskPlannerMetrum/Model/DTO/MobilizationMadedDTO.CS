using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class MobilizationMadedDTO
    {
        public string ID { get; set; }
        public double CountMobilization { get; set; }
        public double CountAccommodation { get; set; }
        public double CountFood { get; set; }
        public double CountAirTransport { get; set; }
        public double CountGroundTransport { get; set; }
        public double CountOthers { get; set; }
        public string MobilizationPlannedID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Description { get; set; }
    }
}
