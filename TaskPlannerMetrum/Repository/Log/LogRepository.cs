using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.Context;

namespace TaskPlannerMetrum.Repository.Log
{
    public class LogRepository : ILogRepository
    {
        private readonly MSSQLContext _context;

        public LogRepository(MSSQLContext context)
        {
            _context = context;
        }

        public void CreateLog(Model.Log log)
        {
            if (log == null) throw new ArgumentException("Log não pode ser nulo.");
            _context.Log.Add(log);
            _context.SaveChanges();
        }

  
        public void DeleteLogs(List<Model.Log> logs, int userId, string userName)
        {
            if (logs == null || logs.Count == 0)
                throw new ArgumentException("A lista de logs para deleção não pode ser nula ou vazia.");

            foreach (var log in logs)
            {
                var deletionLog = new Model.Log
                {
                    UserId = userId,
                    UserName = userName,
                    Action = "Delete",
                    Description = $"Log deletado: {log.Description}",
                    CreatedAt = DateTime.UtcNow,
                    Status = "Deleted",
                    SeverityLevel = "Info",
                    ErrorDetails = null
                };

                _context.Log.Add(deletionLog);
            }

            _context.SaveChanges();
        }
    }
}
