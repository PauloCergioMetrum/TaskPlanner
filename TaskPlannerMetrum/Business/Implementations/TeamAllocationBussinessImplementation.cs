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

        public TeamAllocationResponseDTO GetTeamAllocation(string businessUnit = null, DateTime? startDate = null, DateTime? endDate = null, List<int> functionID = null, string project = null)
        {
            var allocations = _teamAllocationRepository.GetTeamAllocation(businessUnit, startDate, endDate, functionID, project);
            var graphics = _teamAllocationRepository.GetTeamAllocationGraphic(startDate, endDate, functionID);
            var functions = _teamAllocationRepository.GetTeamAllocationGraphicFunctions(startDate, endDate, functionID);
            var cards = _teamAllocationRepository.GetTeamAllocationCards(startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue);
            var filter = _teamAllocationRepository.GetTeamFilterBusinessUnit();

            return new TeamAllocationResponseDTO
            {
                TeamAllocations = allocations,
                TeamAllocationGraphics = graphics,
                TeamAllocationGraphicFunctions = functions,
                GetTeamAllocationCards = cards,
                BusinessUnits = filter,
            };
        }

        public List<TeamAllocationDTO.GetTeamAllocationCards> GetTeamAllocationCards(DateTime DateStart, DateTime DateEnd)
        {
            return _teamAllocationRepository.GetTeamAllocationCards(DateStart, DateEnd);
        }

        public List<TeamAllocationDTO.GetTeamAllocationGraphicFunctions> GetTeamAllocationGraphicFunctions()
        {
            return _teamAllocationRepository.GetTeamAllocationGraphicFunctions();
        }

        public List<string> GetTeamFilterBusinessUnit(string selectedBusinessUnit = null)
        {
            return _teamAllocationRepository.GetTeamFilterBusinessUnit();
        }
    }
}