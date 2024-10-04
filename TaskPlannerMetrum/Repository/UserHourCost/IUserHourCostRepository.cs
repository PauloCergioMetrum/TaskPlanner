using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Repository.UserHourCostRepository
{
    public interface IUserHourCostRepository
    {

        public bool CreateUserHourCost(UserHourCosts userHourCost);

        public bool DeleteUserHourCost(string ID);

        public bool UpdateUserHourCost(UserHourCosts userHourCost);

        List<UserHourCosts> GetAllUserHourCost(int userID);

        public bool ExistUserCost(string id);
        

        public List<User> GetAllUsers();

        public List<UserHourCosts> GetAllHours();


        public bool updateUser(int UserID, int FunctionID, string managementName);

        public List<Management> GetAllManagement();
        public List<Functions> GetAllFunction();




    }
}


