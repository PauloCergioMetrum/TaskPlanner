using System.Collections.Generic;

namespace TaskPlannerMetrum.Business
{
    public interface ILogBusinesssImplementation
    {


        public void DeleteLogs(List<Model.Log> logs, int userId, string userName);


    }
}
