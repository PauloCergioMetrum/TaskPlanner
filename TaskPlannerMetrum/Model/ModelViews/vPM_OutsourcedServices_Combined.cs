using TaskPlannerMetrum.Business;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPM_OutsourcedServices_Combined
    {
        public string ID { get; set; }
        public double ValueUnit { get; set; }
        public int Amount { get; set; }
        public double? TypeID { get; set; }
        public string Description { get; set; }
        public int ContractID { get; set; }
        public bool ? Subcontracting { get; set; }
        public double TotalOutsourcedServices_Planned { get; set; }
        public double TotalOutsourcedServices_Made { get; set; }
        public double Difference { get; set; }      



    }
}
