using System.Collections;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IBusinessUnitBusiness
    {

        IEnumerable<vBusinessUnit> GetAllBusinessUnits();
    }
}
