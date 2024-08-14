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

        public List<ExecutiveDto> GetPvExecutiveTable(string inspectorIDs = null, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            List<ExecutiveDto> pvExecutiveList = new List<ExecutiveDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[GetPvExecutiveTable]";
                command.CommandType = CommandType.StoredProcedure;

                // Adicionando os parâmetros corretamente
                command.Parameters.Add(new SqlParameter("@InspectorID", SqlDbType.NVarChar, 255) { Value = (object)inspectorIDs ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = (object)StartDate ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = (object)EndDate ?? DBNull.Value });

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int index = 0;
                        var pvExecutive = new ExecutiveDto
                        {
                            InspectorId = reader.IsDBNull(index) ? 0 : reader.GetInt32(index++),
                            InspectorName = reader.IsDBNull(index) ? null : reader.GetString(index++),
                            CompanyId = reader.IsDBNull(index) ? 0 : reader.GetInt32(index++),
                            WorkspaceName = reader.IsDBNull(index) ? null : reader.GetString(index++),
                            BusinessUnit = reader.IsDBNull(index) ? null : reader.GetString(index++),
                            SalesOrder = reader.IsDBNull(index) ? null : reader.GetString(index++),
                            ClientId = reader.IsDBNull(index) ? 0 : reader.GetInt32(index++),
                            ClientName = reader.IsDBNull(index) ? null : reader.GetString(index++),
                            SalesDescription = reader.IsDBNull(index) ? null : reader.GetString(index++),
                            ItemValue = reader.IsDBNull(index) ? 0 : reader.GetDouble(index++),
                            InvoicedValue = reader.IsDBNull(index) ? 0 : reader.GetDouble(index++),
                            ExpectedInvoiceMonth = reader.IsDBNull(index) ? (DateTime?)null : reader.GetDateTime(index++),
                            InvoicedDate = reader.IsDBNull(index) ? (DateTime?)null : reader.GetDateTime(index++),
                            Status = reader.IsDBNull(index) ? null : reader.GetString(index++)
                        };

                        pvExecutiveList.Add(pvExecutive);
                    }
                }

                _context.Database.CloseConnection();
            }

            return pvExecutiveList;
        }

       
       
    }
}
