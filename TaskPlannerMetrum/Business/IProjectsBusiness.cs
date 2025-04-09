using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Model.NewContract;

namespace TaskPlannerMetrum.Business
{
    public interface IProjectsBusiness
    {
        bool Create(ProjectDTO projects);

        public bool NewCreat(Model.NewContract.NewProject newproject);

        //UserVO FindByID(long id);
        dynamic FindAll();
        dynamic FindPlannerManager();
        // dynamic FindAllProjects();

        public dynamic GetAllProjects();

        public dynamic GetAllContracts();

        bool UpdateStatus(TaskPlannerMetrum.Model.ProjectStauts newProject);

        public dynamic UserByDep(string departament);



        public dynamic ProgressProject();

        dynamic GetDepFinances(int id);

        dynamic GetAllUsersDep(string depname);


        public dynamic GetAllProjectDep(int contractID);

        public bool UpdateProject(CreateProjectRetroactiveDate newProject);

        public bool ActiveProject(int id);

        public dynamic getActiveProject();

        public dynamic getInfoProject(int id);
     

        public void FavoriteProject(int ContractID, int UserID);

        public void DeletFavoritProject(int ContractID, int UserID);


        public bool UpdateRetroactiveDate(int contractID, DateTime retroactiveDate);

        public List<vContractProject> GetAllContractProjectByTechLeader(int? TechLeaderID, string InspectorName);

        public List<vPM_SummaryPlannedData> Tapscope(int contractID);


        public ProjectStatusInfo GetProjectStatusById(int id, string internalCode);






    }
}
