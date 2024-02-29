using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.BusinessUnit;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class BusinessUnitImplementation : IBusinessUnitBusiness
    {
        private readonly IBusinessUnitRepository _unitRepository;

        public BusinessUnitImplementation(IBusinessUnitRepository businessUnitRepository)
        {
            _unitRepository = businessUnitRepository;
        }

        public IEnumerable<vBusinessUnit> GetAllBusinessUnits()
        {
            return _unitRepository.GetAllBuninesUnit();
        }
    }
}
