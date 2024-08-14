using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.Executive
{
    public interface IExecutiveRepository
    {

 

        List<ExecutiveDto> GetPvExecutiveTable(string inspectorIDs = null, DateTime? StartDate = null, DateTime? EndDate = null);



    }

}
