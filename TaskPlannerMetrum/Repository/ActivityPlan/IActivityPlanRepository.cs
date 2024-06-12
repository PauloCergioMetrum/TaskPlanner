using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ActivityPlan
{
    public interface IActivityPlanRepository
    {
        public dynamic GetExecutorPlan(string projectId);

        public bool Create(TaskPlannerMetrum.Model.ActivityPlan activityPlan);

        public List<vActivePlans> FindAllTaskByProject(string projectId);
        public vActivityPlan GetActivityPlanById(string activityPlanId);
        public List<vActivePlans> FindAllTaskByUser(string userId);
        public bool UpdateActivityPlan(vActivityPlan activityPlan);

        public bool DeleteId(int id);

        public string GetNameProject(int id);

        public dynamic GetDepartamentProject(int id);

        public dynamic GetUserTask(int departamentID);

        public dynamic GetDepTask(UserDepForTask deptask);


        public bool DuplicateTask(Model.DuplicatTask activityPlan);


        public bool UpdateRating(RatingUpdate updatetask);

        public dynamic GetUserforTask(int id);

        public bool UpdateNotes(int taskID, string notesExecut, string notesPlanned, string identifier);

        public string UpdateTaskDescription(int taskID, string taskDescription);

        public List<vActivePlanBusinessUnit> GetBusinessUnitByContract(int ContractID);

        public List<HoursDTO> ExecutorHourForPeriod(HoursExecutorDTO executors);

        // LOCAÇÃO DE EQUIPAMENTOS
        public List<Equipment> GetAllEquipment();


    }
}
