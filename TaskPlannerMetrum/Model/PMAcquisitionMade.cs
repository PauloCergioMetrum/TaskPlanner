using System;

namespace TaskPlannerMetrum.Model
{
    public class PMAcquisitionMade
    {
        public string ID { get; set; }
        public int StatusAcquistionID { get; set; }
        public int Amount { get; set; }
        public string AquisitionPlannedID { get; set; }
        public double Value { get; set; }
        public DateTime DateAcquisition { get; set; }
        public DateTime DateAcquisitionDelivery { get; set; }
        public double TotalMade { get; set; }

    }
}
