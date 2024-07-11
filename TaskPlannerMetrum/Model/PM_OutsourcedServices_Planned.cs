namespace TaskPlannerMetrum.Model
{
    public class PM_OutsourcedServices_Planned
    {
        public string ID { get; set; }
        public double ValueUnit { get; set; }
        public int  Amount { get; set; }
        public double TypeID { get; set; }
        public string Description { get; set; }
        public int ContractID { get; set; }

        public bool? Subcontracting { get; set; }
    }
}
