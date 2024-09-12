using System;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPM_SummaryPlannedData
    {
        public int Id { get; set; }
        // public string ID { get; set; } 
        //public double ValueUnit { get; set; }
        //public int Amount { get; set; }
        // public double TypeID { get; set; }
        //public string OutsourceDescription { get; set; }
        public int ContractID { get; set; }
        //public bool Subcontracting { get; set; }
        //public int TypeAcquisitionID { get; set; }
        public int AmountPlanned { get; set; }
        public string AcquisitionDescription { get; set; }
        public double AmountValue { get; set; }
        public double TotalCost { get; set; }
        public double CountMobilization { get; set; }
        public double CountAccommodation { get; set; }
        public double CountFood { get; set; }
        public double CountAirTransport { get; set; }
        public double CountGroundTransport { get; set; }
        public double CountOthers { get; set; }
        public double TotalMobilizationCount { get; set; }
        public int AmountPlannedSum { get; set; }
        public int AmountSum { get; set; }
        public string TypeAcquisitionName { get; set; }
    }
}
