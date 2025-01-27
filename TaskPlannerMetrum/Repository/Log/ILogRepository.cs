using System.Collections.Generic;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Repository.Log
{
    public interface ILogRepository
    {
        void DeleteLogs(List<Model.Log> logs, int userId, string userName);
    }
}