




using log4net.Util;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration.UserSecrets;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.Generic;
using static TaskPlannerMetrum.Model.DTO.HoursDTO;

namespace TaskPlannerMetrum.Repository.ActivityPlan
{
    public class ActivityPlanRepository : IActivityPlanRepository
    {
        private MSSQLContext _context;

        public ActivityPlanRepository(MSSQLContext context) { _context = context; }

        public bool Create(Model.ActivityPlan activityPlan)
        {
            try
            {


                int pmtemaID = _context.Team.Where(t => t.UserID == activityPlan.PlannerTeamID).Select(t => t.ID).FirstOrDefault();
                _context.Add(new Model.ActivityPlan
                {

                    ActivitiesScopeListID = activityPlan.ActivitiesScopeListID,
                    ExecutedManHour = activityPlan.ExecutedManHour,
                    ExecutorTeamID = _context.Team.Where(i => i.UserID == activityPlan.ExecutorTeamID).Select(i => i.ID).FirstOrDefault(),
                    NotesFromExecutor = activityPlan.NotesFromExecutor,
                    NotesFromPlanner = activityPlan.NotesFromPlanner,
                    PlannedManHour = activityPlan.PlannedManHour,
                    PlannerTeamID = pmtemaID,
                    TaskDescription = activityPlan.TaskDescription,
                    ContractID = activityPlan.ContractID,
                    ScheduledDate = activityPlan.ScheduledDate,
                    Status = activityPlan.Status,
                    WorkspaceID = activityPlan.WorkspaceID,
                    IsRework = activityPlan.IsRework,
                    DepartamentID = activityPlan.DepartamentID,
                    BusinessUnit = activityPlan.BusinessUnit,
                    MilestonesID = activityPlan.MilestonesID,    
                });
                _context.SaveChanges();


                CreateAllRatings(new UserTask
                {
                    ContractID = activityPlan.ContractID,
                    ActivitiesScopeListID = activityPlan.ActivitiesScopeListID,
                    UserID = activityPlan.ExecutorTeamID,
                    Rating = 0,
                    ActivityPlanID = _context.ActivityPlan.OrderBy(i => i.ID).Select(i => i.ID).LastOrDefault()
                },
                (new RatingProject
                {
                    ProjectID = activityPlan.ContractID,

                    UserID = activityPlan.ExecutorTeamID,

                }
                ));




                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateAllRatings(UserTask RatingforTasks, RatingProject ratingProjects)
        {
            if (createTaskRating(RatingforTasks) == true && createRatingProjects(ratingProjects) == true) return true; return false;

        }

        public bool createRatingProjects(Model.RatingProject ratingProject)
        {
            var projectExist = _context.RatingProject.Where(r => r.UserID == ratingProject.UserID && r.ProjectID == ratingProject.ProjectID).FirstOrDefault();

            if (projectExist == null)
            {
                _context.RatingProject.Add(ratingProject);
                _context.SaveChanges();
                creatDescriptionExecuter(ratingProject);



                return true;
            }
            else
            {
                return false;
            }

        }

        public bool creatDescriptionExecuter(RatingProject userRating)
        {

            var leader = _context.DepartmentProjects.Where(t => t.ContractID == userRating.ProjectID && t.TechLeaderID == userRating.UserID).FirstOrDefault();
            List<RatingDescription> descriptions = _context.RatingDescription.ToList();
            var ratingProjects = _context.RatingProject.Where(p => p.ProjectID == userRating.ProjectID && p.UserID == userRating.UserID).Select(i => i.ID).FirstOrDefault();
            try
            {
                if (leader != null)
                {
                    descriptions = descriptions.Where(t => t.Type == "L").ToList();
                    foreach (RatingDescription description in descriptions)
                    {
                        _context.Rating.Add(new Model.Rating
                        {

                            RatingProjectID = ratingProjects,
                            RatingDescriptionID = description.ID,
                            Value = 0

                        });
                        _context.SaveChanges();

                    }

                }
                else
                {
                    descriptions = descriptions.Where(t => t.Type == "E").ToList();
                    foreach (RatingDescription description in descriptions)
                    {
                        _context.Rating.Add(new Model.Rating
                        {

                            RatingProjectID = ratingProjects,
                            RatingDescriptionID = description.ID,
                            Value = 0

                        });
                        _context.SaveChanges();

                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool createTaskRating(UserTask task)
        {
            try
            {
                _context.UserTask.Add(task);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<vActivePlans> FindAllTaskByProject(string projectId)
        {
            return _context.vActivePlans.Where(c => c.ContractID == Convert.ToInt32(projectId)).OrderBy(d => d.ScheduledDate).ToList();
        }





        public string GetNameProject(int id)
        {
            List<Model.Projects> activityPlan = new List<Model.Projects>();

            var getRow = _context.Projects.FirstOrDefault(i => i.ID == id);

            return getRow.Name;
        }

        public List<vActivePlans> FindAllTaskByUser(string userId)
        {
            return _context.vActivePlans.Where(u => u.ExecutorTeamID == Convert.ToInt32(userId)).ToList();
        }



        public static int SetId(List<int> id)
        {

            int retorno = 0;
            foreach (int item in id)
            {
                retorno = item;

            }
            return retorno;

        }

        public static string GetStatusName(string status, DateTime ScheduledDate)
        {
            if (IsAtrasada(status, ScheduledDate))
            {
                return "Atrasada";
            }

            switch (status)
            {
                case "5":
                    return "Não Iniciada";
                case "1":
                    return "Concluída";
                case "2":
                    return "Em progresso";
                case "3":
                    return "Bloqueada";
                case "4":
                    return "Cancelada";
                default:
                    return "Não iniciada";
            }
        }

        private static bool IsAtrasada(string status, DateTime ScheduledDate)
        {



            if (status == "5" || status == "2")
            {

                DateTime currentDate = DateTime.Now;

                if (currentDate > ScheduledDate)
                {

                    return true;
                }
                return false;
            }

            return false;
        }




        public dynamic GetExecutorPlan(string projectId)
        {
            List<ExecutorDTO> ExecutorPlans = new List<ExecutorDTO>();
            try
            {
                var departments = _context.DepartmentProjects.Where(p => p.ContractID == Convert.ToInt32(projectId)).ToList();
                foreach (var department in departments)
                {
                    var usersTeam = _context.Team.Where(u => u.DepartmentID == department.DepartmentID).ToList();

                    foreach (var user in usersTeam)
                    {
                        var userExecutor = _context.Users.Where(u => u.Id == user.UserID).FirstOrDefault();

                        if (userExecutor != null)
                        {

                            ExecutorPlans.Add(new ExecutorDTO { Name = userExecutor.FullName, pmTeamID = user.ID });
                        }

                    }

                }

                return ExecutorPlans;
            }
            catch
            {
                return null;
            }
        }

        public vActivityPlan GetActivityPlanById(string activityPlanId)
        {

            var activity = _context.ActivityPlan.FirstOrDefault(a => a.ID == Convert.ToInt32(activityPlanId));

            if (activity == null)
            {

                return null;
            }

            var activityPlan = new vActivityPlan
            {

                ID = Convert.ToInt32(activityPlanId),
                ExecutedManHour = activity.ExecutedManHour,
                NotesFromExecutor = activity.NotesFromExecutor,
                statusName = GetStatusName(activity.Status.ToString(), activity.ScheduledDate),
                Status = activity.Status,
                TaskDescription = activity.TaskDescription,
                ScheduledDate = activity.ScheduledDate,
                MilestonesID = activity.MilestonesID,
                BusinessUnit = activity.BusinessUnit,
                ProjectName = _context.Projects
                    .Where(p => p.ID == activity.ContractID)
                    .Select(p => p.Name)
                    .FirstOrDefault(),

            };

            return activityPlan;
        }




        public bool UpdateActivityPlan(vActivityPlan activityPlan)
        {
            var newActivityPlan = _context.ActivityPlan.FirstOrDefault(a => a.ID == activityPlan.ID);

            if (newActivityPlan == null)
            {
                return false;
            }

            if (newActivityPlan.ExecutorTeamID != activityPlan.ExecutorTeamID)
            {
                updateRatingExecutor(activityPlan.ExecutorTeamID, newActivityPlan.ExecutorTeamID, newActivityPlan.ContractID);
            }

            if (activityPlan.ExecutedManHour == 0 && (activityPlan.Status != "4" && activityPlan.Status != "3" && activityPlan.Status != "9"))
            {

                activityPlan.Status = "5";
            }


            newActivityPlan.ExecutedManHour = activityPlan.ExecutedManHour;
            newActivityPlan.ScheduledDate = activityPlan.ScheduledDate;
            newActivityPlan.NotesFromExecutor = activityPlan.NotesFromExecutor;
            newActivityPlan.NotesFromPlanner = activityPlan.NotesFromPlanner;
            newActivityPlan.TaskDescription = activityPlan.TaskDescription;
            newActivityPlan.Status = activityPlan.Status;
            newActivityPlan.BusinessUnit = activityPlan.BusinessUnit;
            newActivityPlan.PlannedManHour = activityPlan.PlannedManHour;
            newActivityPlan.ExecutorTeamID = activityPlan.ExecutorTeamID == 0 ? newActivityPlan.ExecutorTeamID : activityPlan.ExecutorTeamID;
            newActivityPlan.MilestonesID = activityPlan.MilestonesID;   

            _context.ActivityPlan.Update(newActivityPlan);
            _context.SaveChanges();



            return true;
        }







        public void updateRatingExecutor(int newUserID, int oldUserID, int contractID)
        {
            var userIDNew = _context.Team.Where(i => i.ID == newUserID).Select(u => u.UserID).FirstOrDefault();
            var userIDOld = _context.Team.Where(i => i.ID == oldUserID).Select(u => u.UserID).FirstOrDefault();

            var taskOldUserID = _context.ActivityPlan.Where(c => c.ContractID == contractID && c.ExecutorTeamID == oldUserID).ToList();
            if (taskOldUserID.Count() <= 1)
            {
                deleteProjectRating(contractID, userIDOld);
                var getUserRatingProject = _context.RatingProject.Where(c => c.ProjectID == contractID && c.UserID == userIDNew).FirstOrDefault();
                if (getUserRatingProject == null)
                {
                    createRatingProjects(new RatingProject
                    {
                        ProjectID = contractID,
                        UserID = userIDNew,
                    });
                }
                else
                {

                    getUserRatingProject.UserID = userIDNew;
                    _context.Update(getUserRatingProject);
                    _context.SaveChanges();
                    //ResetRating(getUserRatingProject.ID);

                }

            }
            else
            {
                var getUserRatingProject = _context.RatingProject.Where(c => c.ProjectID == contractID && c.UserID == userIDNew).FirstOrDefault();
                if (getUserRatingProject == null)
                {
                    _context.RatingProject.Add(new RatingProject
                    {
                        ProjectID = contractID,
                        UserID = userIDNew,
                    });
                    _context.SaveChanges();
                    creatDescriptionExecuter(new RatingProject
                    {
                        ProjectID = contractID,
                        UserID = userIDNew,
                    });
                }
            }

        }




        public void deleteProjectRating(int contractID, int UserID)
        {
            var getRatingProjectID = _context.RatingProject.Where(c => c.ProjectID == contractID && c.UserID == UserID).FirstOrDefault();
            if (getRatingProjectID != null)
            {
                var getRating = _context.Rating.Where(r => r.RatingProjectID == getRatingProjectID.ID).ToList();
                foreach (var rating in getRating)
                {
                    _context.Rating.Remove(rating);
                    _context.SaveChanges();
                }
                _context.RatingProject.Remove(getRatingProjectID);
                _context.SaveChanges();
            }
        }









        public bool DeleteId(int id)
        {
            var deleteActivyplan = _context.ActivityPlan.FirstOrDefault(i => i.ID == id);
            var deleteUserTask = _context.UserTask.FirstOrDefault(i => i.ActivityPlanID == id);
            if (deleteActivyplan != null)
            {

                _context.UserTask.Remove(deleteUserTask);
                _context.SaveChanges();
                _context.ActivityPlan.Remove(deleteActivyplan);
                _context.SaveChanges();
                return true;



            }
            return false;
        }
        public dynamic GetDepartamentProject(int id)
        {
            List<dynamic> departamentos = new List<dynamic>();
            var projects = _context.DepartmentProjects.Where(i => i.ContractID == id).Select(d => d.DepartmentID).Distinct().ToList();
            foreach (var project in projects)
            {
                var departament = new
                {
                    Id = project,
                    Name = _context.Department.Where(i => i.ID == project).Select(n => n.Name).FirstOrDefault(),
                };
                departamentos.Add(departament);
            }
            return (departamentos);
        }

        public dynamic GetUserTask(int GroupTaskID)
        {

            List<dynamic> usersdep = new List<dynamic>();
            var users = _context.Users.Where(d => d.DepartmentId == GroupTaskID).ToList();
            foreach (var u in users)
            {
                var allusers = new
                {
                    id = u.Id,
                    name = u.FullName
                };
                usersdep.Add(allusers);
            }
            List<dynamic> tasks = new List<dynamic>();
            var task = _context.ActivitiesScopeList.Where(d => d.GroupTaskID == GroupTaskID).ToList();
            foreach (var t in task)
            {
                var alltask = new
                {
                    id = t.ID,
                    task = t.Description
                };
                tasks.Add(alltask);
            }
            var data = new
            {
                tasks = tasks,
                users = usersdep,

            };
            return data;

        }

        public dynamic GetDepTask(UserDepForTask deptask)
        {
            return _context.Users.Where(a => a.IsActive == true).OrderBy(i => i.FullName).Select(u => new { UserName = u.FullName, UserID = u.Id, ContractID = 0, IsActive = u.IsActive }).ToList();

        }

        public bool DuplicateTask(Model.DuplicatTask activityPlan)
        {
            var taskforduplicate = _context.ActivityPlan.Where(i => i.ID == activityPlan.ID).FirstOrDefault();

            _context.Add(new Model.ActivityPlan
            {
                ExecutorTeamID = taskforduplicate.ExecutorTeamID,
                PlannedManHour = activityPlan.PlannedManHour,
                ExecutedManHour = 0,
                ScheduledDate = activityPlan.ScheduledDate,
                WorkspaceID = taskforduplicate.WorkspaceID,
                ActivitiesScopeListID = taskforduplicate.ActivitiesScopeListID,
                Status = "5",
                ContractID = taskforduplicate.ContractID,
                DepartamentID = taskforduplicate.DepartamentID,
                PlannerTeamID = taskforduplicate.PlannerTeamID,
                NotesFromPlanner = taskforduplicate.NotesFromPlanner,
                TaskDescription = taskforduplicate.TaskDescription,
                IsRework = taskforduplicate.IsRework,
                NotesFromExecutor = taskforduplicate.NotesFromExecutor,
                MilestonesID = taskforduplicate.MilestonesID,   
            });
            _context.SaveChanges();


            createTaskRating(new UserTask
            {
                ContractID = taskforduplicate.ContractID,
                ActivitiesScopeListID = taskforduplicate.ActivitiesScopeListID,
                UserID = _context.Team.Where(i => i.ID == taskforduplicate.ExecutorTeamID).Select(u => u.UserID).FirstOrDefault(),
                ActivityPlanID = _context.ActivityPlan.OrderBy(i => i.ID).Select(i => i.ID).LastOrDefault(),
                Rating = 0
            });
            return true;




        }

        public bool UpdateRating(RatingUpdate updatetask)
        {

            var getrating = _context.UserTask.Where(i => i.ActivityPlanID == updatetask.ActivityPlanID).FirstOrDefault();
            getrating.Rating = updatetask.Rating;
            getrating.Support = updatetask.Support;
            _context.Update(getrating);
            _context.SaveChanges();
            return true;

        }

        public dynamic GetUserforTask(int id)
        {

            //id = _context.Team.Where(i => i.UserID == id).Select(u => u.ID).FirstOrDefault();
            return _context.GetActivityPlanDetailsByExecutorID(id);


        }









        public string getClientName(int id)
        {
            return _context.Clients.Where(i => i.Id == _context.Contracts.Where(a => a.id == id).Select(c => c.ClientID).FirstOrDefault()).Select(n => n.Name).FirstOrDefault();
        }

        public bool UpdateNotes(int taskID, string notesExecut, string notesPlanned, string identifier)
        {
            if (identifier == "E")
            {
                var updateNotes = _context.ActivityPlan.Where(i => i.ID == taskID).FirstOrDefault();
                updateNotes.NotesFromExecutor = notesExecut;
                _context.Update(updateNotes);
                _context.SaveChanges();
                return true;

            }
            if (identifier == "P")
            {
                var updateNotes = _context.ActivityPlan.Where(i => i.ID == taskID).FirstOrDefault();
                updateNotes.NotesFromPlanner = notesPlanned;
                _context.Update(updateNotes);
                _context.SaveChanges();
                return true;
            }


            else
            {
                return false;
            }

        }


        public string UpdateTaskDescription(int taskID, string taskDescription)
        {
            var updateDescription = _context.ActivityPlan.Where(i => i.ID == taskID).FirstOrDefault();
            updateDescription.TaskDescription = taskDescription;
            _context.Update(updateDescription);
            _context.SaveChanges();
            return taskDescription;
        }

        public List<vActivePlanBusinessUnit> GetBusinessUnitByContract(int ContractID)
        {
            var BusinessOptions = _context.vActivePlanBusinessUnit.Where(i => i.ContractID == ContractID).ToList();
            return BusinessOptions;
        }




        public List<HoursDTO> ExecutorHourForPeriod(HoursExecutorDTO executors)
        {
            string executorTeamIDs = executors.ExecutorsTeamID;
            DateTime startDate = executors.StartDate;
            DateTime endDate = executors.EndDate;
            double Hours = executors.Hours;
            bool IsOverAllocated = executors.IsOverAllocated;



            List<HoursDTO> hoursDto = new List<HoursDTO>();

            var hoursExecutorsList = _context.GetActivityPlanByExecutorTeamIDAndPeriod(executorTeamIDs, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), Hours.ToString());

            var executorDataList = hoursExecutorsList.Select(s => s.Executor).Distinct().ToList();

            var scheduleDataList = hoursExecutorsList.Select(s => s.ScheduledDate).Distinct().ToList();

 
            foreach (var schedule in scheduleDataList)
            {
                var executorList = hoursExecutorsList.Where(s => s.ScheduledDate == schedule).Select(s => s.Executor).Distinct().ToList();

                foreach (var executor in executorList)
                {
                    List<HoursByDay> hoursByDays = new List<HoursByDay>();

                    var hoursOnDateForExecutor = hoursExecutorsList
                        .Where(h => h.ScheduledDate == schedule && h.Executor == executor);

                    Dictionary<string, TimeSpan> totalPlannedHoursByProject = new Dictionary<string, TimeSpan>();

            
                    foreach (var hourOnDate in hoursOnDateForExecutor)
                    {
                        if (totalPlannedHoursByProject.ContainsKey(hourOnDate.Project))
                        {
                            totalPlannedHoursByProject[hourOnDate.Project] += TimeSpan.Parse(hourOnDate.PlannedManHours);
                        }
                        else
                        {
                            totalPlannedHoursByProject[hourOnDate.Project] = TimeSpan.Parse(hourOnDate.PlannedManHours);
                        }
                    }

            
                    foreach (var kvp in totalPlannedHoursByProject)
                    {
                        hoursByDays.Add(new HoursByDay
                        {
                            PlannedManHours = $"{(int)kvp.Value.TotalHours:00}:{kvp.Value.Minutes:00}",
                            Project = kvp.Key,
                       

                        });
                    }

                    TimeSpan totalHours = totalPlannedHoursByProject.Values
                        .Aggregate(TimeSpan.Zero, (total, time) => total.Add(time));

                    string resultTotalHours = $"{(int)totalHours.TotalHours:00}:{totalHours.Minutes:00}";

                    hoursDto.Add(new HoursDTO
                    {
                        Executor = executor,
                        ScheduledDate = schedule,
                        TotalPlannedHours = resultTotalHours,
                        Hours = hoursByDays,
                      




                    });
                }


            }
            return hoursDto;
        }



    }
}
