using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var startDate = reportPlannedExecuted.startDate ?? DateTime.MinValue;
            var endDate = reportPlannedExecuted.endDate ?? DateTime.MaxValue;

            var listPlannedExecuted = _repository.ReportsPlannedExecuted(startDate, endDate).AsQueryable();

            if (reportPlannedExecuted.lExecutedID?.Count > 0)
            {
                listPlannedExecuted = listPlannedExecuted
                    .Where(c => reportPlannedExecuted.lExecutedID.Any(u => u == c.ExecutorID))
                    .AsQueryable();
            }

            if (reportPlannedExecuted.lContractID?.Count > 0)
            {
                listPlannedExecuted = listPlannedExecuted
                    .Where(c => reportPlannedExecuted.lContractID.Any(u => u == c.ContractID))
                    .AsQueryable();
            }

            if (reportPlannedExecuted.lInspectorID?.Count > 0)
            {
                listPlannedExecuted = listPlannedExecuted
                    .Where(c => reportPlannedExecuted.lInspectorID.Any(u => u == c.inspectorID))
                    .AsQueryable();
            }

            if (reportPlannedExecuted.lTechLeaderID?.Count > 0)
            {
                listPlannedExecuted = listPlannedExecuted
                    .Where(c => reportPlannedExecuted.lTechLeaderID.Any(u => u == c.TechLeaderID))
                    .AsQueryable();
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
            var listPlannedExecuted = SearchReport(reportPlannedExecuted);

            var plannedExecuted = new ReportPlannedExecuted
            {
                listPlannedExecuted = listPlannedExecuted,
                totalPlanned = listPlannedExecuted.Sum(x => x.TotalPlanned ?? 0.0),  
                totalExecuted = listPlannedExecuted.Sum(x => x.TotalExecuted ?? 0.0),
                totalCost = listPlannedExecuted.Sum(x => x.TotalExecutedCost ?? 0.0), 
                totalExpectedHour = Math.Round(HoursExpetcted(
                    reportPlannedExecuted.startDate ?? DateTime.MinValue,
                    reportPlannedExecuted.endDate ?? DateTime.MaxValue,
                    reportPlannedExecuted.lContractID), 2)
            };

            return plannedExecuted;
        }




        public double HoursExpetcted(DateTime? startDate, DateTime? endDate, List<int> contractID)
        {
            double ExpectedHours = 0;

          
            DateTime start = startDate ?? DateTime.MinValue;
            DateTime end = endDate ?? DateTime.MaxValue;

            foreach (var contract in contractID)
            {
                ExpectedHours += _repository.GetHourExpectedHour(start, end, contract);
            }

            return ExpectedHours;
        }

        public List<PreparetBalancePerProject> PreparetBalancePerProject(OperationalReportReportDTO OperationalReportReportDTO)
        {
            List<BalancePerProject> allBalancePerProject = _repository.GetBalancePerProject(OperationalReportReportDTO);
            var distinctBalanceProjects = allBalancePerProject.ToList().Distinct();
            List<PreparetBalancePerProject> balanceDetailsFullList = new List<PreparetBalancePerProject>();

            foreach (var businessUnit in distinctBalanceProjects)
            {
                if (balanceDetailsFullList.Where(b => b.BusinessUnit == businessUnit.BusinessUnit).Count() < 1)
                {
                    balanceDetailsFullList.Add(new PreparetBalancePerProject
                    {
                        BusinessUnit = businessUnit.BusinessUnit,
                        Details = allBalancePerProject
                                .Where(b => b.BusinessUnit == businessUnit.BusinessUnit)
                                .Select(b => new BalancePerProject
                                {
                                    BusinessUnit = b.BusinessUnit,
                                    Period = b.Period,
                                    AumontClose = b.AumontClose,
                                    AumontOpen = b.AumontOpen,
                                })
                                .Distinct()
                                .ToList()
                    });
                }
            }

            return balanceDetailsFullList;
        }


        public ProjectOperational OperationalProjectReport(OperationalReportReportDTO OperationalReportReportDTO)
        {
            List<NumberOfContractsForBusinessUnit> ContractsForBusinessUnit = _repository.CountContractsPerBusinessUnit(OperationalReportReportDTO);
            List<OperationalRelationshipTable> OperationalRelationshipTable = _repository.GetContractDetails(OperationalReportReportDTO);
            List<StatusForPeriod> StatusForPeriod = _repository.getStatusPerPeriod(OperationalReportReportDTO);
            List<PreparetBalancePerProject> BalancePerProject = PreparetBalancePerProject(OperationalReportReportDTO);
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
                BusinessUnits = _repository.GetAllBusinesUnit(),
                ProjectInspector = _repository.GetAllInspector(),
                TechLeader = _repository.GetAllTechLeader(),
                Status = new List<string> { "ABERTO", "FECHADO" }

            };
        }

        public ReportInvoiceDetails InvoiceReport(ReportInvoice filters)
        {

            List<PredictedInvoiced> PredictedInvoiced = _repository.GetMaterialAndService(filters);
            List<MaterialServices> MaterialServices = _repository.GetPredictedInvoicedReport(filters);
            List<BillingPerBusinessUnit> BillingPerBusinessUnit = _repository.GetBillingPerBusinessUnit(filters);
            List<ReportDetailsTable> ReportDetailsTable = _repository.GetReportDetailsTable(filters);
            GoalRealizationReport GoalRealizationReport = _repository.GetGoalsAndRealized(filters);
            return new ReportInvoiceDetails
            {
                PredictedInvoiced = PredictedInvoiced,
                MaterialAndService = MaterialServices,
                BillingPerBusinessUnit = BillingPerBusinessUnit,
                ReportDetailsTable = ReportDetailsTable,
                GoalRealizationReport = GoalRealizationReport

            };
        }

        public async Task<IEnumerable<ContractGraphic>> GetAllContractsGraphicAsync(int? contractID, string internalCode)
        {
            return await _repository.GetAllContractsGraphicAsync(contractID, internalCode);
        }

        public async Task<List<ContractGraphic>> GetContractsByRequestAsync(ContractGraphicRequest request)
        {
            if (request?.Contracts == null || !request.Contracts.Any())
            {
                return (await GetAllContractsGraphicAsync(null, null)).ToList();
            }

            var contractsResult = new List<ContractGraphic>();

            foreach (var contract in request.Contracts)
            {
                var contracts = await GetAllContractsGraphicAsync(contract.ContractID, contract.InternalCode);
                contractsResult.AddRange(contracts ?? Enumerable.Empty<ContractGraphic>());
            }

            return contractsResult;
        }
    }
}




