using System;
using System.Collections.Generic;
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

        

        public List<StatusForPeriod> getStatusPerPeriod();

        public List<BalancePerProject> GetBalancePerProject(OperationalReportReportDTO operationalReportReportDTO);





        public List<OperationalRelationshipTable> GetContractDetails(OperationalReportReportDTO OperationalReportReportDTO);

        public List<Model.ModelViews.BusinessUnit> GetAllBusinesUnit();

        public List<OptionsFilterTechLead> GetAllTechLeader();

        public List<OptionsListFilterProjectInspector> GetAllInspector();






    }
}
