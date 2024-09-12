using System;
using System.Collections.Generic;
using System.Drawing.Text;
using TaskPlannerMetrum.Business.Implementations;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.Executive;
using TaskPlannerMetrum.Repository.Financs;
using TaskPlannerMetrum.Repository.TeamAllocation;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ExecutiveImplentations : IExecutiveBussines
    {

        private readonly IExecutiveRepository _executiveRepository;

        public ExecutiveImplentations(IExecutiveRepository executiveRepository)
        {
            _executiveRepository = executiveRepository;
        }

        public List<ExecutiveDto> GetPvExecutiveTable(string inspectorIDs = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return _executiveRepository.GetPvExecutiveTable(inspectorIDs, startDate, endDate);  
        }
    }
}





