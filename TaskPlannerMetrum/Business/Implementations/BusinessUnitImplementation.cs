using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.ActiviesScope;
using TaskPlannerMetrum.Repository.ActivityPlan;
using TaskPlannerMetrum.Repository.BusinessUnit;
using TaskPlannerMetrum.Repository.Calendar;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class BusinessUnitImplementation : IBusinessUnitBusiness
    {
        private readonly IBusinessUnitRepository _repository;

        public BusinessUnitImplementation(IBusinessUnitRepository repository)
        {
            _repository = repository;   
        }


    

        public dynamic GetBusinessUnit()
        {
          return _repository.GetBusinessUnit();  
        }
    }
}
