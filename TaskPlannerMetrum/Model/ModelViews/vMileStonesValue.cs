using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vMileStonesValue
    {
#nullable enable
        public int ContractID { get; set; }
        public string? MilestonesName { get; set; }
        public int MilestonesID { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime RescheduledDate { get; set; }
        public DateTime? ExecutedDate { get; set; }
        public string? Description { get; set; }
        public int MilestonesTypeID { get; set; }
        public int Baseline { get; set; }
        public string ID { get; set; } = string.Empty;
        public  double Value { get; set; }
        public int TechLeadID { get; set; }
        public string? TechLeaderName { get; set; }
        public string? BusinessName { get; set; }
        public string? TypeMilestonesID { get; set; }
        public int BusinessUnitID { get; set; }
        public string? FinanceBusinessUnit { get; set; }
        public string? Status { get;set; }

    }
}
