using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Repository.UserHourCostRepository
{
    public interface IUserHourCostRepository
    {
        // Métodos existentes
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

        // Novos métodos para operações em lote
        void CreateUserHourCostsBulk(List<UserHourCosts> userHourCosts);
        void UpdateUserHourCostsBulk(List<UserHourCosts> userHourCosts);
        void UpdateUsersBulk(List<User> users);

    
    }

}


