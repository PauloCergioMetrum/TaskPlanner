using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ReportsViewer
{
    public interface IReportsPlannedExecutedViewerRepository
    {
        public List<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate);
        public List<HoursCostModel> GetHourCost(DateTime startDate, DateTime endDate);
        public double GetHourExpectedHour(DateTime startDate, DateTime endDate, int contractID);



        public List<NumberOfContractsForBusinessUnit> CountContractsPerBusinessUnit(OperationalReportReportDTO operationalReportReportDTO);

        public List<StatusForPeriod> getStatusPerPeriod(OperationalReportReportDTO OperationalReportReportDTO);

        public List<BalancePerProject> GetBalancePerProject(OperationalReportReportDTO operationalReportReportDTO);

        public List<OperationalRelationshipTable> GetContractDetails(OperationalReportReportDTO OperationalReportReportDTO);

        public List<Model.ModelViews.BusinessUnit> GetAllBusinesUnit();

        public List<OptionsFilterTechLead> GetAllTechLeader();

        public List<OptionsListFilterProjectInspector> GetAllInspector();

        public List<PredictedInvoiced> GetMaterialAndService(ReportInvoice parameters);

        public List<MaterialServices> GetPredictedInvoicedReport(ReportInvoice parameters);

        public List<BillingPerBusinessUnit> GetBillingPerBusinessUnit(ReportInvoice filter);

        public List<ReportDetailsTable> GetReportDetailsTable(ReportInvoice filter);
        public GoalRealizationReport GetGoalsAndRealized(ReportInvoice filter);

        Task<IEnumerable<ContractGraphic>> GetAllContractsGraphicAsync(int? contractID, string internalCode);

        List<GetContractsByMonthDto> GetContractsByMonth(DateTime startDate, DateTime endDate);
        List<GetContractsStatusByMonthDTO> GetContractsStatusByMonthDTO(DateTime startDate, DateTime endDate);

        List<GetProjectStatusPeriodDTO> GetProjectStatusPeriodDTO(DateTime startDate, DateTime endDate, int FiltroStatusID);

        List<GetTableProjectExecutiveReportDTO> GetTableProjectExecutiveReportDTO(string BusinessUnit, string InspectorName, string TechLeaderName, int Status, DateTime StartDate, DateTime DateFineshed);


    } 
}
