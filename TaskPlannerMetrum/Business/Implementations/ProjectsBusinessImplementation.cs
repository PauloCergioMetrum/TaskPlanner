using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TaskPlannerMetrum.Data.Converter.Implementations;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.NewContract;
using TaskPlannerMetrum.Repository.DepartmentProjects;
using TaskPlannerMetrum.Repository.Generic;
using TaskPlannerMetrum.Repository.Projects;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ProjectsBusinessImplementation : IProjectsBusiness
    {

        private readonly IProjectsRepository _projectRepository;
        private readonly IUserBusiness _userBusiness;
        private readonly IDepartmentProjectsRepository _departmentProjectsRepository;
     


        public ProjectsBusinessImplementation(IProjectsRepository projectsRepository, IUserBusiness userBusiness, IDepartmentProjectsRepository departmentProjectsRepository)
        {
            _projectRepository= projectsRepository;
            _userBusiness= userBusiness;
            _departmentProjectsRepository = departmentProjectsRepository;


        }
        public bool Create(ProjectDTO projects)
        {

            Projects newProject = new Projects
            {
                ClientID = Convert.ToInt32(projects.clientID),
                StartDate = Convert.ToDateTime(projects.startDate),
                EndDate = Convert.ToDateTime(projects.endDate),
                ExpectedManHor = float.Parse(projects.expectedManHor),
                ContractEndDate = Convert.ToDateTime(projects.contractEndDate),
                Name = projects.projectName,
                PMTeamID = Convert.ToInt32(projects.PMTeamID),
                InternalCode = projects.internalCode,
                PlannedManHour = 0,
                WorkspaceID = 1,
                Status = "5",
                TechLeaderID = projects.TechLeaderID,
                
                DepartamentID = _projectRepository.getDep(projects.dapartment[0])
            };

            if (_projectRepository.Create(newProject))
            {

                return _departmentProjectsRepository.Create(projects.projectName, projects.dapartment);
            }
            else
            {
                return false;
            }

        }

        public dynamic FindAll()
        {
            try
            {
                var projectList = _projectRepository.GetAllProjects().Where(s => s.StartDate != null).OrderByDescending(s => s.ContractEndDate).Select(s => new { s.ProjectName, s.ClientName, s.PlannedManHour, s.StartDate, s.ProjectID, s.full_name, s.ExecutedManHour, s.ExpectedManHor, s.ContractEndDate, s.EndDate, s.Status, s.TechLeaderName }).AsQueryable();
                int totalRecors = projectList.Count();
                projectList = projectList.OrderByDescending(x => x.StartDate);
                var result = new
                {
                    projectList = projectList,
                    totalRecors = totalRecors,
                };
                return result;
            }
            catch (Exception e)
            {
                return e;
            }

        }

        public dynamic FindPlannerManager()

        {
            return _projectRepository.FindAllUser();
        }

        public dynamic GetAllContracts()
        {
            try
            {
                var projectList = _projectRepository.GetAllProjectsContracts().Where(s => s.StartDate != null).OrderByDescending(s => s.ContractEndDate).Select(s => new { s.ProjectName, s.ClientName, s.PlannedManHour, s.StartDate, s.ProjectID, s.full_name, s.ExecutedManHour, s.ExpectedManHor, s.ContractEndDate, s.EndDate, s.Status }).AsQueryable();
                int totalRecors = projectList.Count();
                if (projectList != null)
                {
                    projectList = projectList.OrderByDescending(x => x.StartDate);
                }

                var result = new
                {
                    projectList = projectList,
                    totalRecors = totalRecors,
                };
                return result;
            }
            catch (Exception e)
            {
                return e;
            }

        }

        public dynamic GetAllProjectDep(int contractID)
        {
            return _projectRepository.GetAllProjectDep(contractID); 
        }

        public bool UpdateProject(DepartmentProjects newProject)
        {
            return _projectRepository.UpdateProject(newProject);
        }

        public dynamic GetAllProjects()
        {

            try
            {
                var projectList = _projectRepository.GetAllProjects().Where(s => s.StartDate != null).OrderByDescending(s => s.ContractEndDate).Select(s => new { s.ProjectName, s.ClientName, s.PlannedManHour, s.StartDate, s.ProjectID, s.full_name, s.ExecutedManHour, s.ExpectedManHor, s.ContractEndDate, s.EndDate, s.Status }).AsQueryable();
                int totalRecors = projectList.Count();
                if (projectList != null)
                {
                    projectList = projectList.OrderByDescending(x => x.StartDate);
                }

                var result = new
                {
                    projectList = projectList,
                    totalRecors = totalRecors,
                };
                return result;
            }
            catch (Exception e)
            {
                return e;
            }


        }

        public dynamic GetAllUsersDep(string depname)
        {
            return _projectRepository.GetAllUsersDep(depname);
        }

        public dynamic GetDepFinances(int id)
        {
            return  _projectRepository.GetDepFinances(id);
        }

        public bool NewCreat(Model.NewContract.NewProject newproject)
        {
            return _projectRepository.NewCreat(newproject);
        }

        public dynamic ProgressProject()
        {
            return _projectRepository.ProgressProject();
        }

        public bool UpdateStatus(TaskPlannerMetrum.Model.ProjectStauts newProject)
        {


            return _projectRepository.SetStatus(newProject);
        }

        public dynamic UserByDep(string departament)
        {
            return _projectRepository.UserByDep(departament);
        }

        public bool ActiveProject(int id)
        {
            return _projectRepository.ActiveProject(id);    
        }

        public dynamic getActiveProject()
        {
            return _projectRepository.getActiveProject();
        }

        public dynamic getInfoProject(int id)
        {
            return _projectRepository.getInfoProject(id);
        }
    }
}
