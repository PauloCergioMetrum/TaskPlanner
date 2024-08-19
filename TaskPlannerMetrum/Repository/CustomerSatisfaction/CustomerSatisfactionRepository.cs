using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using System.Linq;
using Memt.Logger;

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
                                ClientRating = reader.IsDBNull(reader.GetOrdinal("ClientRating")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientRating")), 
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


        public bool CreateCustomerFeedback(  int contractID, string clientResponse, int? clientRating, string receivedComplaint)
        {
            try
            {
                var newCustomerFeedback = new Satisfaction_Customer
                {
                 
                    ContractID = contractID,
                    ClientResponse = clientResponse,
                    ClientRating = clientRating,
                    ReceivedComplaint = receivedComplaint
                    
                };

                _context.Satisfaction_Customer.Add(newCustomerFeedback);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Error while saving: {ex.Message}, Inner Exception: {ex.InnerException?.Message}", ELoggerType.Debug);
                throw; 
            }
        }

    }
}

