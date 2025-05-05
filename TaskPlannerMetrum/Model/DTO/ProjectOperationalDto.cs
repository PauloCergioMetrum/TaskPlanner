using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ProjectOperationalDto
    {
        [Key]
        public List<MonthlyContractDto> ContractsByMonth { get; set; }
        public List<ContractStatusSummaryDto> ContractStatusSummary { get; set; }
        public List<ProjectStatusTimelineDto> ProjectStatusTimeline { get; set; }
        public List<ExecutiveProjectReportDto> ExecutiveProjectReport { get; set; }
    }
}