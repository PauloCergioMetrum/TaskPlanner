


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

        public List<NumberOfContractsForBusinessUnit> CountContractsPerBusinessUnit()
        {
            var sql = " EXEC [dbo].[GetNumberOfContractsForBusinessUnit]";
            return _context.Set<NumberOfContractsForBusinessUnit>().FromSqlRaw(sql).ToList();
        }
        public List<StatusForPeriod> getStatusPerPeriod()
        {
            var sql = " EXEC [dbo].[getStatusPerPeriod]";
            return _context.Set<StatusForPeriod>().FromSqlRaw(sql).ToList();
        }


        public List<BalancePerProject> GetBalancePerProject()
        {

            var sql = " EXEC [dbo].[getBalancePerProject]";
            return _context.Set<BalancePerProject>().FromSqlRaw(sql).ToList();
          
        }

        public List<OperationalRelationshipTable> GetContractDetails(OperationalReportReportDTO OperationalReportReportDTO)
        {
          
            var sql = $"EXEC [dbo].[GetContractDetails] @ContractIDs = '{string.Join(",", OperationalReportReportDTO.ContractIDs)}' @TechLeadIDs =' {string.Join(",", OperationalReportReportDTO.TechLeadIDs)}'";
            return _context.Set<OperationalRelationshipTable>().FromSqlRaw(sql).ToList();


        }



        public List<OptionsFilterTechLead> GetAllTechLeader()
        {
            var allTechLeaderID = _context.MilestonesValue.Select(t => t.TechLeadID).Distinct().ToList();
            return _context.Users
            .Where(u => allTechLeaderID.Contains(u.Id)).Select(u => new OptionsFilterTechLead
            {
                ID = u.Id,
                Name = u.FullName
            }).ToList();
        }
        public List<OptionsListFilterProjectInspector> GetAllFiscal()
        {
            var allProjectInspector = _context.Contracts.Select(t => t.inspectorID).Distinct().ToList();
            return _context.Users
            .Where(u => allProjectInspector.Contains(u.Id)).Select(u => new OptionsListFilterProjectInspector
            {
                id = u.Id,
                Name = u.FullName
            }).ToList();
        }

        public List<Model.ModelViews.BusinessUnit> GetAllBusinesUnit()
        {
            return _context.BusinessUnit.ToList();

        }













    }

}





