using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ProjectExecutiveReportDto
    {
        public List<GetProjectExecutiveStatusResultDto> GetProjectExecutiveStatus { get; set; }
        public List<GetTableProjectExecutiveReportDto> GetTableProjectExecutiveReport { get; set; }
    }
}
