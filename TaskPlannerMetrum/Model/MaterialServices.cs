namespace TaskPlannerMetrum.Model
#nullable enable

{
    public class MaterialServices
    {
        public string? MonthYear { get; set; }

        public double? InvoicedMaterial { get; set; }

        public double? InvoicedService { get; set; }
        public double? ExpectedMaterial { get; set; }

        public double? ExpectedService { get; set; }
    }
}
