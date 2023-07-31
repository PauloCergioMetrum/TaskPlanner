using System.Collections.Generic;

namespace TaskPlannerMetrum.Repository.DepartmentProjects
{
    public interface IDepartmentProjectsRepository
    {
        public bool Create(string projectName, string[] departmentsName);
    }
}
