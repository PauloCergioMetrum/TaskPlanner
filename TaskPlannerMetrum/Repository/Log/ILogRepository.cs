using System.Collections.Generic;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Repository.Log
{
    public interface ILogRepository
    {

        //delete de clienet 
        void CreateLog(Model.Log log);
        void DeleteLogs(List<Model.Log> logs, int userId, string userName);
    }
}