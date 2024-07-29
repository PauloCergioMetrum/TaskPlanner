namespace TaskPlannerMetrum.Model
{
    public class BillingPerBusinessUnit
    {

        public string MonthYear { get; set; }

        public string BusinessUnit { get; set; }

        public double ValueExpectedInvoice { get; set; }
        public double ValueInvoice { get; set; }
    }
}
