using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IActivityPlanBusiness
    {
        public dynamic GetExecutorPlan(string projectId);
        public bool Create(ActivePlanList activityPlan);
        public dynamic TasksByProject(int MilestonesID, int? page, int? size, string searchExecutor, int ContractID);

        public dynamic TasksByUser(string userId, int? page, int? size, string searchExecutor);
        public dynamic GetActivityPlan(int MilestonesID, int ContractID );
        //public List<MilestoneDetailDTO> FindAllMilestonesByContract(int contractID);



        Task<List<MilestoneDetailDTO>> FindAllMilestonesByContractAsync(int contractID);

        public bool UpdateActivityPlan(TaskPlannerMetrum.Model.ModelViews.vActivityPlan activityPlan);
        public bool DeleteId(int id);

        public dynamic LoadTaskUsers(string userId, string searchExecutor);

        public string GetNamePorject(int id);

        public dynamic GetDepartamentProject(int id);

        public dynamic GetUserTask(int departamentID);

        public dynamic GetDepTask(UserDepForTask deptask);


        public bool DuplicateTask(Model.DuplicatTask activityPlan);

        public bool UpdateRating(Model.RatingUpdate updatetask);

        public dynamic GetUserforTask(int id);

        public bool UpdateNotes(int taskID, string notesExecut, string notesPlanned, string identifier);

        public string UpdateTaskDescription(int taskID, string taskDescription);
        public List<vActivePlanBusinessUnit> GetBusinessUnitByContract(int ContractID);


        public List<HoursDTO> ExecutorHourForPeriod(HoursExecutorDTO activityPlan);


        public List<Equipment> GetAllEquipment();


        public List<GetEquipamentAvaibilaity> GetEquipamentAvaibilaities(int EquipamentID, DateTime StartDate, DateTime EndDate);

    }
}
