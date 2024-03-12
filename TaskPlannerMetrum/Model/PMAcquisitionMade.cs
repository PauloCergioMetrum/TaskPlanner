using System;

namespace TaskPlannerMetrum.Model
{
    public class PMAcquisitionMade
    {
        public int ID { get; set; }
        public int StatusAcquistionID { get; set; }
        public int Amount { get; set; }
        public int AquisitionPlannedID { get; set; }
        public double Value { get; set; }
        public DateTime DateAcquisition { get; set; }
        public DateTime DateAcquisitionDelivery { get; set; }

    }
}
