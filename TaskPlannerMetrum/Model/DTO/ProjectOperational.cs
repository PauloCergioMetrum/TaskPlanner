using DocumentFormat.OpenXml.Office.CoverPageProps;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ProjectOperational
    {
       

        public List<GetContractsByMonthDto> ContractsByMonth { get; set; }
        public List<GetContractsStatusByMonthDTO> ContractsStatusByMonth { get; set; }
        public List<GetExecutiveProjectStatusPeriodDto> ProjectStatusPeriod { get; internal set; }
     

    }
}
