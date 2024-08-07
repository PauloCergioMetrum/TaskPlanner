namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPmCostMade
    {
        public string Id { get; set; }

        public int Amount { get; set; }
        public double ValueUnit { get; set; }
        public string Description { get; set; }

        public string Pm_Cost_Planned_Id { get; set; }
        public double Total {  get; set; }      

    }
}
