using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.TeamAllocation;
namespace TaskPlannerMetrum.Business
{
    public interface ITeamAllocationBusiness
    {
        TeamAllocationResponseDTO GetTeamAllocation(string businessUnit, DateTime? startDate, DateTime? endDate, string function, string project);

    }
}
