using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Repository.Generic;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class DepartmentBusinessImplementation : IDepartmentBusiness
    {
        private readonly IRepository<Department> _repository;

        public DepartmentBusinessImplementation(IRepository<Department> repository)
        {
            _repository = repository;
        }
        public Department Create(Department department)
        {
            throw new System.NotImplementedException();
        }

        public dynamic FindAll()
        {
            var departments = _repository.FindAll().Select(s => new { s.ID, s.Name });

            return departments;
        }
    }
}
