using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.TeamAllocation;

namespace TaskPlannerMetrum.Business
{
    public class TeamAllocationBussinessImplementation : ITeamAllocationBusiness
    {
        private readonly ITeamAllocationRepository _teamAllocationRepository;

        public TeamAllocationBussinessImplementation(ITeamAllocationRepository teamAllocationRepository)
        {
            _teamAllocationRepository = teamAllocationRepository;
        }

        public TeamAllocationResponseDTO GetTeamAllocation(string businessUnit, DateTime? startDate, DateTime? endDate, string function, string project)
        {
            var allocations = _teamAllocationRepository.GetTeamAllocation(businessUnit, startDate?.Year, startDate?.Month, endDate?.Year, endDate?.Month, function, project);
            var graphics = _teamAllocationRepository.GetTeamAllocationGraphic(startDate, endDate, function);
            var functions = _teamAllocationRepository.GetTeamAllocationGraphicFunctions();

            return new TeamAllocationResponseDTO
            {
                TeamAllocations = allocations,
                TeamAllocationGraphics = graphics,
                GetTeamAllocationGraphicFunctions = functions,
                
            };
        }
        public List<TeamAllocationDTO.GetTeamAllocationGraphicFunctions> GetTeamAllocationGraphicFunctions()
        {
            return _teamAllocationRepository.GetTeamAllocationGraphicFunctions();
        }


    }
}
