namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vIndirectcostChart
    {
        public int ContractID { get; set; }
        public string PlannedId { get; set; }       
        public string MadeId { get; set; }          
        public int TypeID { get; set; }          
        public string TypeDescription { get; set; }
        public double PlannedTotal { get; set; }     
        public double MadeTotal { get; set; }
    }
}
