using System.Collections;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.BusinessUnit
{
    public interface IBusinessUnitRepository
    {
      
        IEnumerable<vBusinessUnit> GetAllBuninesUnit();
    }
}
