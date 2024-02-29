using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.BusinessUnit
{
    public class BusinessUnitRepository :IBusinessUnitRepository
    {
        private MSSQLContext _context;
        public BusinessUnitRepository (MSSQLContext  context)
        {
            _context = context;
        }

        public IEnumerable<vBusinessUnit> GetAllBuninesUnit()
        {
            return _context.vBusinessUnit.ToList();
        }
    }
}
