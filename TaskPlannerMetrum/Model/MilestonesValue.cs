using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
#nullable enable
    public class MilestonesValue
    {
        [Key]
        public string ID { get; set; } = string.Empty;
        public int MilestonesID { get; set; } 
        public int? Baseline { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? RescheduledDate { get; set; }
        public DateTime? ExecutedDate { get; set; }
        public string? Description { get; set; } 
        public int? TypeID { get; set; }
        public double? Value { get; set; }
        public string? TypeMilestonesID { get; set; }   
        public int? TechLeadID {  get; set; }   
        public  int? BusinessUnitID {  get; set; }   
       


    }
}
