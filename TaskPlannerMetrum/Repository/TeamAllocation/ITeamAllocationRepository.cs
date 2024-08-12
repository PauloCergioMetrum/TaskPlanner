using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.TeamAllocation
{
    public interface ITeamAllocationRepository
    {
        List<TeamAllocationDTO> GetTeamAllocation(string businessUnit = null, int? startYear = null, int? startMonth = null, int? endYear = null, int? endMonth = null, string function = null, string project = null);
        List<TeamAllocationDTO.TeamAllocationGraphicDTO> GetTeamAllocationGraphic(DateTime? startDate = null, DateTime? endDate = null, string functionName = null);
        List<TeamAllocationDTO.GetTeamAllocationGraphicFunctions> GetTeamAllocationGraphicFunctions();
    }
}
