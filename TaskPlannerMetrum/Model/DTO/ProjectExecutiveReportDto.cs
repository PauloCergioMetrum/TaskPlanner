using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ProjectExecutiveReportDto
    {
        public List<GetProjectExecutiveStatusResultDto> GetProjectExecutiveStatus { get; set; }
        public List<GetTableProjectExecutiveReportDto> GetTableProjectExecutiveReport { get; set; }
        public List<GetExecutiveProjecStatusBusinnesUnitDto> GetExecutiveProjecStatusBusinnesUnit { get; set; }
        public List<GetExecutiveProjectStatusPeriodDto> GetExecutiveProjectStatusPeriod { get; set; }

    }
}
