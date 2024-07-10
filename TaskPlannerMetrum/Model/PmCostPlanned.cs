namespace TaskPlannerMetrum.Model
{
    public class PmCostPlanned
    {
        public string Id { get; set; }
        public int TypeID {  get; set; }     
        public int Amount { get; set; }
        public double ValueUnit { get; set; }
        public string Description { get; set; }
        public int ContractID { get; set; }

        


    }
}
