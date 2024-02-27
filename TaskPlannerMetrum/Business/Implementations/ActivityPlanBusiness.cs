using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.ActiviesScope;
using TaskPlannerMetrum.Repository.ActivityPlan;
using TaskPlannerMetrum.Repository.Generic;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ActivityPlanBusiness : IActivityPlanBusiness
    {
        private readonly IActivityPlanRepository _activiesRepository;

        public ActivityPlanBusiness(IActivityPlanRepository activiesRepository)
        {
            _activiesRepository = activiesRepository;
        }

        public bool Create(ActivePlanList activityPlan)
        {

            ActivityPlan task = new ActivityPlan();

            try
            {
                foreach (var item in activityPlan.ExecutorTeamID)
                {

                    task.ID = activityPlan.ID;
                    task.PlannerTeamID = activityPlan.PlannerTeamID;
                    task.Status = "5";
                    task.WorkspaceID = 1;
                    task.ContractID = activityPlan.ContractID;
                    task.ActivitiesScopeListID = activityPlan.ActivitiesScopeListID;
                    task.ScheduledDate = activityPlan.ScheduledDate;
                    task.PlannedManHour = activityPlan.PlannedManHour;
                    task.ExecutorTeamID = item;
                    task.NotesFromPlanner = activityPlan.NotesFromPlanner;
                    task.ExecutedManHour = activityPlan.ExecutedManHour;
                    task.NotesFromExecutor = activityPlan.NotesFromExecutor;
                    task.IsRework = activityPlan.IsRework;
                    task.DepartamentID = activityPlan.DepartamentID;
                    task.TaskDescription = activityPlan.TaskDescription;
                  
                    _activiesRepository.Create(task);

                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }



        public bool DeleteId(int id)
        {
            if (_activiesRepository.DeleteId(id) == true)
            {
                return true;
            }
            return false;
        }

        public dynamic GetActivityPlan(string activityId)
        {
            var activitList = _activiesRepository.FindAllTaskByProject(activityId);
            int totalRecords = activitList.Count();
            var result = new
            {
                activitList = activitList,
                totalRecords = totalRecords
            };


            return result;
        }

        public dynamic GetExecutorPlan(string projectId)
        {
            return _activiesRepository.GetExecutorPlan(projectId);
        }

        public dynamic TasksByProject(string projectId, int? page, int? size, string searchExecutor)
        {
            var activitList = _activiesRepository.FindAllTaskByProject(projectId).OrderBy(s => s.ScheduledDate).AsEnumerable();
            int totalRecords = activitList.Count();
            if (!string.IsNullOrEmpty(searchExecutor))
            {
                activitList = activitList.Where(a => a.ExecutorName.Contains(searchExecutor)).AsQueryable();
                totalRecords = activitList.Count();
            }

            if (page.HasValue)
            {
                activitList = activitList.OrderBy(x => x.ScheduledDate).Skip((page.Value - 1) * size.Value).Take(size.Value);
            }
            var result = new
            {
                activitList = activitList,
                totalRecords = totalRecords
            };


            return result;
        }
        public dynamic TasksByUser(string userId, int? page, int? size, string searchExecutor)
        {

            var activitList = _activiesRepository.FindAllTaskByUser(userId).OrderByDescending(s => s.ScheduledDate).AsQueryable();
            int totalRecords = activitList.Count();
            if (!string.IsNullOrEmpty(searchExecutor))
            {
                activitList = activitList.Where(a => a.ExecutorName.Contains(searchExecutor)).AsQueryable();
                totalRecords = activitList.Count();
            }

            if (page.HasValue)
            {
                activitList = activitList.OrderByDescending(x => x.ScheduledDate).Skip((page.Value - 1) * size.Value).Take(size.Value);
            }
            var result = new
            {
                activitList = activitList,
                totalRecords = totalRecords
            };

            return result;
        }

        public dynamic LoadTaskUsers(string userId, string searchExecutor)
        {

            var activitList = _activiesRepository.FindAllTaskByUser(userId).OrderByDescending(s => s.ScheduledDate).AsQueryable();
            int totalRecords = activitList.Count();
            if (!string.IsNullOrEmpty(searchExecutor))
            {
                activitList = activitList.Where(a => a.ExecutorName.Contains(searchExecutor)).AsQueryable();
                totalRecords = activitList.Count();
            }


            var result = new
            {
                activitList = activitList,
                totalRecords = totalRecords
            };

            return result;

        }


        public bool UpdateActivityPlan(vActivityPlan activityPlan)
        {
            return _activiesRepository.UpdateActivityPlan(activityPlan);
        }

        public string GetNamePorject(int id)
        {
            return _activiesRepository.GetNameProject(id);
        }

        public dynamic GetDepartamentProject(int id)
        {
            return _activiesRepository.GetDepartamentProject(id);
        }

        public dynamic GetUserTask(int departamentID)
        {
            return _activiesRepository.GetUserTask(departamentID);
        }

        public dynamic GetDepTask(UserDepForTask deptask)
        {
            return _activiesRepository.GetDepTask(deptask);
        }

        public bool DuplicateTask(Model.DuplicatTask activityPlan)
        {
            return _activiesRepository.DuplicateTask(activityPlan);
        }

        public bool UpdateRating(RatingUpdate updatRating)
        {
            return _activiesRepository.UpdateRating(updatRating);
        }

        public dynamic GetUserforTask(int id)
        {
            return _activiesRepository.GetUserforTask(id);
        }

        public bool UpdateNotes(int taskID, string notesExecut, string notesPlanned, string identifier)
        {
            return _activiesRepository.UpdateNotes(taskID, notesExecut, notesPlanned, identifier);
        }

        public string UpdateTaskDescription(int taskID, string taskDescription)
        {
            return _activiesRepository.UpdateTaskDescription(taskID, taskDescription);
        }


    }
}
