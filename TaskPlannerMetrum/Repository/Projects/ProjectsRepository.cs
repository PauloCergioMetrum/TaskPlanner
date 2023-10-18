using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.ModelViews;

using System.Data;
using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Policy;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Enums;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using TaskPlannerMetrum.Model.NewContract;
using System.Diagnostics.Contracts;
using System.Collections.Immutable;
using System.Reflection.PortableExecutable;
using Castle.Components.DictionaryAdapter;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace TaskPlannerMetrum.Repository.Projects
{



    public class ProjectsRepository : IProjectsRepository
    {
        private MSSQLContext _context;


        public ProjectsRepository(MSSQLContext context) { _context = context; }

        public bool NewCreat(Model.NewContract.NewProject newproject)
        {
            try
            {
                foreach (var item in newproject.DepartmentContract)
                {

                    _context.DepartmentProjects.Add(new Model.DepartmentProjects
                    {

                        ContractID = newproject.ContractID,
                        DepartmentID = item.DepartmentID,
                        ExpectedHour = item.ExpectedHour,
                        TechLeaderID = item.TechLeaderID,
                    });

                    var contract = _context.Contracts.Where(c => c.id == newproject.ContractID).FirstOrDefault();
                    contract.DateRetroactive = newproject.DateRetroactive;
                    _context.Contracts.Update(contract);

                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }



        }

        public bool Create(Model.Projects newProject)
        {
            try
            {
                _context.Add(newProject);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<vProjectList> GetAllProjectsContracts()
        {

            var listProjects = _context.vProjectList.ToList();
            foreach (var project in listProjects)
            {
                var executedHour = _context.ActivityPlan.Where(p => p.ContractID == project.ProjectID).Select(h => h.ExecutedManHour).ToList();
                project.ExecutedManHour = executedHour == null ? 0 : executedHour.Sum();
                var plannedHour = _context.ActivityPlan.Where(p => p.ContractID == project.ProjectID).Select(h => h.PlannedManHour).ToList();
                project.PlannedManHour = plannedHour == null ? 0 : plannedHour.Sum();
                if (project.Status != "1" && project.Status != "6")
                {
                    project.Status = GetStatus(project.ProjectID);
                }



                //SetStatus(project.ProjectID, project.Status); 

            }

            return listProjects;
        }

        public List<vProjectList> GetAllProjects()

        {
            var listProjects = _context.vProjectList.Where(p => p.Status != "6").ToList();
            foreach (var project in listProjects)
            {

                var executedHour = _context.ActivityPlan.Where(p => p.ContractID == project.ProjectID).Select(h => h.ExecutedManHour).ToList();
                project.ExecutedManHour = executedHour == null ? 0 : executedHour.Sum();
                var plannedHour = _context.ActivityPlan.Where(p => p.ContractID == project.ProjectID).Select(h => h.PlannedManHour).ToList();
                project.PlannedManHour = plannedHour == null ? 0 : plannedHour.Sum();
                if (project.Status != "1" && project.Status != "6")
                {
                    project.Status = GetStatus(project.ProjectID);

                }
                //SetStatus(project.ProjectID, project.Status); 

            }

            return listProjects;
        }

        private string GetStatus(int projectId)
        {


            var tasks = _context.ActivityPlan.Where(t => t.ContractID == projectId).Select(s => s.Status).ToList();
            if (tasks.Count() <= 0)
            {
                return "5";
            }
            if (tasks.Contains("2"))
            {
                return "2";
            }

            if (tasks.Contains("1"))
            {
                if (tasks.Contains("2"))
                {
                    return "2";
                }
                if (tasks.Contains("5"))
                {
                    return "2";
                }

                return "1";

            }
            if (tasks.Contains("5"))
            {
                if (tasks.Contains("1") || tasks.Contains("4"))
                {
                    return "2";
                }
                return "5";

            }

            return "2";


        }

        public bool SetStatus(TaskPlannerMetrum.Model.ProjectStauts newProject)
        {
            try
            {
                var project = _context.Projects.Where(e => e.ID == newProject.projectID).ToList().FirstOrDefault();
                if (project != null)
                {
                    project.Status = newProject.status;
                    project.StartDate = newProject.startDate;
                    project.EndDate = newProject.endDate;
                    project.ContractEndDate = newProject.contractDate;

                    _context.Update(project);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<User> FindAllUser()
        {
            List<User> users = new List<User>();
            var ListUsers = _context.vUserList.Where(u => u.DepartmentID == 4).Select(u => new { u.PMTeamID, u.userName }).ToList();
            foreach (var user in ListUsers)
            {
                users.Add(new User
                {
                    FullName = user.userName,
                    Id = user.PMTeamID
                });
            }

            return users;
        }
        public List<User> UserByDep(string departament)
        {
            List<User> retorno = new List<User>();
            var dep = _context.Department.Where(d => d.Name == departament).Select(d => d.ID).FirstOrDefault();
            var result = _context.Users.Where(d => d.DepartmentId == dep).ToList();
            foreach (var user in result)
            {
                retorno.Add(new User
                {
                    UserName = user.UserName,
                    Id = user.Id

                });
            }
            return retorno;

        }

        public int getDep(string departament)
        {
            return _context.Department.Where(d => d.Name == departament).Select(d => d.ID).FirstOrDefault();


        }

        public List<Model.ProjectProgress> ProgressProject()
        {
            var allProject = _context.Projects.ToList();
            List<Model.ProjectProgress> result = new List<Model.ProjectProgress>();
            foreach (var item in allProject)
            {
                var allTask = _context.ActivityPlan.Where(p => p.ContractID == item.ID).ToList();
                var allttaskCount = allTask.Count();
                var task = allTask.Where(s => s.Status == "1" && s.Status == "6").Count();


                if (allttaskCount != 0)
                {
                    result.Add(new Model.ProjectProgress
                    {
                        Project = item.ID,
                        Progress = ((double)task / allttaskCount) * 100
                    });

                }
                else
                {
                    result.Add(new Model.ProjectProgress
                    {
                        Project = item.ID,
                        Progress = 0
                    });
                }
            }
            return result;
        }


        public dynamic GetDepFinances(int id)
        {

            List<dynamic> retorno = new List<dynamic>();

            var buscadep = _context.vFinanceContract.Where(i => i.ContractID == id).ToList();


            foreach (var item in buscadep)
            {
                bool existeValor = retorno.Any(objeto => objeto.DepartmentID == item.DepartmentID);
                if (!existeValor)
                {
                    var user = _context.Users.Where(u => u.DepartmentId == item.DepartmentID).ToList();
                    var result = new
                    {
                        DepartmentID = item.DepartmentID,
                        DepartmentName = item.DepartmentName,
                        users = user.Select(u => new { u.Id, u.FullName }).ToList()

                    };
                    retorno.Add(result);

                }
            }
            return retorno;

        }
        public List<User> GetAllUsersDep(string depname)
        {
            List<User> newretorno = new List<User>();
            var findIDDEP = _context.Department.Where(n => n.Name == depname).Select(i => i.ID).FirstOrDefault();
            var retorno = _context.Users.Where(i => i.DepartmentId == findIDDEP).ToList();
            foreach (var item in retorno)
            {
                newretorno.Add(new User
                {
                    FullName = item.FullName,
                    Id = item.Id,
                    IsActive = item.IsActive,
                });

            }
            return newretorno;


        }

        public dynamic GetAllProjectDep(int contractID)
        {
            var info = _context.DepartmentProjects.Where(c => c.ContractID == contractID).ToList();
            List<dynamic> retorno = new List<dynamic>();
            foreach (var item in info)
            {
                var users = _context.Users.Where(i => i.DepartmentId == item.DepartmentID).Select(u => new { u.Id, u.FullName }).ToList();
                var departamentName = _context.Department.Where(i => i.ID == item.DepartmentID).Select(n => n.Name).FirstOrDefault().ToString();
                var TechLeaderName = _context.Users.Where(i => i.Id == item.TechLeaderID).Select(n => n.FullName).FirstOrDefault();
                var result = new
                {
                    DepartmentID = item.DepartmentID,
                    DepartmentName = departamentName,
                    users = users,
                    expectedHour = item.ExpectedHour,
                    TechLeaderID = item.TechLeaderID,
                    TechLeaderName = TechLeaderName,

                };
                retorno.Add(result);
                var count = retorno.Where(i => i.DepartmentID == item.DepartmentID).Count();
                if (count > 1)
                {
                    retorno.Remove(result);
                }
            }
            return retorno;
        }

        public bool UpdateProject(Model.DepartmentProjects newProject)
        {



            var updateproject = _context.DepartmentProjects.Where(i => i.ContractID == newProject.ContractID && i.DepartmentID == newProject.DepartmentID).FirstOrDefault();

            var removeRatings = _context.RatingProject.Where(r => r.ProjectID == updateproject.ContractID && r.UserID == updateproject.TechLeaderID).FirstOrDefault();


            if (removeRatings != null)
            {
                var rating = _context.Rating.Where(i => i.RatingProjectID == removeRatings.ID).ToList();
                foreach (var leader in rating)
                {
                    _context.Rating.Remove(leader);
                    _context.SaveChanges();
                }
            }



            var updateprojectTechLeader = _context.RatingProject.Where(t => t.ProjectID == newProject.ContractID && t.UserID == updateproject.TechLeaderID).FirstOrDefault();
            if (updateprojectTechLeader != null)
            {
                _context.RatingProject.Remove(updateprojectTechLeader);
                _context.SaveChanges();
            }

            _context.RatingProject.Add(new RatingProject
            {
                ProjectID = newProject.ContractID,
                UserID = newProject.TechLeaderID,

            });
            _context.SaveChanges();

            UpdateRatingLeader(newProject.TechLeaderID, newProject.ContractID);

            updateproject.TechLeaderID = newProject.TechLeaderID;
            updateproject.ExpectedHour = newProject.ExpectedHour;
            updateproject.ID = updateproject.ID;
            updateproject.FinancesID = updateproject.FinancesID;
            _context.Update(updateproject);
            _context.SaveChanges();


            return true;

        }


        public void UpdateRatingLeader(int userID, int contractID)
        {
            var ratingProjectID = _context.RatingProject.Where(r => r.ProjectID == contractID && r.UserID == userID).Select(r => r.ID).FirstOrDefault();
            var descriptionLeader = _context.RatingDescription.Where(t => t.Type == "L").ToList();

            foreach (var description in descriptionLeader)
            {
                _context.Rating.Add(new Model.Rating
                {
                    RatingDescriptionID = description.ID,
                    RatingProjectID = ratingProjectID,
                    Value = 0

                });

                _context.SaveChanges();
            }


        }


        public bool ActiveProject(int id)
        {


            var project = _context.Contracts.Where(i => i.id == id).FirstOrDefault();
            project.EnableProject = true;
            _context.Update(project);
            _context.SaveChanges();
            return true;


        }

        public dynamic getActiveProject()
        {


            var project = _context.vContractProject.Where(i => i.EnableProject == true).ToList();

            return true;

        }


        public dynamic getInfoProject(int id)
        {

            var taskDep = _context.vPlannedHours.Where(i => i.ContractID == id).ToList();
            return taskDep.Select(p => new
            {
                ProjectName = p.ProjectName,
                ClientName = p.ClientName,

                percentage = SetPercentege(p.ContractID),
                executedHourFull = taskDep.Where(p => p.ContractID == id).Select(e => e.ExecutedHour).Sum(),
                plannedHourFull = taskDep.Where(p => p.ContractID == id).Select(e => e.PlannedHour).Sum(),
                expectedHoursFull = taskDep.Where(p => p.ContractID == id).Select(e => e.ExpectedHour).Sum(),

                ExpetedHours = taskDep.Where(c => c.ContractID == p.ContractID).Select(t => new
                {
                    departamentName = t.DepartmentName,
                    tecLeader = t.TechLeader,
                    expectedHours = t.ExpectedHour,
                    plannedHour = t.PlannedHour,
                    executedHour = t.ExecutedHour


                }).ToList(),

            }).FirstOrDefault();



        }


        public string SetPercentege(int id)
        {
            var allstatus = _context.ActivityPlan.Where(i => i.ContractID == id && i.Status != "3" && i.Status != "4").ToList();
            double finalyStatus = allstatus.Where(s => s.Status == "6" || s.Status == "1").Count();
            double totalProgress = 0;
            double allstatusCount = allstatus.Count();

            if (finalyStatus != 0)
            {
                totalProgress = (finalyStatus / allstatusCount) * 100;
            }

            return totalProgress.ToString();
        }

        public void FavoriteProject(UserProjects userProjects)
        {

            _context.UserProjects.Add(userProjects);
            _context.SaveChanges();




        }

        public void DeletFavoritProject(UserProjects userProjects)
        {

            var userfavorite = _context.UserProjects.Where(u => u.UserID == userProjects.UserID && u.ContractID == userProjects.ContractID).FirstOrDefault();
            _context.Remove(userfavorite);
            _context.SaveChanges();

        }


        public void CreateRetroactiveDate(int contractID, DateTime retroactiveDate)
        {
            var createRetroactiveDate = _context.Contracts.Where(c => c.id == contractID).FirstOrDefault();
            createRetroactiveDate.DateRetroactive = retroactiveDate;
            _context.Update(createRetroactiveDate);
            _context.SaveChanges();
        }

        public bool UpdateRetroactiveDate(int contractID, DateTime retroactiveDate)
        {
            try
            {
                var contract = _context.Contracts.Where(i => i.id == contractID).FirstOrDefault();
                contract.DateRetroactive = retroactiveDate;
                _context.Update(contract);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}

