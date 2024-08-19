using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.CustomerSatisfaction
{
    public class CustomerSatisfactionRepository : ICustomerSatisfactionRepository
    {
        private readonly MSSQLContext _context;

        public CustomerSatisfactionRepository(MSSQLContext context)
        {
            _context = context;
        }

        public List<CustomerSatisfactionDTO> ClientFeedbackDetailsModel(int contractID)
        {
            List<CustomerSatisfactionDTO> feedbackDetailsList = new List<CustomerSatisfactionDTO>();

            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("[dbo].[GetClientFeedbackDetails]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@ContractID";
                    parameter.Value = contractID;
                    command.Parameters.Add(parameter);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var feedbackDetail = new CustomerSatisfactionDTO
                            {
                                ID = reader.GetInt64(reader.GetOrdinal("ID")), 

                                ContractName = reader["ContractName"] as string,
                                Contact = reader["Contact"] as string,
                                Scope = reader["Scope"] as string,
                                LastExecutedDate = reader.IsDBNull(reader.GetOrdinal("LastExecutedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LastExecutedDate")),
                                LastContactDate = reader.IsDBNull(reader.GetOrdinal("LastContactDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LastContactDate")),
                                ClientResponse = reader["ClientResponse"] as string,
                                ClientRating = reader.IsDBNull(reader.GetOrdinal("ClientRating")) ? 0 : Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("ClientRating"))), // Conversão correta
                                ReceivedComplaint = reader["ReceivedComplaint"] as string
                            };

                            feedbackDetailsList.Add(feedbackDetail);
                        }
                    }

                    connection.Close();
                }
            }

            return feedbackDetailsList;
        }

    }
}
