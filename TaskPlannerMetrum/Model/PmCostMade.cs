namespace TaskPlannerMetrum.Model
{
    public class PmCostMade
    {
        public string Id { get; set; }
        //public string TypeCostID { get; set; }
        public int Amount { get; set; }
        public double ValueUnit { get; set; }
        public string Description { get; set; }
        public string Pm_Cost_PlannedID { get; set; }
    }
}
