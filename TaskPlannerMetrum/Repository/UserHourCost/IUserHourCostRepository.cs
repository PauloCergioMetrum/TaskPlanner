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


    }
}


