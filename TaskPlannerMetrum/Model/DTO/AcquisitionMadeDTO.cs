using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class AcquisitionMadeDTO
    {
        public int ID { get; set; }
        public int StatusAcquistionID { get; set; }
        public int Amount { get; set; }
        public int AquisitionPlannedID { get; set; }
        public int Value { get; set; }
        public DateTime DateAcquisition { get; set; }
        public DateTime DateAcquisitionDelivery { get; set; }
    }
}
