using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Threading.Tasks;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.ModelViews;
using static System.Net.Mime.MediaTypeNames;

namespace TaskPlannerMetrum.Repository.Calendar
{
    public class CalendarRepository : ICalendarRepository
    {
        private MSSQLContext _context;
        public CalendarRepository(MSSQLContext context)
        {
            _context = context;
        }
        public dynamic UsersCalender(string depID)
        {

            List<dynamic> retorno = new List<dynamic>();
            var getDepId = _context.Department.Where(i => i.Name == depID).Select(i => i.ID).FirstOrDefault();
            var allusers = _context.Users.Where(d => d.DepartmentId == getDepId).ToList();
            foreach (var user in allusers)
            {
                var users = new
                {
                    id = user.Id,
                    name = user.FullName,
                    isActive = user.IsActive

                };
                retorno.Add(users);

            }
            return retorno;

        }
        public dynamic TaskforUsers(AllUserCalendar AllUserCalendar)
        {
            List<vCalendar> tasksUsers = _context.vCalendar.ToList();
            //var userRemove= _context.Users.Where(u => u.IsActive== true).ToList();
            if (AllUserCalendar.userID != 0)
            {
                var taskuser = tasksUsers.Where(u => u.UserID == AllUserCalendar.userID && u.UserDepartment == AllUserCalendar.DepName && u.Status != "4" && u.Status != "3")
                     .Select(u => new
                    {
                        userID = u.UserID,
                        titleUser = u.UserName,
                        title = u.UserName,
                        contractName = u.InternalCode,
                        Taskstart = u.ScheduledDate.ToString("yyyy-MM-dd") + "T08:00:00",
                        start = u.ScheduledDate.ToString("yyyy-MM-dd"),
                        end = u.ScheduledDate.ToString("yyyy-MM-dd"),
                        depNameUSER = u.UserDepartment,
                        task = tasksUsers
                            .Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID)
                            .Select(s => new { s.ScheduledDate, s.ActivityDescription, s.ActivityDepartment, s.PlannedManHour, s.InternalCode, status = SetStatus(s.Status) })
                            .Distinct(),
                        backgroundColor = setColor(tasksUsers.Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID).Select(s => s.PlannedManHour).Sum())

                    }).Distinct().ToList();
                var taskUserList = taskuser.ToList();
                foreach (var task in taskUserList)
                {
                    if (taskuser.Where(u => u.userID == task.userID && u.start == task.start).Count() >= 2)
                    {
                        taskuser.Remove(task);
                    }
                }
                return taskuser;

            }
            else
            {
                var taskuser = tasksUsers.Where(u => u.UserDepartment == AllUserCalendar.DepName && u.Status != "4" && u.Status != "3")
                 .Select(u => new
                  {
                      userID = u.UserID,
                      titleUser = u.UserName,
                      title = u.UserName,
                      contractName = u.InternalCode,
                      Taskstart = u.ScheduledDate.ToString("yyyy-MM-dd") + "T08:00:00",
                      start = u.ScheduledDate.ToString("yyyy-MM-dd"),
                      end = u.ScheduledDate.ToString("yyyy-MM-dd"),
                      depNameUSER = u.UserDepartment,
                      task = tasksUsers
                          .Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID)
                          .Select(s => new { s.ScheduledDate, s.ActivityDescription, s.ActivityDepartment, s.PlannedManHour, s.InternalCode, status = SetStatus(s.Status) })
                          .Distinct(),
                      backgroundColor = setColor(tasksUsers.Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID).Select(s => s.PlannedManHour).Sum())
                  }).Distinct().ToList();

                var taskUserList = taskuser.ToList();
                foreach (var task in taskUserList)
                {
                    if (taskuser.Where(u => u.userID == task.userID && u.start == task.start).Count() >= 2)
                    {
                        taskuser.Remove(task);
                    }
                }
                return taskuser;

            }

        }

        public dynamic UsersForProjects(int contractID)
        {
            List<vCalendar> contractsUsers = _context.vCalendar.Where(i => i.contractID == contractID && i.Status != "4" && i.Status != "3").ToList();
            try
            {
                //Lista de tasks de Usuarios 
                var tasksUsers = contractsUsers.Select(u => new
                {
                    userID = u.UserID,
                    titleUser = u.UserName,
                    title = u.UserName,
                    contractName = u.InternalCode,
                    Taskstart = u.ScheduledDate.ToString("yyyy-MM-dd") + "T08:00:00",
                    start = u.ScheduledDate.ToString("yyyy-MM-dd"),
                    end = u.ScheduledDate.ToString("yyyy-MM-dd"),
                    depNameUSER = u.UserDepartment,
                    task = contractsUsers
                           .Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID)
                           .Select(s => new { s.ScheduledDate, s.ActivityDescription, s.ActivityDepartment, s.PlannedManHour, s.InternalCode, status = SetStatus(s.Status) })
                           .Distinct(),
                    backgroundColor = setColor(contractsUsers.Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID).Select(s => s.PlannedManHour).Sum())
                }).Distinct().ToList();

                //Verificando se existe User Duplicado
                //Nao foi possivel setar List<dynamic> ou <vCalendar> 
                //Lista copia para evitar erro de AsEnumerable 
                var userDuplicate = tasksUsers.ToList();
                foreach (var user in userDuplicate)
                {
                    //Remove User Duplicado 
                    if (tasksUsers.Where(u => u.userID == user.userID && u.start == user.start).Count() >= 2)
                    {
                        tasksUsers.Remove(user);
                    }
                }
                return tasksUsers;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public string setColor(double valor)
        {

            switch (valor)
            {

                case <= 5:
                    return "#629763";
                    break;
                case <= 7:
                    return "#efbf4d";
                    break;
                case >= 8:
                    return "#a32638";
                    break;
                default:
                    return "#a32638";
            }


        }



        //public dynamic GetAllContracts()
        //{
        //    var matchedClientNames = _context.Contracts
        //        .Join(_context.vContractList,
        //            contract => contract.InternalCode,
        //            vContract => vContract.InternalCode,
        //            (contract, vContract) => new
        //            {
        //                ContractID = contract.id,
        //                internalCode = contract.InternalCode,
        //                ClientName = vContract.ClientName
        //            })
        //        .Where(joinResult => joinResult.internalCode != null && joinResult.ClientName != null)
        //        .Select(joinResult => joinResult.ClientName)
        //        .ToList();

        //    return matchedClientNames;
        //}





        //public dynamic GetAllContracts()
        //{

        //    var teste = _context.vContractList.Where(i => i.InternalCode == i.InternalCode).ToList();
        //    {

        //    }

        //    var contracts = _context.Contracts
        //        .Where(e => true)
        //        .Select(c => new
        //        {
        //            contractID = c.id,
        //            internalCode = c.InternalCode,
        //            StatusID = c.StatusID,
        //            DateRetroactive = c.DateRetroactive,
        //        })
        //        .ToList();

        //    return contracts;
        //}



        public dynamic GetAllContracts()
        {
            var matchedClientNames = _context.Contracts
                .Join(_context.vContractList,
                    contract => contract.id,
                    vContract => vContract.ContractID,
                    (contract, vContract) => new
                    {
                        contractID = contract.id,
                        internalCode = contract.InternalCode,
                        ClientName = vContract.ClientName,
                        statusID = contract.StatusID,
                        DateRetroactive = contract.DateRetroactive
                    })
                .Where(joinResult => joinResult.internalCode != null && joinResult.ClientName != null)
                .ToList();

            return matchedClientNames;
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
    }
}
