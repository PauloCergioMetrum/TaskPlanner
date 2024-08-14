using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface IExecutiveBussines
    {
        List<ExecutiveDto> GetPvExecutiveTable(string inspectorIDs = null);


    }
}
