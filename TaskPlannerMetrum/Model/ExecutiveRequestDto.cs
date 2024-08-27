using System;
namespace TaskPlannerMetrum.Model
{
    public class ExecutiveRequestDto
    {
        public string InspectorIDs { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Closed { get; set; }
        public bool? Open { get; set; }
    }
}
