using DocumentFormat.OpenXml.Office.CoverPageProps;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ProjectOperational
    {
       public List<NumberOfContractsForBusinessUnit> ContractsForBusinessUnit { get; set; }

       public List<StatusForPeriod> StatusForPeriod { get; set; } 
        
        public List<PreparetBalancePerProject> BalancePerProject { get; set; }  

        public List<OperationalRelationshipTable> OperationalRelationshipTable { get; set; }

        public List<GetContractsByMonthDto> ContractsByMonth { get; set; }
        public List<GetContractsStatusByMonthDTO> ContractsStatusByMonth { get; set; }
        public List<GetProjectStatusPeriodDTO> ProjectStatusPeriod { get; internal set; }
        public List<GetTableProjectExecutiveReportDTO> TableProjectExecutiveReport { get; internal set; }
    }
}
