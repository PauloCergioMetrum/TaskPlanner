using System;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPMAcquisitionCost
    {
        public string ID { get; set; }
        //public int? StatusAcquistionID { get; set; }
        public double Amount { get; set; }
        public string AquisitionPlannedID { get; set; }
        public double? Value { get; set; }
        public DateTime DateAcquisition { get; set; }
        public DateTime DateAcquisitionDelivery { get; set; }
        public double TotalMade { get; set; }
        public string Description {  get; set; }    
     

    }
}
