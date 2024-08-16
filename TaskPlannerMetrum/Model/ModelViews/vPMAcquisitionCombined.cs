namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPMAcquisitionCombined
    {
        public string ID { get; set; }
        public int TypeAcquisitionID { get; set; }
        public int AmountPlanned { get; set; }
        public string Description { get; set; }
        public int ContractID { get; set; }
        public double AmountValue { get; set; }
        public double TotalCost { get; set; }
        public double PredictedTotal { get; set; }
        public double Difference {  get; set; }
        public double CombinedTotal {  get; set; }  
    }
}
