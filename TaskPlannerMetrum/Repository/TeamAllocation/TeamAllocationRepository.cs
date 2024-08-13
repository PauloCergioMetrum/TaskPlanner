using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.TeamAllocation
{
    public class TeamAllocationRepository : ITeamAllocationRepository
    {
        private readonly MSSQLContext _context;

        public TeamAllocationRepository(MSSQLContext context)
        {
            _context = context;
        }

        public List<TeamAllocationDTO> GetTeamAllocation(string businessUnit = null, DateTime? startDate = null, DateTime? endDate = null, string function = null, string project = null)
        {
            List<TeamAllocationDTO> teamAllocationList = new List<TeamAllocationDTO>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[GetTeamAllocationTable]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@BusinessUnit", SqlDbType.NVarChar, 100) { Value = (object)businessUnit ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = (object)startDate ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = (object)endDate ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@Function", SqlDbType.NVarChar, 100) { Value = (object)function ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@Project", SqlDbType.NVarChar, 100) { Value = (object)project ?? DBNull.Value });

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int index = 0;
                        var teamAllocation = new TeamAllocationDTO
                        {
                            ID = reader.IsDBNull(index) ? 0 : (int)reader.GetInt64(index++),
                            SaleOrder = reader.IsDBNull(index) ? null : reader.GetString(index++),
                            Client = reader.IsDBNull(index) ? null : reader.GetString(index++),
                            BusinessUnit = reader.IsDBNull(index) ? null : reader.GetString(index++),
                            PlannedHours = reader.IsDBNull(index) ? 0 : reader.GetDouble(index++),
                            ExecutedHours = reader.IsDBNull(index) ? 0 : reader.GetDouble(index++),
                            ScheduledDate = reader.IsDBNull(index) ? DateTime.MinValue : reader.GetDateTime(index++), // Agora lê como DateTime
                            Executor = reader.IsDBNull(index) ? null : reader.GetString(index++)
                        };

                        teamAllocationList.Add(teamAllocation);
                    }
                }

                _context.Database.CloseConnection();
            }

            return teamAllocationList;
        }


        public List<TeamAllocationDTO.TeamAllocationGraphicDTO> GetTeamAllocationGraphic(DateTime? startDate = null, DateTime? endDate = null, string functionName = null)
        {
            List<TeamAllocationDTO.TeamAllocationGraphicDTO> teamAllocationGraphicList = new List<TeamAllocationDTO.TeamAllocationGraphicDTO>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[GetTeamAllocationGraphic]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = (object)startDate ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = (object)endDate ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@FunctionName", SqlDbType.NVarChar, 255) { Value = (object)functionName ?? DBNull.Value });

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var teamAllocationGraphic = new TeamAllocationDTO.TeamAllocationGraphicDTO
                        {
                            FunctionName = reader.IsDBNull(0) ? null : reader.GetString(0),
                            Month = reader.IsDBNull(1) ? null : reader.GetString(1),
                            TotalPlannedHours = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                            TotalExecutedHours = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                            AvailableTime = reader.IsDBNull(4) ? 0 : reader.GetDouble(4)
                        };

                        teamAllocationGraphicList.Add(teamAllocationGraphic);
                    }
                }

                _context.Database.CloseConnection();
            }

            return teamAllocationGraphicList;
        }

        public List<TeamAllocationDTO.GetTeamAllocationGraphicFunctions> GetTeamAllocationGraphicFunctions()
        {
            List<TeamAllocationDTO.GetTeamAllocationGraphicFunctions> teamAllocationGraphicFunctions = new List<TeamAllocationDTO.GetTeamAllocationGraphicFunctions>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[GetTeamAllocationGraphicFunctions]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var function = new TeamAllocationDTO.GetTeamAllocationGraphicFunctions
                        {
                            SeniorityLevel = reader["SeniorityLevel"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        };

                        teamAllocationGraphicFunctions.Add(function);
                    }
                }

                _context.Database.CloseConnection();
            }

            return teamAllocationGraphicFunctions;
        }

    }
}
