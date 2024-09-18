using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class MilestoneDetailDTO
    {
        public int ContractID { get; set; }
        public int MilestonesID { get; set; }
        public string MilestoneName { get; set; }
        public int? TechLeadID { get; set; }
        public string TechLeadName { get; set; }
        public int? BusinessUnitID { get; set; }
        public string BusinessUnit { get; set; }
        public int? Delayed { get; set; }
        public DateTime? ScheduledDate {  get; set; }    
        public DateTime? RescheduledDate { get; set; } 
    }


}
