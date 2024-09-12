using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ReportsViewer
{
    public interface IReportsPlannedExecutedViewerRepository
    {
        public List<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate );
        public List<HoursCostModel> GetHourCost(DateTime startDate, DateTime endDate);
        public double GetHourExpectedHour(DateTime startDate, DateTime endDate, int contractID);

       // public List<NumberOfContractsForBusinessUnit> CountContractsPerBusinessUnit();

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




    }
}
