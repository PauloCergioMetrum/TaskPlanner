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

        public List<TeamAllocationDTO.GetTeamAllocationCards> GetTeamAllocationCards(DateTime DateStart, DateTime DateEnd)
        {
            return _teamAllocationRepository.GetTeamAllocationCards(DateStart, DateEnd);
        }

        public List<TeamAllocationDTO.GetTeamAllocationGraphicFunctions> GetTeamAllocationGraphicFunctions()
        {
            return _teamAllocationRepository.GetTeamAllocationGraphicFunctions();
        }

        public TeamAllocationResponseDTO GetTeamAllocation(string businessUnit = null, DateTime? startDate = null, DateTime? endDate = null, string function = null, string project = null)
        {
            var allocations = _teamAllocationRepository.GetTeamAllocation(businessUnit, startDate, endDate, function, project);
            var graphics = _teamAllocationRepository.GetTeamAllocationGraphic(startDate, endDate, function);
            var functions = _teamAllocationRepository.GetTeamAllocationGraphicFunctions();
            var cards = _teamAllocationRepository.GetTeamAllocationCards(startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue);

            return new TeamAllocationResponseDTO
            {
                TeamAllocations = allocations,
                TeamAllocationGraphics = graphics,
                TeamAllocationGraphicFunctions = functions,
                GetTeamAllocationCards = cards,
            };
        }
    }
}