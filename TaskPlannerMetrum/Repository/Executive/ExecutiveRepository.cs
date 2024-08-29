using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.Executive;

namespace TaskPlannerMetrum.Repository.PvExecutive
{
    public class ExecutiveRepository : IExecutiveRepository
    {
        private readonly MSSQLContext _context;

        public ExecutiveRepository(MSSQLContext context)
        {
            _context = context;
        }




        public List<ExecutiveDto> GetPvExecutiveTable(string inspectorIDs = null, DateTime? startDate = null, DateTime? endDate = null, string businessUnits = null)
        {
            var pvExecutiveList = new List<ExecutiveDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[GetFiscalFilter]";
                command.Parameters.Add(new SqlParameter("@InspectorIDs", inspectorIDs ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@StartDate", startDate ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@EndDate", endDate ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@BusinessUnits", businessUnits ?? (object)DBNull.Value));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dto = new ExecutiveDto
                        {
                            ID = reader.GetInt64(reader.GetOrdinal("ID")),
                            InspectorID = reader.GetInt32(reader.GetOrdinal("inspectorID")),
                            UserName = reader.GetString(reader.GetOrdinal("user_name")),
                            WorkSpaceID = reader.GetInt32(reader.GetOrdinal("WorkSpaceID")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            BusinessUnit = reader.GetString(reader.GetOrdinal("BusinessUnit")),
                            InternalCode = reader.GetString(reader.GetOrdinal("InternalCode")),
                            ClientID = reader.GetInt32(reader.GetOrdinal("ClientID")),
                            ClientName = reader.GetString(reader.GetOrdinal("ClientName")),
                            Condition = reader.GetString(reader.GetOrdinal("Condition")),
                            Value = reader.GetDouble(reader.GetOrdinal("Value")),
                            InvoicedValue = reader.GetDouble(reader.GetOrdinal("InvoicedValue")),
                            ExpectedInvoiceDate = reader.GetString(reader.GetOrdinal("ExpectedInvoiceDate")),
                            InvoicedDate = reader.IsDBNull(reader.GetOrdinal("InvoicedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("InvoicedDate")),
                            StatusDpv = reader.GetString(reader.GetOrdinal("StatusDpv"))
                        };

                        pvExecutiveList.Add(dto);
                    }
                }
            }

            return pvExecutiveList;
        }





        public List<GetPvSummary> GetPvSummaries(DateTime? StartYearMonth = null, DateTime? EndYearMonth = null)
        {
            var getPvSummaries = new List<GetPvSummary>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[GetPvSummary]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StartYearMonth", SqlDbType.DateTime) { Value = (object)StartYearMonth ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@EndYearMonth", SqlDbType.DateTime) { Value = (object)EndYearMonth ?? DBNull.Value });

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var summary = new GetPvSummary
                        {
                            Ano = reader.GetInt32(reader.GetOrdinal("Ano")),
                            Mes = reader.GetString(reader.GetOrdinal("Mes")),
                            Abertos = reader.GetInt32(reader.GetOrdinal("Abertos")),
                            Fechados = reader.GetInt32(reader.GetOrdinal("Fechados")),
                        };

                        getPvSummaries.Add(summary);
                    }
                }

                _context.Database.CloseConnection();
            }

            return getPvSummaries;
        }




        public List<BusinessUnitPercentage> GetBusinessUnitPercentages()
        {
            var businessUnitPercentages = new List<BusinessUnitPercentage>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[GetBusinessUnitPercentage]";
                command.CommandType = CommandType.StoredProcedure;

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var percentage = new BusinessUnitPercentage
                        {
                            BusinessUnit = reader.GetString(reader.GetOrdinal("BusinessUnit")),
                            UnitCount = reader.GetInt32(reader.GetOrdinal("UnitCount")),
                            Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage"))
                        };

                        businessUnitPercentages.Add(percentage);
                    }
                }
            }

            return businessUnitPercentages;
        }
        public List<ExecutivePVgraphic> GetExecutivePVGraphic()
        {
            var executivePVgraphicList = new List<ExecutivePVgraphic>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[GetExecutivePVGraphic]";
                command.CommandType = CommandType.StoredProcedure;

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var executivePVgraphic = new ExecutivePVgraphic
                        {
                            TotalValue = reader.GetDouble(reader.GetOrdinal("TotalValue")),
                            TotalInternalCode = reader.GetInt32(reader.GetOrdinal("TotalInternalCode"))
                        };

                        executivePVgraphicList.Add(executivePVgraphic);
                    }
                }

                _context.Database.CloseConnection();
            }

            return executivePVgraphicList;
        }

        public List<ExecutivePvGraphicsResult> GetExecutivePvGraphicsOpenClose(bool? closed = null, bool? open = null)
        {
            var executivePvGraphics = new List<ExecutivePvGraphicsResult>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[GetExecutivePvGraphicsOpenClose]";
                command.CommandType = CommandType.StoredProcedure;

          
                var closedParameter = command.CreateParameter();
                closedParameter.ParameterName = "@Closed";
                closedParameter.Value = closed.HasValue ? (object)closed.Value : DBNull.Value;
                command.Parameters.Add(closedParameter);

                var openParameter = command.CreateParameter();
                openParameter.ParameterName = "@Open";
                openParameter.Value = open.HasValue ? (object)open.Value : DBNull.Value;
                command.Parameters.Add(openParameter);

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var executivePVgraphic = new ExecutivePvGraphicsResult
                        {
                            BusinessUnit = reader.GetString(reader.GetOrdinal("BusinessUnit")),
                            StartDate = reader.GetString(reader.GetOrdinal("StartDate")),
                            ValueTotal = reader.GetDouble(reader.GetOrdinal("ValueTotal"))
                        };

                        executivePvGraphics.Add(executivePVgraphic);
                    }
                }

                _context.Database.CloseConnection();
            }

            return executivePvGraphics;
        }


    }





}












