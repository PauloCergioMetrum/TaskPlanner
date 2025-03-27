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

        public TeamAllocationResponseDTO GetTeamAllocation(string[] businessUnit, DateTime? startDate, DateTime? endDate, List<int> functionID, string[] project, string[] techLeadName = null)
        {
            var allocations = _teamAllocationRepository.GetTeamAllocation(businessUnit, startDate, endDate, functionID, project , techLeadName);
            var graphics = _teamAllocationRepository.GetTeamAllocationGraphic(startDate, endDate, functionID, project);
            var functions = _teamAllocationRepository.GetTeamAllocationGraphicFunctions(startDate, endDate, functionID);
            var cards = _teamAllocationRepository.GetTeamAllocationCards(startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue);
            var filter = _teamAllocationRepository.GetTeamFilterBusinessUnit().ToArray();

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

        public string[] GetTeamFilterBusinessUnit(string selectedBusinessUnit = null)
        {
            return _teamAllocationRepository.GetTeamFilterBusinessUnit().ToArray();
        }

     
    }
}