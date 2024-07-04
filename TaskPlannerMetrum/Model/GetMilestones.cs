namespace TaskPlannerMetrum.Model
{
    public class GetMilestones
    {
        public int ID { get; set; }
        public int? ContractID { get; set; }
        public string Name { get; set; }
        public bool? IsDefault { get; set; }
    }
}
