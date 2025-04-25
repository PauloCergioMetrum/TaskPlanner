


using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlannerMetrum.Repository.ReportsViewer
{
    public class ReportsPlannedExecutedViewerRepository : IReportsPlannedExecutedViewerRepository
    {
        private readonly MSSQLContext _context;

        public ReportsPlannedExecutedViewerRepository(MSSQLContext context)
        {
            _context = context;
        }
        public double GetHourExpectedHour(DateTime startDate, DateTime endDate, int contractID)
        {
            double hourResult = 0;
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "EXECUTE [dbo].[ExpectedHour] @Start,@End,@ContractID ";
                command.Parameters.Add(new SqlParameter("@Start", startDate));
                command.Parameters.Add(new SqlParameter("@End", endDate));
                command.Parameters.Add(new SqlParameter("@ContractID", contractID));
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        hourResult = 0;

                    }
                    else
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0))
                            {
                                hourResult = 0;
                            }
                            else
                            {
                                hourResult = Convert.ToDouble(reader.GetValue(0));
                            }
                        }                  
                    }

                }
                return hourResult;
            }
        }
        public List<HoursCostModel> GetHourCost(DateTime startDate, DateTime endDate)
        {
            List<HoursCostModel> listHoursCost = new List<HoursCostModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "EXECUTE [dbo].[HoursCost] @startDate,@endDate,@contractID,@userID";
                 command.Parameters.Add(new SqlParameter("@startDate", startDate));
                command.Parameters.Add(new SqlParameter("@endDate", endDate));
                command.Parameters.Add(new SqlParameter("@contractID", Convert.ToInt64(0)));
                command.Parameters.Add(new SqlParameter("@userID", Convert.ToInt64(0)));
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader()){
                    while (reader.Read())
                    {
                        int index = 0;
                        var register = new HoursCostModel
                        {
                            ContractID = reader.GetInt32(index++),
                            ActivityID = reader.GetInt32(index++),
                            UserID = reader.GetInt32(index++),
                            ScheduleDate = reader.GetDateTime(index++),
                            DayCost = reader.GetDouble(index++),
                        };

                        listHoursCost.Add(register);
                    }
                }
                return listHoursCost;
            }

        }

        public List<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate)
        {
            try
            {

                var list = _context.vReports_PlannedExecuted.Where(e => e.ScheduledDate.Date >= startDate.Date && e.ScheduledDate.Date <= endDate.Date).OrderByDescending(e => e.ScheduledDate).ToList();

                return list;

            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<NumberOfContractsForBusinessUnit> CountContractsPerBusinessUnit(OperationalReportReportDTO operationalReportReportDTO)
        {
            var contractIDs = string.Join(",", operationalReportReportDTO.ContractIDs);

            var filteredBusinessUnits = _context.BusinessUnit
                                        .Where(bu => operationalReportReportDTO.BusinessUnitIDs.Contains(bu.Id))
                                        .ToList();
            var businessUnitNames = string.Join(",", filteredBusinessUnits.Select(bu => bu.Name));

            var startDate = operationalReportReportDTO.StartDate != default(DateTime)
                                ? operationalReportReportDTO.StartDate.ToString("yyyy-MM-dd")
                                : "null";
            var endDate = operationalReportReportDTO.EndDate != default(DateTime)
                                ? operationalReportReportDTO.EndDate.ToString("yyyy-MM-dd")
                                : "null";

            var projectInspectorIDs = operationalReportReportDTO.ProjectInspectorIDs != null && operationalReportReportDTO.ProjectInspectorIDs.Any()
                                ? string.Join(",", operationalReportReportDTO.ProjectInspectorIDs)
                                : "null";

            var techLeadIDs = operationalReportReportDTO.TechLeadIDs != null && operationalReportReportDTO.TechLeadIDs.Any()
                                ? string.Join(",", operationalReportReportDTO.TechLeadIDs)
                                : "null";

            if (operationalReportReportDTO.ContractIDs.Count == 0)
            {
                contractIDs = "null";
            }
            if (operationalReportReportDTO.BusinessUnitIDs.Count == 0)
            {
                businessUnitNames = "null";
            }

            var sql = "EXEC [dbo].[GetNumberOfContractsForBusinessUnit] " +
                      $"@BusinessUnitNames = {(businessUnitNames == "null" ? "null" : $"'{businessUnitNames}'")}, " +
                      $"@ProjectInspectorIDS = {(projectInspectorIDs == "null" ? "null" : $"'{projectInspectorIDs}'")}, " +
                      $"@TechLeadIDs = {(techLeadIDs == "null" ? "null" : $"'{techLeadIDs}'")}, " +
                      $"@StartPeriod = {(startDate != "null" ? $"'{startDate}'" : "null")}, " +
                      $"@EndPeriod = {(endDate != "null" ? $"'{endDate}'" : "null")}";

            return _context.Set<NumberOfContractsForBusinessUnit>().FromSqlRaw(sql).ToList();
        }





        public List<StatusForPeriod> getStatusPerPeriod(OperationalReportReportDTO OperationalReportReportDTO)
        {
            var startDate = OperationalReportReportDTO.StartDate != default(DateTime)
                                ? OperationalReportReportDTO.StartDate.ToString("yyyy-MM-dd")
                                : "null";
            var endDate = OperationalReportReportDTO.EndDate != default(DateTime)
                                ? OperationalReportReportDTO.EndDate.ToString("yyyy-MM-dd")
                                : "null";

            var sql = "EXEC [dbo].[getStatusPerPeriod] " +
                      $"@StartDate = {(startDate != "null" ? $"'{startDate}'" : "null")}, " +
                      $"@EndDate = {(endDate != "null" ? $"'{endDate}'" : "null")}";

            return _context.Set<StatusForPeriod>().FromSqlRaw(sql).ToList();
        }


        public List<BalancePerProject> GetBalancePerProject(OperationalReportReportDTO operationalReportReportDTO)
        {
            var contractIDs = string.Join(",", operationalReportReportDTO.ContractIDs);
            var startDate = operationalReportReportDTO.StartDate.ToString("yyyy-MM-dd");
            var endDate = operationalReportReportDTO.EndDate.ToString("yyyy-MM-dd");

            var sql = "EXEC [dbo].[getBalancePerProject] " +
                      $"@ContractIDs = {(string.IsNullOrEmpty(contractIDs) ? "null" : $"'{contractIDs}'")}, " +
                      $"@StartDate = '{startDate}', " +
                      $"@EndDate = '{endDate}'";
            return _context.Set<BalancePerProject>().FromSqlRaw(sql).ToList();
        }


        public List<OperationalRelationshipTable> GetContractDetails(OperationalReportReportDTO operationalReportReportDTO)
        {
            var contractIDs = string.Join(",", operationalReportReportDTO.ContractIDs);
            var techLeadIDs = string.Join(",", operationalReportReportDTO.TechLeadIDs);
            var inspectorIDs = string.Join(",", operationalReportReportDTO.ProjectInspectorIDs);

            var filteredBusinessUnits = _context.BusinessUnit
                .Where(bu => operationalReportReportDTO.BusinessUnitIDs.Contains(bu.Id))
                .ToList();

            var businessUnitNames = filteredBusinessUnits.Select(bu => bu.Name).ToList();

            string startDate = operationalReportReportDTO.StartDate != default(DateTime)
                ? operationalReportReportDTO.StartDate.ToString("yyyy-MM-dd")
                : "null";

            string endDate = operationalReportReportDTO.EndDate != default(DateTime)
                ? operationalReportReportDTO.EndDate.ToString("yyyy-MM-dd")
                : "null";

            string statusFilter = !string.IsNullOrEmpty(operationalReportReportDTO.StatusFilter)
                ? $"'{operationalReportReportDTO.StatusFilter}'"
                : "null";

            var sql = $"EXEC [dbo].[GetContractDetails] " +
                      $"@ContractIDs = {(string.IsNullOrEmpty(contractIDs) ? "null" : $"'{contractIDs}'")}, " +
                      $"@TechLeadIDs = {(string.IsNullOrEmpty(techLeadIDs) ? "null" : $"'{techLeadIDs}'")}, " +
                      $"@InspectorIDs = {(string.IsNullOrEmpty(inspectorIDs) ? "null" : $"'{inspectorIDs}'")}, " +
                      $"@BusinessUnitIDs = {(businessUnitNames.Count > 0 ? $"'{string.Join(",", businessUnitNames)}'" : "null")}, " +
                      $"@StartDate = {(startDate != "null" ? $"'{startDate}'" : "null")}, " +
                      $"@EndDate = {(endDate != "null" ? $"'{endDate}'" : "null")}, " +
                      $"@StatusFilter = {statusFilter}";    

            return _context.Set<OperationalRelationshipTable>().FromSqlRaw(sql).ToList();
        }


        public List<OptionsFilterTechLead> GetAllTechLeader()
        {

            var allPermissions = _context.Permissions.ToList();
            var idPermissions = allPermissions.Where(n => n.Description == "Admin" || n.Description == "Supervisor").Select(i => i.id).ToList();
            return _context.Users
            .Where(u => idPermissions.Contains(u.PermissionId) && u.IsActive == true).Select(u => new OptionsFilterTechLead
            {
                ID = u.Id,
                Name = u.FullName
            }).ToList();
        }
        public List<OptionsListFilterProjectInspector> GetAllInspector()
        {
            var getIdSFiscalList = _context.Department.Where(d => d.Name == "DEPCNT").Select(i => i.ID).ToList();
            return _context.Users
            .Where(u => getIdSFiscalList.Contains(u.DepartmentId) && u.IsActive == true).Select(u => new OptionsListFilterProjectInspector
            {
                id = u.Id,
                Name = u.FullName
            }).ToList();
        }

        public List<Model.ModelViews.BusinessUnit> GetAllBusinesUnit()
        {
            return _context.BusinessUnit.ToList();

        }


        public List<PredictedInvoiced> GetMaterialAndService(ReportInvoice parameters)
        {
           
            string businessUnits = parameters.BusinessUnits.Any() ? string.Join(",", parameters.BusinessUnits) : null;
            string inspectorIDs = parameters.InspectorIDs.Any() ? string.Join(",", parameters.InspectorIDs) : null;  
            var sql = "EXEC [dbo].[GetPredictedInvoiced] " +
                      "@startDate, " +
                      "@endDate, " +
                      "@businessUnits, " +
                      "@inspectorIDs";
            return _context.Set<PredictedInvoiced>()
                .FromSqlRaw(sql,
                    new SqlParameter("@startDate", (object)parameters.StartDate ?? DBNull.Value),
                    new SqlParameter("@endDate", (object)parameters.EndDate ?? DBNull.Value),
                    new SqlParameter("@businessUnits", (object)businessUnits ?? DBNull.Value),
                    new SqlParameter("@inspectorIDs", (object)inspectorIDs ?? DBNull.Value))
                .ToList();
        }


        public List<MaterialServices> GetPredictedInvoicedReport(ReportInvoice parameters)
        {
          
            string businessUnits = parameters.BusinessUnits != null && parameters.BusinessUnits.Any()
                ? string.Join(",", parameters.BusinessUnits)
                : null;
            string inspectorIDs = parameters.InspectorIDs != null && parameters.InspectorIDs.Any()
                ? string.Join(",", parameters.InspectorIDs)
                : null;
            var sql = "EXEC [dbo].[GetMaterialAndServiceCount] @startDate, @endDate, @BusinessUnits, @InspectorIDs";
            return _context.Set<MaterialServices>()
                .FromSqlRaw(sql,
                    new SqlParameter("@startDate", parameters.StartDate.HasValue ? (object)parameters.StartDate.Value : DBNull.Value),
                    new SqlParameter("@endDate", parameters.EndDate.HasValue ? (object)parameters.EndDate.Value : DBNull.Value),
                    new SqlParameter("@BusinessUnits", businessUnits ?? (object)DBNull.Value),
                    new SqlParameter("@InspectorIDs", inspectorIDs ?? (object)DBNull.Value))
                .ToList();
        }
        public List<BillingPerBusinessUnit> GetBillingPerBusinessUnit(ReportInvoice filter)
        {
           
            var businessUnits = filter.BusinessUnits != null && filter.BusinessUnits.Count > 0
                ? string.Join(",", filter.BusinessUnits)
                : null;
            var inspectorIDs = filter.InspectorIDs != null && filter.InspectorIDs.Count > 0
                ? string.Join(",", filter.InspectorIDs)
                : null;
            var sql = "EXEC [dbo].[BillingPerBusinessUnit] @startDate, @endDate, @BusinessUnits, @InspectorIDs";
            return _context.Set<BillingPerBusinessUnit>()
                .FromSqlRaw(sql,
                    new SqlParameter("@startDate", (object)filter.StartDate ?? DBNull.Value),
                    new SqlParameter("@endDate", (object)filter.EndDate ?? DBNull.Value),
                    new SqlParameter("@BusinessUnits", (object)businessUnits ?? DBNull.Value),
                    new SqlParameter("@InspectorIDs", (object)inspectorIDs ?? DBNull.Value))
               .ToList();
        }
        public List<ReportDetailsTable> GetReportDetailsTable(ReportInvoice filter)
        {
          
            var businessUnits = filter.BusinessUnits != null && filter.BusinessUnits.Count > 0
                ? string.Join(",", filter.BusinessUnits)
                : null;
            var inspectorIDs = filter.InspectorIDs != null && filter.InspectorIDs.Count > 0
                ? string.Join(",", filter.InspectorIDs)
                : null;
            var sql = "EXEC [dbo].[GetReportDetailsTable] @startDate, @endDate, @BusinessUnits, @InspectorIDs";
            return _context.Set<ReportDetailsTable>()
                .FromSqlRaw(sql,
                    new SqlParameter("@startDate", (object)filter.StartDate ?? DBNull.Value),
                    new SqlParameter("@endDate", (object)filter.EndDate ?? DBNull.Value),
                    new SqlParameter("@BusinessUnits", (object)businessUnits ?? DBNull.Value),
                    new SqlParameter("@InspectorIDs", (object)inspectorIDs ?? DBNull.Value))
                .ToList();
        }

        public GoalRealizationReport GetGoalsAndRealized(ReportInvoice filter)
        {
          
            if (!filter.StartDate.HasValue)
                throw new ArgumentException("StartDate is required.");
            string year = filter.StartDate.Value.Year.ToString();

            var sql = "EXEC [dbo].[GetGoalsAndRealized] @year, @startDate, @endDate";

            var result = _context.Set<GoalRealizationReport>()
                .FromSqlRaw(sql,
                    new SqlParameter("@year", year),
                    new SqlParameter("@startDate", filter.StartDate ?? (object)DBNull.Value),
                    new SqlParameter("@endDate", filter.EndDate ?? (object)DBNull.Value))
                .AsEnumerable()
                .FirstOrDefault();

            return result ?? new GoalRealizationReport();
        }

        public async Task<IEnumerable<ContractGraphic>> GetAllContractsGraphicAsync(int? contractID, string internalCode)
        {
            var contractIDParam = contractID.HasValue
                ? new SqlParameter("@ContractID", contractID.Value)
                : new SqlParameter("@ContractID", DBNull.Value);

            var internalCodeParam = string.IsNullOrEmpty(internalCode)
                ? new SqlParameter("@InternalCode", DBNull.Value)
                : new SqlParameter("@InternalCode", internalCode);

            var result = await _context.ContractGraphics
                .FromSqlRaw("EXEC GetAllContractsGraphic @ContractID, @InternalCode", contractIDParam, internalCodeParam)
                .ToListAsync();

            return result;
        }


        public List<GetContractsByMonthDto> GetContractsByMonth(DateTime startDate, DateTime endDate)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);

            return _context.GetContractsByMonthDto.FromSqlRaw(
                "EXEC dbo.GetContractsByMonth @StartDate, @EndDate",
                startParam, endParam
            ).ToList();
        }


        public List<GetContractsStatusByMonthDTO> GetContractsStatusByMonthDTO(DateTime startDate, DateTime endDate)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);

            return _context.GetContractsStatusByMonthDTO.FromSqlRaw(
                "EXEC dbo.GetContractsStatusByMonth @StartDate, @EndDate",
                startParam, endParam
            ).ToList();
        }

        public List<GetProjectStatusPeriodDTO> GetProjectStatusPeriodDTO(DateTime startDate, DateTime endDate, int FiltroStatusID)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var FiltroStatusIDParam = new SqlParameter("@FiltroStatusID", FiltroStatusID);

            return _context.GetProjectStatusPeriodDTO.FromSqlRaw(
                "EXEC dbo.GetProjectStatusPeriod @StartDate, @EndDate, @FiltroStatusID",
                startParam, endParam, FiltroStatusIDParam
            ).ToList();
        }


    }

}











