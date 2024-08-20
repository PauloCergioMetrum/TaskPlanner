

using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
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
        public bool CreateUserHourCost(UserHourCosts userHourCost)
        {
            try
            {
                var UpdateUserHourCost = _context.UserHourCosts.Add(userHourCost);
                _context.SaveChanges();
                return true;
            }
            catch
            {
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

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool UpdateUserHourCost(UserHourCosts userHourCost)
        {
            try
            {
                var existingUserHourCost = _context.UserHourCosts
                    .FirstOrDefault(s => s.StartDate == userHourCost.StartDate && s.EndDate == userHourCost.EndDate && userHourCost.UserID ==userHourCost.UserID);
                if (existingUserHourCost == null)
                {
                    return false;
                }
                existingUserHourCost.HourCost = userHourCost.HourCost;
                _context.Update(existingUserHourCost);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
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




    }



}






