using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.TeamAllocation
{
    public interface ITeamAllocationRepository
    {
        //List<TeamAllocationDTO> GetTeamAllocation(string businessUnit = null, DateTime? startDate = null, DateTime? endDate = null, List<int> function = null, string project = null);

        List<TeamAllocationDTO.TeamAllocationGraphicDTO> GetTeamAllocationGraphic(DateTime? startDate = null, DateTime? endDate = null, List<int> functionID = null, string[] project = null);


        List<TeamAllocationDTO.GetTeamAllocationGraphicFunctions> GetTeamAllocationGraphicFunctions(DateTime? startDate = null, DateTime? endDate = null, List<int> functionIDs = null);

        List<TeamAllocationDTO.GetTeamAllocationCards> GetTeamAllocationCards(DateTime DateStart, DateTime DateEnd);
        List<string> GetTeamFilterBusinessUnit(string selectedBusinessUnit = null);


        public List<TeamAllocationDTO> GetTeamAllocation(string[] businessUnit = null, DateTime? startDate = null, DateTime? endDate = null, List<int> functionID = null, string[] project = null , string[] techLeadName = null);





    }
}