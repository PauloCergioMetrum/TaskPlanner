

using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;

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
            catch (Exception ex)
            {

                throw;
            }
        }


        public bool UpdateUserHourCost(UserHourCosts userHourCost)
        {
            try
            {
                var UpdateUserHourCost = _context.UserHourCosts.Update(userHourCost);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }



}

