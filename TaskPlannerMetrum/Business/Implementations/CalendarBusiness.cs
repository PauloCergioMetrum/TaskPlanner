using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Asn1.X509.SigI;
using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.Calendar;
using TaskPlannerMetrum.Repository.Generic;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class CalendarBusiness : ICalendarBusiness
    {

        private readonly ICalendarRepository _repository;
        public CalendarBusiness(ICalendarRepository repository)
        {
            _repository = repository;
        }
        public dynamic UsersCalender(string depID)
        {
            return _repository.UsersCalender(depID);
        }

        public dynamic TaskforUsers(AllUserCalendar userID)
        {
            return _repository.TaskforUsers(userID);



        }

        public dynamic UsersForProjects(int contractID)
        {
            //return _repository.UsersForProjects(contractID);


            List<vCalendar> UsersProjects = _repository.UsersForProjects(contractID);

            // Dictionary para rastrear elementos únicos
            var taskDict = new Dictionary<(int userID, string start), dynamic>();
            var distinctTasks = new List<dynamic>();

            foreach (var u in UsersProjects)
            {
                var key = (u.UserID, u.ScheduledDate.ToString("yyyy-MM-dd"));

                if (!taskDict.ContainsKey(key))
                {
                    var taskItems = UsersProjects
                        .Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID)
                        .Select(s => new { s.ScheduledDate, s.ActivityDescription, s.ActivityDepartment, s.PlannedManHour, s.InternalCode, status = SetStatus(s.Status) })
                        .Distinct()
                        .ToList(); // Use ToList to materialize the collection

                    var backgroundColor = setColor(taskItems.Sum(s => s.PlannedManHour));

                    var taskUser = new
                    {
                        userID = u.UserID,
                        titleUser = u.UserName,
                        title = u.UserName,
                        contractName = u.InternalCode,
                        Taskstart = u.ScheduledDate.ToString("yyyy-MM-dd") + "T08:00:00",
                        start = u.ScheduledDate.ToString("yyyy-MM-dd"),
                        end = u.ScheduledDate.ToString("yyyy-MM-dd"),
                        depNameUSER = u.UserDepartment,
                        task = taskItems,
                        backgroundColor = backgroundColor,
                        equipamentName = u.EquipamentName ?? "Nenhum equipamento alocado"
                    };

                    taskDict[key] = taskUser;
                    distinctTasks.Add(taskUser);
                }
            }

            return distinctTasks;
        }

        public dynamic GetAllContracts()
        {
            return _repository.GetAllContracts();
        }


        public string setColor(double valor)
        {
            if (valor <= 5)
            {
                return "#629763";
            }
            else if (valor <= 7)
            {
                return "#efbf4d";
            }
            else
            {
                return "#a32638";
            }
        }

        public string SetStatus(string status)
        {

            switch (status)
            {
                case "5":
                    status = "Não Iniciada";
                    break;
                case "3":
                    status = "Bloqueada";
                    break;
                case "4":
                    status = "Cancelada";
                    break;
                case "6":
                    status = "Finalizada";
                    break;
                case "2":
                    status = "Em Progresso";
                    break;
                case "1":
                    status = "Concluído";
                    break;
            }

            return status;

        }

        public List<CalendarEquipament> GetEquipamentCalendar(List<int> equipaments)
        {
            List<vCalendar> ListEquipaments = _repository.GetEquipaments(equipaments);

            // Dictionary para rastrear elementos únicos
            var EquipmanetUser = new Dictionary<(int userID, string start), dynamic>();
            var uniqEquipaments = new List<CalendarEquipament>();

            foreach (var u in ListEquipaments)
            {
                var key = (u.UserID, u.ScheduledDate.ToString("yyyy-MM-dd"));

                if (!EquipmanetUser.ContainsKey(key))
                {
                    var taskItems = ListEquipaments
                        .Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID && s.Status != "4")
                        .Select(s => new { s.ScheduledDate, s.ActivityDescription, s.ActivityDepartment, s.PlannedManHour, s.InternalCode, status = SetStatus(s.Status) })
                        .Distinct()
                        .ToList();

                    // Verifica se existem itens válidos antes de criar o objeto `taskUser`
                    if (taskItems.Any())
                    {
                        var backgroundColor = setColor(taskItems.Sum(s => s.PlannedManHour));

                        var taskUser = new CalendarEquipament
                        {
                            userID = u.UserID,
                            titleUser = u.UserName,
                            title = u.UserName,
                            contractName = u.InternalCode,
                            Taskstart = u.ScheduledDate.ToString("yyyy-MM-dd") + "T08:00:00",
                            start = u.ScheduledDate.ToString("yyyy-MM-dd"),
                            end = u.ScheduledDate.ToString("yyyy-MM-dd"),
                            depNameUSER = u.UserDepartment,
                            task = taskItems,
                            backgroundColor = backgroundColor,
                            equipamentName = u.EquipamentName ?? "Nenhum equipamento alocado"
                        };

                        EquipmanetUser[key] = taskUser;
                        uniqEquipaments.Add(taskUser);
                    }
                }
            }

            return uniqEquipaments;
        }


        public List<Equipment> GetAllEquipament()
        {
            return _repository.GetAllEquipament();
        }
    }
}
