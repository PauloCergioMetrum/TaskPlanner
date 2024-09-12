using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface IUserHourCostBusiness
    {
        bool CreateHourCost(UserHourCosts userHourCos);

        bool DeleteHourCost(string ID);

        bool UpdateHourCost(UserHourCosts userHourCost);

        public List<UserHourCostsDTO> ListUserHoursCost(int userID);



        Task<bool> CreatHoursCostByExcel(IFormFile excelFile, DateTime startDate, DateTime endDate);



    }

}

