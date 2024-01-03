using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Business
{
    public interface IActivityPlanBusiness
    {
        public dynamic GetExecutorPlan(string projectId);
        public bool Create(ActivePlanList activityPlan);
        public dynamic TasksByProject(string projectId, int? page, int? size, string searchExecutor);
        public dynamic TasksByUser(string userId, int? page, int? size, string searchExecutor);
        public dynamic GetActivityPlan(string activityId);
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







    }
}
