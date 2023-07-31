using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ActiviesScope
{
    public class ActiviesScopeRepository : IActiviesRepository
    {
        private MSSQLContext _context;
        public ActiviesScopeRepository(MSSQLContext context) { _context = context; }
        public List<ActiviesScopeList> GetActiviesScopeByProject(string ProectID)
        {
            List<ActiviesScopeList> activiesScopeLists = new List<ActiviesScopeList>();

                var departmentList = _context.DepartmentProjects.Where(p => p.ContractID == Convert.ToInt32(ProectID)).Select(s => s.DepartmentID).ToList();
                foreach (var department in departmentList)
                {
                    var actives = _context.ActivitiesScopeList.Where(a => a.GroupTaskID == department).ToList();
                    activiesScopeLists.AddRange(actives);
                }

                return activiesScopeLists;

            
            
        }

        public List<vProjectList> GetTaskById(string ProectID)
        {
            List<vProjectList> Cliente = new List<vProjectList>();
           
                var cliente = _context.vProjectList.Where(p => p.ProjectID == Convert.ToInt32(ProectID)).ToList();
                foreach (var task in cliente)
                {
                    Cliente.Add(new vProjectList() { ClientName = task.ClientName, ProjectName = task.ProjectName });
                }
                return Cliente;
            
         

        }
        public dynamic GetAllTasks(int GrupID)
        {
            return _context.ActivitiesScopeList.Where(i => i.GroupTaskID == GrupID).ToList();

        }
        public dynamic GetTaskByGrup()
        {
            return _context.GroupTasks.ToList();
        }



    }
}
