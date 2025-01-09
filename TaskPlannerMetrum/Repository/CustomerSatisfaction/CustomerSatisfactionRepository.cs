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

        public List<ClientFeedbackDetailsView> ClientFeedbackDetailsModel(int contractID)
        {
             var ExixteContractID = _context.ClientFeedbackDetailsView.Where(a => a.ContractID == contractID).ToList();
            return ExixteContractID;
        }

        public bool CreateCustomerFeedback(DateTime? FeedbackDate, int contractID, string clientResponse, int? clientRating, string receivedComplaint , int? Status)
        {
            try
            {
                var existingFeedback = _context.Satisfaction_Customer
                                               .FirstOrDefault(f => f.ContractID == contractID);

                if (existingFeedback != null)
                {
                    existingFeedback.ClientResponse = clientResponse;
                    existingFeedback.ClientRating = clientRating;
                    existingFeedback.ReceivedComplaint = receivedComplaint;
                    existingFeedback.FeedbackDate = FeedbackDate; 
                    existingFeedback.status = Status;   
                }
                else
                {
                    var newCustomerFeedback = new Satisfaction_Customer
                    {
                        ContractID = contractID,
                        ClientResponse = clientResponse,
                        ClientRating = clientRating,
                        ReceivedComplaint = receivedComplaint,
                        FeedbackDate = FeedbackDate,
                        status = Status 
                    };


                    _context.Satisfaction_Customer.Add(newCustomerFeedback);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Error while saving: {ex.Message}, Inner Exception: {ex.InnerException?.Message}", ELoggerType.Debug);
                throw;
            }
        }


        public List<CustomerSastifaction> CustomerSastifaction()
        {
            List<CustomerSastifaction> contractDetailsList = new List<CustomerSastifaction>();

            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("[dbo].[CustomerSatisfactionTable]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var contractDetail = new CustomerSastifaction
                            {
                                ID = reader.GetInt64(reader.GetOrdinal("ID")),
                                ContractName = reader["ContractName"] as string,
                                ContractID = reader.GetInt32(reader.GetOrdinal("ContractID")),
                                Contact = reader["Contact"] as string,
                                LastExecutedDate = reader.IsDBNull(reader.GetOrdinal("LastExecutedDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("LastExecutedDate")),
                                LastContactDate = reader.IsDBNull(reader.GetOrdinal("LastContactDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("LastContactDate")),
                                FeedbackDate = reader.IsDBNull(reader.GetOrdinal("FeedbackDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("FeedbackDate")),
                                ClientName = reader["ClientName"] as string,
                                ClientRating = reader.IsDBNull(reader.GetOrdinal("ClientRating"))
                                    ? 0
                                    : reader.GetInt32(reader.GetOrdinal("ClientRating")),
                                ReceivedComplaint = reader["ReceivedComplaint"] as string,
                                StatusID = reader.GetInt32(reader.GetOrdinal("StatusID")),
                                BusinessUnit = reader["BusinessUnit"] as string,
                                VendorName = reader["VendorName"] as string,
                                InspectorName = reader["InspectorName"] as string,
                                StartDate = reader.IsDBNull(reader.GetOrdinal("StartDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                DateFineshed = reader.IsDBNull(reader.GetOrdinal("DateFineshed"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("DateFineshed")),


                                Status_satisfaction_Customer = reader.IsDBNull(reader.GetOrdinal("status_satisfaction_Customer"))
                            ? 0 
                            : reader.GetInt32(reader.GetOrdinal("status_satisfaction_Customer"))
                            };

                            contractDetailsList.Add(contractDetail);
                        }
                    }

                    connection.Close();
                }
            }

            return contractDetailsList;
        }




    }
}

