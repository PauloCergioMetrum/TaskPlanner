using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface IUserHourCostBusiness
    {
        bool CreateHourCost(UserHourCosts userHourCos);

        bool DeleteHourCost(string ID);

        bool UpdateHourCost(UserHourCosts userHourCost);

        List<UserHourCosts> ListUserHoursCost(int userID);











      




    }

}

