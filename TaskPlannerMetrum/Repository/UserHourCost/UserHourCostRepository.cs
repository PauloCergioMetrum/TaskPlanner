

using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OfficeOpenXml.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;

using TaskPlannerMetrum.Model.DTO;

using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.UserHourCostRepository
{
    public class UserHourCostRepository : IUserHourCostRepository
    {

        private MSSQLContext _context;
        public UserHourCostRepository(MSSQLContext context)
        {
            _context = context;
        }
        public bool CreateUserHourCost(UserHourCosts newCost)
        {
        

            try
            {
   

                var conflictingCost = _context.UserHourCosts
                    .FirstOrDefault(u => u.UserID == newCost.UserID &&
                                         u.StartDate <= newCost.EndDate &&
                                         u.EndDate >= newCost.StartDate);

                if (conflictingCost != null)
                {
                    conflictingCost.EndDate = newCost.StartDate.AddDays(-1);
                    _context.UserHourCosts.Update(conflictingCost);
                }

                _context.UserHourCosts.Add(newCost);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar custo horário: {ex.Message}");
                return false;
            }
            
        }


        public bool DeleteUserHourCost(string ID)
        {
            try
            {
                var userHourCost = _context.UserHourCosts.Where(u => u.ID == ID).FirstOrDefault();
                if (userHourCost != null)
                {
                    _context.UserHourCosts.Remove(userHourCost);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ExistUserCost(string id)
        {
            return _context.UserHourCosts.Any(u => u.ID == id);
        }

        public List<UserHourCosts> GetAllUserHourCost(int userID)
        {
            try
            {
                var userHourCostList = _context.UserHourCosts.Where(u => u.UserID == userID).OrderBy(u => u.EndDate).ToList();


                return userHourCostList;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool UpdateUserHourCost(UserHourCosts userHourCost)
        {
            string triggerName = "trg_UpdateFunctionNameOnUserHourCosts";

            try
            {
                // Desabilitar o trigger
                _context.Database.ExecuteSqlRaw($"DISABLE TRIGGER {triggerName} ON dbo.UserHourCosts;");


                var existingUserHourCost = _context.UserHourCosts
                    .FirstOrDefault(s => s.ID == userHourCost.ID);

                if (existingUserHourCost == null)
                {

                    return false;
                }


                existingUserHourCost.HourCost = userHourCost.HourCost;
                existingUserHourCost.StartDate = userHourCost.StartDate;
                existingUserHourCost.EndDate = userHourCost.EndDate;
                existingUserHourCost.FunctionName = userHourCost.FunctionName;


                _context.Entry(existingUserHourCost).State = EntityState.Modified;
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar custo horário: {ex.Message}");
                return false;
            }
            finally
            {
                // Reativar o trigger
                _context.Database.ExecuteSqlRaw($"ENABLE TRIGGER {triggerName} ON dbo.UserHourCosts;");
            }
        }









        public List<UserHourCosts> GetAllHours()
        {
            return _context.UserHourCosts.ToList();
        }

        public List<Functions> GetAllFunction()
        {

            var functions = _context.Functions.ToList();
            return functions;
        }

        public string RemoveAccents(string text)
        {
            text = text.Replace("-", "");
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in text)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToUpper();
        }
        public List<Management> GetAllManagement()
        {
            return _context.Management.ToList();
        }

        public bool updateUser(int UserID, int FunctionID, string managementName)
        {

            User UserUpdate = _context.Users.FirstOrDefault(i => i.Id == UserID);
            UserUpdate.FunctionID = FunctionID;
            if (UserUpdate == null)
            {

                return false;
            }
            string managementNameNormalized = NormalizeString(managementName).ToUpper();
            var managementID = _context.Management
                .Where(m => EF.Functions.Collate(m.Name.ToUpper(), "SQL_Latin1_General_CP1_CI_AI") == managementNameNormalized)
                .Select(m => m.ID)
                .FirstOrDefault();
            if (managementID != 0)
            {
                UserUpdate.ManagementID = managementID;
            }

            _context.SaveChanges();
            return true;
        }


        public string NormalizeString(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }

            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }


        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }




  


        public void CreateUserHourCostsBulk(List<UserHourCosts> userHourCosts)
        {
            foreach (var userHourCost in userHourCosts)
            {
                if (userHourCost.CreationDate == null)
                {
                    userHourCost.CreationDate = userHourCost.StartDate.Date + DateTime.Now.TimeOfDay;
                }
            }


            _context.UserHourCosts.AddRange(userHourCosts);
            _context.SaveChanges();
        }


        public void UpdateUserHourCostsBulk(List<UserHourCosts> userHourCosts)
        {
            foreach (var userHourCost in userHourCosts)
            {

                var existingEntity = _context.UserHourCosts.FirstOrDefault(u =>
                    u.UserID == userHourCost.UserID &&
                    u.StartDate == userHourCost.StartDate &&
                    u.EndDate == userHourCost.EndDate &&
                    u.HourCost == userHourCost.HourCost &&
                    u.FunctionName == userHourCost.FunctionName);

                if (existingEntity == null)
                {

                    var newUserHourCost = new UserHourCosts
                    {
                        UserID = userHourCost.UserID,
                        HourCost = userHourCost.HourCost,
                        StartDate = userHourCost.StartDate,
                        EndDate = userHourCost.EndDate,
                        FunctionName = userHourCost.FunctionName,
                        ID = Guid.NewGuid().ToString()
                    };

                    _context.UserHourCosts.Add(newUserHourCost);
                }

            }

            _context.SaveChanges();
        }





        public void UpdateUsersBulk(List<User> users)
        {
            _context.Users.UpdateRange(users);
            _context.SaveChanges();
        }


        public async Task ExecuteSqlCommandAsync(string sql)
        {
            await _context.Database.ExecuteSqlRawAsync(sql);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<List<UserHourCosts>> GetLatestFunctionByAllUsersAsync()
        {
            return await _context.UserHourCosts
                .GroupBy(u => u.UserID)
                .Select(g => g.OrderByDescending(u => u.StartDate)
                              .ThenByDescending(u => u.CreationDate)
                              .FirstOrDefault())
                .ToListAsync();
        }

        public List<UserHourCosts> GetUserCostsByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            return _context.UserHourCosts
                .Where(u => u.UserID == userId &&
                            u.StartDate <= endDate &&
                            u.EndDate >= startDate)
                .OrderBy(u => u.StartDate)
                .ToList();
        }


    }




}






