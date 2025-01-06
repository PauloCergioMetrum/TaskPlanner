using System;

namespace TaskPlannerMetrum.Model.DTO
{
#nullable enable
    public class OrderInformationDTO
    {
        public string? Client { get; set; }

        public string? SalesOrder { get; set; }

        public string? BusinessUnit { get; set; }

        public string? Validity { get; set; }

        public string? PredictedSavings { get; set; }
        public string? PredictedMarkup { get; set; }
        public DateTime? ValidityStartDate { get; set; }
        public DateTime? ValidityEndDate { get; set; }
        public string? WorkspaceName { get; set; }







    }
}
