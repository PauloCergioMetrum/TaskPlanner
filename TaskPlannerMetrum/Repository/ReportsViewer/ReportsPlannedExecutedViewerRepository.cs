


using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

                //list.Sort((d1, d2) => d1.ScheduledDate.CompareTo(d2.ScheduledDate));



                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}



