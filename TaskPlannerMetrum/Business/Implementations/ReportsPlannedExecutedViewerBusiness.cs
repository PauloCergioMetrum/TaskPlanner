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

      


        public ProjectOperational OperationalProjectReport(OperationalReportReportDTO OperationalReportReportDTO)
        {
           


            List<GetContractsByMonthDto> ContractsByMonth = _repository.GetContractsByMonth(
                OperationalReportReportDTO.StartDate, 
                OperationalReportReportDTO.EndDate,
                OperationalReportReportDTO.InternalCode
                );

            List<GetContractsStatusByMonthDTO> ContractsStatusByMonth = _repository.GetContractsStatusByMonthDTO(OperationalReportReportDTO.StartDate,
                OperationalReportReportDTO.EndDate,
                OperationalReportReportDTO.InternalCode
                );

            List<GetExecutiveProjectStatusPeriodDto> ProjectStatusPeriod = _repository.GetProjectStatusPeriodDTO(OperationalReportReportDTO.StartDate,
                OperationalReportReportDTO.EndDate,
                OperationalReportReportDTO.FiltroStatusID,
                OperationalReportReportDTO.InternalCode);


            //List<GetTableProjectExecutiveReportDTO> TableProjectExecutiveReport = _repository.GetTableProjectExecutiveReportDTO(
            //    OperationalReportReportDTO.InternalCode,
            //    OperationalReportReportDTO.BusinessUnit,
            //    OperationalReportReportDTO.ProjectManager, 
            //    OperationalReportReportDTO.TechLeaderName,
            //    OperationalReportReportDTO.ProjectStatus, 
            //    OperationalReportReportDTO.StartDateFilter,
            //    OperationalReportReportDTO.EndDateFilter
            //);


            List<GetTaskExecutionMetricsDTO> TaskExecutionMetrics = _repository.GetTaskExecutionMetricsDTO(OperationalReportReportDTO.StartDate, OperationalReportReportDTO.EndDate);



            return new ProjectOperational
            {
                //ContractsForBusinessUnit = ContractsForBusinessUnit,
                //StatusForPeriod = StatusForPeriod,
                //BalancePerProject = BalancePerProject,
                //OperationalRelationshipTable = OperationalRelationshipTable,
                ContractsByMonth = ContractsByMonth,  // grafico STATUS POR UNIDADE DE NEGÓCIO
                ContractsStatusByMonth = ContractsStatusByMonth, // grafico PROJETOS ABERTOS E FECHADOS
                ProjectStatusPeriod = ProjectStatusPeriod, // (Projeto por unidade de Negocio)
                //TableProjectExecutiveReport = TableProjectExecutiveReport, // (Tabela Relatório  Executivo  de projetos)

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



        //Relatorio - Relatório Executivo de Projetos 
        public async Task<List<GetProjectExecutiveStatusResultDto>> GetProjectExecutiveStatusAsync(GetProjectExecutiveStatusDto dto)
        {
            return await _repository.GetProjectExecutiveStatusAsync(dto);
        }

        public async Task<List<GetTableProjectExecutiveReportDto>> GetTableProjectExecutiveReportAsync(GetProjectExecutiveStatusDto dto)
        {
            return await _repository.GetTableProjectExecutiveReportAsync(dto);
        }



        public async Task<List<GetExecutiveProjectStatusPeriodDto>> GetExecutiveProjectStatusPeriodAsync(GetProjectExecutiveStatusDto dto)
        {
            return await _repository.GetExecutiveProjectStatusPeriodAsync(dto);
        }

        public async Task<ProjectExecutiveReportDto> GetProjectExecutiveCombinedReportAsync(GetProjectExecutiveStatusDto dto)
        {
            var status = await _repository.GetProjectExecutiveStatusAsync(dto);
            var table = await _repository.GetTableProjectExecutiveReportAsync(dto);
            var businessUnitStatus = await _repository.GetExecutiveProjecStatusBusinnesUnitAsync(dto);
            var projectStatusPeriod = await _repository.GetExecutiveProjectStatusPeriodAsync(dto);

            return new ProjectExecutiveReportDto
            {
                GetProjectExecutiveStatus = status,
                GetTableProjectExecutiveReport = table,
                GetExecutiveProjecStatusBusinnesUnit = businessUnitStatus,
                GetExecutiveProjectStatusPeriod = projectStatusPeriod
            };
        }




    }
}




