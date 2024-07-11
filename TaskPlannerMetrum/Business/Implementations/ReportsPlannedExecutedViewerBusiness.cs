using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.Generic;
using TaskPlannerMetrum.Repository.ReportsViewer;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ReportsPlannedExecutedViewerBusiness : IReportsPlannedExecutedViewewBussines
    {
        private readonly IReportsPlannedExecutedViewerRepository _repository;

        public ReportsPlannedExecutedViewerBusiness(IReportsPlannedExecutedViewerRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate)
        {
            return _repository.ReportsPlannedExecuted(startDate, endDate);
        }

        public List<vReports_PlannedExecuted> SearchReport(ReportPlannedExecutedDTO reportPlannedExecuted)
        {
            var listPlannedExecuted = _repository.ReportsPlannedExecuted(reportPlannedExecuted.startDate, reportPlannedExecuted.endDate).AsQueryable();

            if (reportPlannedExecuted.lExecutedID.Count() != 0)
            {
                listPlannedExecuted = listPlannedExecuted.Where(c => reportPlannedExecuted.lExecutedID.Any(u => u == c.ExecutorID)).AsQueryable();
            }

            if (reportPlannedExecuted.lContractID.Count() != 0)
            {

                listPlannedExecuted = listPlannedExecuted.Where(c => reportPlannedExecuted.lContractID.Any(u => u == c.ContractID)).AsQueryable();
            }

            if (reportPlannedExecuted.lInspectorID.Count() != 0)
            {
                listPlannedExecuted = listPlannedExecuted.Where(c => reportPlannedExecuted.lInspectorID.Any(u => u == c.inspectorID)).AsQueryable();
            }

            if (reportPlannedExecuted.lTechLeaderID.Count() != 0)
            {
                listPlannedExecuted = listPlannedExecuted.Where(c => reportPlannedExecuted.lTechLeaderID.Any(u => u == c.TechLeaderID)).AsQueryable();
            }
            return listPlannedExecuted.ToList();
        }

        public double HoursCostSearch(List<vReports_PlannedExecuted> reportPlannedExecuted)
        {
            var startDate = reportPlannedExecuted.OrderBy(s => s.ScheduledDate).Select(s => s.ScheduledDate).First();
            var endDate = reportPlannedExecuted.OrderBy(s => s.ScheduledDate).Select(s => s.ScheduledDate).Last();
            var listExecutedID = reportPlannedExecuted.Select(e => e.ExecutorID).ToList();
            var listContractID = reportPlannedExecuted.Select(e => e.ContractID).ToList();
            var listHoursCost = _repository.GetHourCost(startDate, endDate).AsQueryable();


            if (reportPlannedExecuted.Select(e => e.ExecutorID).ToList().Count != 0)
            {
                listHoursCost = listHoursCost.Where(l => listExecutedID.Any(u => u == l.UserID)).AsQueryable();
            }

            if (reportPlannedExecuted.Select(e => e.ContractID).ToList().Count != 0)
            {
                listHoursCost = listHoursCost.Where(l => listContractID.Any(u => u == l.ContractID)).AsQueryable();
            }
            return Math.Round(listHoursCost.Select(d => d.DayCost).Sum(), 2);
        }


        public ReportPlannedExecuted GetPlannedExecuted(ReportPlannedExecutedDTO reportPlannedExecuted)
        {
            //Lista Filtadra com todos os paramentros para a geração do relatorio
            var listPlannedExecuted = SearchReport(reportPlannedExecuted);



            var plannedExecuted = new ReportPlannedExecuted
            {
                listPlannedExecuted = listPlannedExecuted,
                totalPlanned = Math.Round(listPlannedExecuted.Sum(s => s.PlannedManHour), 2),
                totalExecuted = Math.Round(listPlannedExecuted.Sum(s => s.ExecutedManHour), 2),
                totalCost = HoursCostSearch(listPlannedExecuted),
                totalExpectedHour = Math.Round(HoursExpetcted(reportPlannedExecuted.startDate, reportPlannedExecuted.endDate, reportPlannedExecuted.lContractID), 2)

            };

            return plannedExecuted;
        }

        public double HoursExpetcted(DateTime startDate, DateTime endDate, List<int> contractID)
        {
            double ExpectedHours = 0;
            foreach (var contract in contractID)
            {
                ExpectedHours += _repository.GetHourExpectedHour(startDate, endDate, contract);
            }
            return ExpectedHours;

        }



        public ProjectOperational OperationalProjectReport(OperationalReportReportDTO OperationalReportReportDTO)
        {
            List<NumberOfContractsForBusinessUnit> ContractsForBusinessUnit = _repository.CountContractsPerBusinessUnit();

            List<StatusForPeriod> StatusForPeriod = _repository.getStatusPerPeriod();
            List<BalancePerProject> BalancePerProject = _repository.GetBalancePerProject();
            List<OperationalRelationshipTable> OperationalRelationshipTable = _repository.GetContractDetails(OperationalReportReportDTO);
            return new ProjectOperational
            {
                ContractsForBusinessUnit = ContractsForBusinessUnit,
                StatusForPeriod = StatusForPeriod,
                BalancePerProject = BalancePerProject,
                OperationalRelationshipTable = OperationalRelationshipTable
            };
        }

        public OptionsListFilter OptionsListFilter()
        {
            return new OptionsListFilter
            {
                BusinessUnits =_repository.GetAllBusinesUnit(),
                ProjectInspector = _repository.GetAllFiscal(),
                TechLeader = _repository.GetAllTechLeader(),
                Status = new List<string>{ "ABERTO", "FECHADO"}

            };
        }
    }
}



