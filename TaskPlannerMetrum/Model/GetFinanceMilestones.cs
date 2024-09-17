using System;

namespace TaskPlannerMetrum.Model
{
    public class GetFinanceMilestones
    {
        public string MilestonesName { get; set; }
        public int MilestonesID { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? RescheduledDate { get; set; }
        public DateTime? ExecutedDate { get; set; }
        public string Description { get; set; }
        public int? MilestonesTypeID { get; set; }
        public int? Baseline { get; set; }
        public Guid ID { get; set; }

        public double? Value { get; set; }
        public int ContractID { get; set; }

        public string? TypeMilestonesID { get; set; }    
        public int? TechLeadID {  get; set; } 
        public int? BusinessUnitID { get; set; }
        public int TaskCount { get; set; }




    }
}
