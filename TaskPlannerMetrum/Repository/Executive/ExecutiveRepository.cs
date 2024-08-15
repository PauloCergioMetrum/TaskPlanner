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


        public List<ExecutiveDto> GetPvExecutiveTable(string inspectorIDs = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var pvExecutiveList = new List<ExecutiveDto>();

       
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[GetFiscalFilter]";            
                    command.Parameters.Add(new SqlParameter("@InspectorIDs", inspectorIDs ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@StartDate", startDate ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@EndDate", endDate ?? (object)DBNull.Value));               
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
            }

            return pvExecutiveList;
        }



    }
}
