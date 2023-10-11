using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Model.NewContract;

namespace TaskPlannerMetrum.Repository.Projects
{
    public interface IProjectsRepository
    {
        public List<vProjectList> GetAllProjects();

        public List<vProjectList> GetAllProjectsContracts();

        public List<User> FindAllUser();


        public  bool SetStatus(TaskPlannerMetrum.Model.ProjectStauts project);

        
        public bool Create(TaskPlannerMetrum.Model.Projects newProject);

        public List<User> UserByDep(string departament);

        public int getDep(string departament);

        public List<Model.ProjectProgress> ProgressProject();

        public dynamic GetDepFinances(int id);

        public List<User> GetAllUsersDep(string depname);


        public bool NewCreat(Model.NewContract.NewProject newproject);

        public dynamic GetAllProjectDep(int contractID);

        public bool UpdateProject(Model.DepartmentProjects newProject);

        public bool ActiveProject(int id);

        public dynamic getActiveProject();

        public dynamic getInfoProject(int id);

        public void FavoriteProject(UserProjects userProjects);

        public void DeletFavoritProject(UserProjects userProjects);


        public void CreateRetroactiveDate(int contractID, DateTime retroactiveDate);
        

        public bool UpdateRetroactiveDate(int contractID, DateTime retroactiveDate);    
    }
}
