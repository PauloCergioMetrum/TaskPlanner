using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

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
                    ContractID = activityPlan.ContractID,
                    ScheduledDate = activityPlan.ScheduledDate,
                    Status = activityPlan.Status,
                    WorkspaceID = activityPlan.WorkspaceID,
                    IsRework = activityPlan.IsRework,
                    DepartamentID= activityPlan.DepartamentID,
                });
                _context.SaveChanges();


                createTaskRating(new UserTask
                {
                    ContractID= activityPlan.ContractID,
                    ActivitiesScopeListID=activityPlan.ActivitiesScopeListID,
                    UserID= activityPlan.ExecutorTeamID,
                    Rating=0,
                    ActivityPlanID = _context.ActivityPlan.OrderBy(i => i.ID).Select( i => i.ID).LastOrDefault()
                });


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

        public List<vActivityPlan> FindAllTaskByProject(string projectId)
        {

            List<vActivityPlan> _activityPlans = new List<vActivityPlan>();
            var taskPlans = _context.ActivityPlan.Where(a => a.ContractID == Convert.ToInt32(projectId)).ToList().OrderBy(d => d.ScheduledDate);

            foreach (var taskPlan in taskPlans)
            {

                var userid = _context.Team.Where(t => t.ID == taskPlan.ExecutorTeamID).Select(s => s.UserID).FirstOrDefault();
                _activityPlans.Add(new vActivityPlan
                {
                    ActivitiesScopeListID = taskPlan.ActivitiesScopeListID,
                    Description = _context.ActivitiesScopeList.Where(d => d.ID == Convert.ToInt32(taskPlan.ActivitiesScopeListID)).Select(s => s.Description).FirstOrDefault(),
                    ExecutedManHour = taskPlan.ExecutedManHour,
                    ExecutorName = _context.Users.Where(u => u.Id == userid).Select(s => s.FullName).FirstOrDefault(),
                    ExecutorTeamID = taskPlan.ExecutorTeamID,
                    NotesFromExecutor = taskPlan.NotesFromExecutor,
                    NotesFromPlanner = taskPlan.NotesFromPlanner,
                    PlannedManHour = taskPlan.PlannedManHour,
                    PlannerTeamID = taskPlan.PlannerTeamID,
                    ContractID = taskPlan.ContractID,
                    ScheduledDate = taskPlan.ScheduledDate,
                    Status = taskPlan.Status,
                    ID = taskPlan.ID,
                    statusName = GetStatusName(taskPlan.Status.ToString()),
                    IsRework = taskPlan.IsRework,
                    ProjectName = _context.Projects.Where(p => p.ID == taskPlan.ContractID).Select(p => p.Name).FirstOrDefault(),

                });
            }

            return _activityPlans;
        }

        public string GetNameProject(int id)
        {
            List<Model.Projects> activityPlan = new List<Model.Projects>();

            var getRow = _context.Projects.FirstOrDefault(i => i.ID==id);

            return getRow.Name;
        }

        public List<vActivityPlan> FindAllTaskByUser(string userId)
        {
            List<vActivityPlan> _activityPlans = new List<vActivityPlan>();
            var pmTeamId = _context.Team.Where(t => t.UserID == Convert.ToInt32(userId)).Select(s => s.ID).FirstOrDefault();
            foreach (var taskPlan in _context.ActivityPlan.Where(t => t.ExecutorTeamID == pmTeamId).ToList())
            {
                _activityPlans.Add(new vActivityPlan
                {
                    ActivitiesScopeListID = taskPlan.ActivitiesScopeListID,
                    Description = _context.ActivitiesScopeList.Where(d => d.ID == Convert.ToInt32(taskPlan.ActivitiesScopeListID)).Select(s => s.Description).FirstOrDefault(),
                    ExecutedManHour = taskPlan.ExecutedManHour,
                    ExecutorName = _context.Users.Where(u => u.Id == Convert.ToInt32(userId)).Select(s => s.FullName).FirstOrDefault(),
                    ExecutorTeamID = taskPlan.ExecutorTeamID,
                    NotesFromExecutor = taskPlan.NotesFromExecutor,
                    NotesFromPlanner = taskPlan.NotesFromPlanner,
                    PlannedManHour = taskPlan.PlannedManHour,
                    PlannerTeamID = taskPlan.PlannerTeamID,
                    ContractID = taskPlan.ContractID,
                    ScheduledDate = taskPlan.ScheduledDate,
                    Status = taskPlan.Status,
                    ID = taskPlan.ID,
                    statusName = GetStatusName(taskPlan.Status.ToString()),
                    ProjectName = _context.Projects.Where(p => p.ID == taskPlan.ContractID).Select(p => p.Name).FirstOrDefault(),
                    IsRework = taskPlan.IsRework,


                });
            }
            return _activityPlans;
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
        public static string GetStatusName(string status)
        {
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

            }
            return "Não iniciada";
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
            var actvity = _context.ActivityPlan.Where(a => a.ID == Convert.ToInt32(activityPlanId)).FirstOrDefault();
            var activityPaln = new vActivityPlan
            {
                ID = Convert.ToInt32(activityPlanId),
                ExecutedManHour = actvity.ExecutedManHour,
                NotesFromExecutor = actvity.NotesFromExecutor,
                statusName = GetStatusName(actvity.Status.ToString()),
                Status = actvity.Status,
                ScheduledDate = actvity.ScheduledDate,
                ProjectName = _context.Projects.Where(p => p.ID == actvity.ContractID).Select(p => p.Name).FirstOrDefault(),


            };
            return activityPaln;
        }



        public bool UpdateActivityPlan(vActivityPlan activityPlan)
        {
            
                var newActivityPaln = _context.ActivityPlan.Where(a => a.ID == activityPlan.ID).FirstOrDefault();
                newActivityPaln.Status = activityPlan.Status;
                newActivityPaln.ExecutedManHour = activityPlan.ExecutedManHour;
                newActivityPaln.ScheduledDate = activityPlan.ScheduledDate;
                newActivityPaln.NotesFromExecutor = activityPlan.NotesFromExecutor;
                newActivityPaln.NotesFromPlanner = activityPlan.NotesFromPlanner;
                newActivityPaln.PlannedManHour = activityPlan.PlannedManHour;

                newActivityPaln.ExecutorTeamID = activityPlan.ExecutorTeamID;


                _context.ActivityPlan.Update(newActivityPaln);
                _context.SaveChanges();
                return true;
            
            
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
            var task = _context.ActivitiesScopeList.Where(d => d.GroupTaskID== GroupTaskID).ToList();
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
            List<dynamic> Users = new List<dynamic>();
            var userid = _context.Users.Where(a => a.IsActive == true).OrderBy(i=> i.FullName).ToList();
            foreach (var user in userid)
            {
                var raiting = _context.UserTask.Where(i => i.ActivitiesScopeListID == deptask.taskID && i.UserID == user.Id).Select(r => r.Rating).Sum();
                double allTask = _context.UserTask.Where(a => a.ActivitiesScopeListID == deptask.taskID && user.Id == a.UserID).Count();
                double media = raiting/allTask;
                if( raiting == 0 && allTask == 0)
                {
                    media = 0;
                }

                var users = new
                {
                    UserName = user.FullName,
                    UserID = user.Id,
                    Rating = Convert.ToDouble(media.ToString("0.0")),
                    ContractID = deptask.ContractID
                };
                Users.Add(users);

            }
            return Users;   
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
                    Status = taskforduplicate.Status,
                    ContractID = taskforduplicate.ContractID,
                    DepartamentID = taskforduplicate.DepartamentID,
                    PlannerTeamID = taskforduplicate.PlannerTeamID,
                    NotesFromPlanner = taskforduplicate.NotesFromPlanner,
                    IsRework = taskforduplicate.IsRework,
                    NotesFromExecutor = taskforduplicate.NotesFromExecutor
                });
                _context.SaveChanges();


                createTaskRating(new UserTask
                {
                    ContractID = taskforduplicate.ContractID,
                    ActivitiesScopeListID = taskforduplicate.ActivitiesScopeListID,
                    UserID = _context.Team.Where(i => i.ID ==taskforduplicate.ExecutorTeamID).Select(u => u.UserID).FirstOrDefault(),
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
            List<dynamic> retorno = new List<dynamic>();
            id = _context.Team.Where(i => i.UserID == id).Select(u => u.ID).FirstOrDefault();
            var alltasks = _context.ActivityPlan.Where(i => i.ExecutorTeamID == id).ToList();
            
                foreach (var task in alltasks)
                {
                    var Task = new
                    {
                        id = task.ID,
                        contractID = task.ContractID,
                        projectName = _context.Contracts.Where(i => i.id == task.ContractID).Select(i => i.InternalCode).FirstOrDefault(),
                        description = _context.ActivitiesScopeList.Where(i => i.ID == task.ActivitiesScopeListID).Select(d => d.Description).FirstOrDefault(),
                        activitiesScopeListID = task.ActivitiesScopeListID,
                        scheduledDate = task.ScheduledDate,
                        plannedManHour = task.PlannedManHour,
                        plannerTeamID = task.PlannerTeamID,
                        notesFromPlanner = task.NotesFromPlanner,
                        executorTeamID = task.ExecutorTeamID,
                        status = task.Status,
                        executedManHour = task.ExecutedManHour,
                        notesFromExecutor = task.NotesFromExecutor,
                        userID = task.ExecutorTeamID,
                        executorName = _context.Users.Where( i=> i.Id == _context.Team.Where(i => i.ID == id).Select(i => i.UserID).FirstOrDefault()).Select(n=> n.FullName).FirstOrDefault(),
                        isRework = task.IsRework,
                        
                    };
                    retorno.Add(Task);



                }
                return retorno;
          



        }
        public bool UpdateNotes(int taskID, string notesExecut, string notesPlanned)
        {
            if(notesExecut != null)
            {
                var updateNotes = _context.ActivityPlan.Where( i=> i.ID == taskID).FirstOrDefault();
                updateNotes.NotesFromExecutor = notesExecut;
                _context.Update(updateNotes);
                _context.SaveChanges();
                return true;
            }
            if(notesPlanned != null)
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


    }
}
