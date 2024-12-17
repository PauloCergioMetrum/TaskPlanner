namespace TaskPlannerMetrum.Model
{
    public class OutsourcedServiceDetailsResult
    {
        public string? ID { get; set; }
        public double? ValueUnit { get; set; }
        public double? Amount { get; set; }
        public double? TypeID { get; set; }
        public string? Description { get; set; }
        public string? Subcontracting { get; set; }
        public double? TotalOutsourcedServices_Planned { get; set; }
        public double? TotalOutsourcedServices_Made { get; set; }
        public double? Difference { get; set; }
    }
}
