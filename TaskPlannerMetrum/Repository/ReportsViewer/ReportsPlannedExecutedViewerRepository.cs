


using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                //command.CommandType = System.Data.CommandType.StoredProcedure;
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
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@startDate", startDate));
                command.Parameters.Add(new SqlParameter("@endDate", endDate));
                command.Parameters.Add(new SqlParameter("@contractID", Convert.ToInt64(0)));
                command.Parameters.Add(new SqlParameter("@userID", Convert.ToInt64(0)));
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {

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
            catch (Exception )
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

            // Verifica se a lista de contratos está vazia e ajusta para "null" se for o caso
            if (operationalReportReportDTO.ContractIDs.Count == 0)
            {
                contractIDs = "null";
            }

            // Verifica se a lista de IDs de unidades de negócio está vazia e ajusta para "null" se for o caso
            if (operationalReportReportDTO.BusinessUnitIDs.Count == 0)
            {
                businessUnitNames = "null";
            }

            var sql = "EXEC [dbo].[GetNumberOfContractsForBusinessUnit] " +
                      $"@ContractIDs = {contractIDs}, " +
                      $"@BusinessUnitNames = {(businessUnitNames == "null" ? "null" : $"'{businessUnitNames}'")}";  // Inclui null ou os nomes das unidades de negócio

            return _context.Set<NumberOfContractsForBusinessUnit>().FromSqlRaw(sql).ToList();
        }
        public List<StatusForPeriod> getStatusPerPeriod( OperationalReportReportDTO OperationalReportReportDTO)
        {
            var startDate = OperationalReportReportDTO.StartDate != null ?
                                OperationalReportReportDTO.StartDate.ToString("yyyy-MM-dd") :
                                "null";
            var endDate = OperationalReportReportDTO.EndDate != null ?
                                OperationalReportReportDTO.EndDate.ToString("yyyy-MM-dd") :
                                "null";
            var sql = "EXEC [dbo].[getStatusPerPeriod]" +
                        $"@StartDate = {(startDate != "null" ? $"'{startDate}'" : "null")}, " +
                        $"@EndDate = {(endDate != "null" ? $"'{endDate}'" : "null")}";
            return _context.Set<StatusForPeriod>().FromSqlRaw(sql).ToList();
        }


        public List<BalancePerProject> GetBalancePerProject(OperationalReportReportDTO operationalReportReportDTO)
        {
            var contractIDs = string.Join(",", operationalReportReportDTO.ContractIDs);
            var startDate = operationalReportReportDTO.StartDate != null ?
                                operationalReportReportDTO.StartDate.ToString("yyyy-MM-dd") :
                                "null";
            var endDate = operationalReportReportDTO.EndDate != null ?
                                operationalReportReportDTO.EndDate.ToString("yyyy-MM-dd") :
                                "null";

            var sql = "EXEC [dbo].[getBalancePerProject] " +
                        $"@ContractIDs = {(string.IsNullOrEmpty(contractIDs) ? "null" : $"'{contractIDs}'")}, " +
                        $"@StartDate = {(startDate != "null" ? $"'{startDate}'" : "null")}, " +
                        $"@EndDate = {(endDate != "null" ? $"'{endDate}'" : "null")}";

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

            string startDate = operationalReportReportDTO.StartDate != null ?
                                operationalReportReportDTO.StartDate.ToString("yyyy-MM-dd") :
                                "null";

            string endDate = operationalReportReportDTO.EndDate != null ?
                                operationalReportReportDTO.EndDate.ToString("yyyy-MM-dd") :
                                "null";

            var sql = $"EXEC [dbo].[GetContractDetails] " +
                      $"@ContractIDs = {(string.IsNullOrEmpty(contractIDs) ? "null" : $"'{contractIDs}'")}, " +
                      $"@TechLeadIDs = {(string.IsNullOrEmpty(techLeadIDs) ? "null" : $"'{techLeadIDs}'")}, " +
                      $"@InspectorIDs = {(string.IsNullOrEmpty(inspectorIDs) ? "null" : $"'{inspectorIDs}'")}, " +
                      $"@BusinessUnitIDs = {(businessUnitNames.Count > 0 ? $"'{string.Join(",", businessUnitNames)}'" : "null")}, " +
                      $"@StartDate = {(startDate != "null" ? $"'{startDate}'" : "null")}, " +
                      $"@EndDate = {(endDate != "null" ? $"'{endDate}'" : "null")}";

            return _context.Set<OperationalRelationshipTable>().FromSqlRaw(sql).ToList();
        }

        public List<OptionsFilterTechLead> GetAllTechLeader()
        {

            var allPermissions =_context.Permissions.ToList();  
            var idPermissions = allPermissions.Where(n=> n.Description =="Admin" || n.Description =="Supervisor").Select(i=> i.id).ToList();
            return _context.Users
            .Where(u => idPermissions.Contains(u.PermissionId) && u.IsActive == true).Select(u => new OptionsFilterTechLead
            {
                ID = u.Id,
                Name = u.FullName
            }).ToList();
        }
        public List<OptionsListFilterProjectInspector> GetAllInspector()
        {
            var getIdSFiscalList =_context.Department.Where(d=> d.Name == "DEPCNT").Select(i=> i.ID).ToList();
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
            // Converte listas para strings, ou nulo se a lista estiver vazia
            string businessUnits = parameters.BusinessUnits.Any() ? string.Join(",", parameters.BusinessUnits) : null;
            string inspectorIDs = parameters.InspectorIDs.Any() ? string.Join(",", parameters.InspectorIDs) : null;

            // Cria a string SQL para a chamada da stored procedure
            var sql = "EXEC [dbo].[GetPredictedInvoiced] " +
                      "@startDate, " +
                      "@endDate, " +
                      "@businessUnits, " +
                      "@inspectorIDs";

            // Chama a stored procedure passando os parâmetros
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
            // Converte listas para strings, ou nulo se a lista estiver vazia
            string businessUnits = parameters.BusinessUnits != null && parameters.BusinessUnits.Any()
                ? string.Join(",", parameters.BusinessUnits)
                : null;
            string inspectorIDs = parameters.InspectorIDs != null && parameters.InspectorIDs.Any()
                ? string.Join(",", parameters.InspectorIDs)
                : null;

            // Prepara o comando SQL com os parâmetros
            var sql = "EXEC [dbo].[GetMaterialAndServiceCount] @startDate, @endDate, @BusinessUnits, @InspectorIDs";

            // Chama a stored procedure passando os parâmetros
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
            // Converte a lista de BusinessUnits e InspectorIDs para strings ou usa null se estiver vazia
            var businessUnits = filter.BusinessUnits != null && filter.BusinessUnits.Count > 0
                ? string.Join(",", filter.BusinessUnits)
                : null;
            var inspectorIDs = filter.InspectorIDs != null && filter.InspectorIDs.Count > 0
                ? string.Join(",", filter.InspectorIDs)
                : null;

            // SQL para chamar a stored procedure
            var sql = "EXEC [dbo].[BillingPerBusinessUnit] @startDate, @endDate, @BusinessUnits, @InspectorIDs";

            // Chama a stored procedure e retorna os resultados
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
            // Converte a lista de BusinessUnits e InspectorIDs para strings ou usa null se estiver vazia
            var businessUnits = filter.BusinessUnits != null && filter.BusinessUnits.Count > 0
                ? string.Join(",", filter.BusinessUnits)
                : null;
            var inspectorIDs = filter.InspectorIDs != null && filter.InspectorIDs.Count > 0
                ? string.Join(",", filter.InspectorIDs)
                : null;

            // SQL para chamar a stored procedure
            var sql = "EXEC [dbo].[GetReportDetailsTable] @startDate, @endDate, @BusinessUnits, @InspectorIDs";

            // Chama a stored procedure e retorna os resultados
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
            // Verifica se o StartDate tem valor, se não, lança uma exceção ou lida de outra forma
            if (!filter.StartDate.HasValue)
                throw new ArgumentException("StartDate is required.");

            // Extrair o ano do StartDate como string
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










    }

}





