using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model.Context;

namespace TaskPlannerMetrum.Repository.DepartmentProjects
{
    public class DepartmentProjectsRepository : IDepartmentProjectsRepository
    {
        private MSSQLContext _context;

        public DepartmentProjectsRepository(MSSQLContext context) { _context = context; }
        public bool Create(string projectName, string[] departmentsName)
        {
            
                int projectID = _context.Projects.Where(p => p.Name == projectName).Select(p => p.ID).FirstOrDefault();
                foreach (var department in departmentsName)
                {
                     
                    _context.DepartmentProjects.Add(new Model.DepartmentProjects
                    {
                        DepartmentID = _context.Department.Where(d => d.Name == department).Select(d => d.ID).FirstOrDefault(),
                        ContractID = projectID,
                    });
                    _context.SaveChanges();
                   

                }
                return true;
            
           
            
        }
    }
}
