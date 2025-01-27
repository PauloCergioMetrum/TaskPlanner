using System.Collections.Generic;
using System;
using TaskPlannerMetrum.Repository.Log;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class LogBusinesssImplementation : ILogBusinesssImplementation
    {
        private readonly ILogRepository _logRepository;

        public LogBusinesssImplementation(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void DeleteLogs(List<Model.Log> logs, int userId, string userName)
        {
            try
            {
                _logRepository.DeleteLogs(logs, userId, userName); 
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Erro ao deletar logs: {ex.Message}");
                throw;
            }
        }
    }
}