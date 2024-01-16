using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ProjectManagementDTO
    {
        public int id { get;set; }
        public double PredictedSavings { get; set; }

        public double PredictedMarkup { get; set; }

        public DateTime? ValidityStartDate { get; set; }

        public DateTime? ValidityEndDate { get; set; }
    }
}
