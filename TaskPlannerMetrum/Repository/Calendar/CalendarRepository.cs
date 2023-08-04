using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
                    name = user.FullName
                };
                retorno.Add(users);

            }
            return retorno;

        }
        public dynamic TaskforUsers(AllUserCalendar AllUserCalendar)
        {
            List<vCalendar> tasksUsers = _context.vCalendar.ToList();
            if (AllUserCalendar.userID != 0)
            {
                var  taskuser = tasksUsers.Where(u => u.UserID == AllUserCalendar.userID &&  u.UserDepartment == AllUserCalendar.DepName)
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
                            .Select(s => new { s.ScheduledDate, s.ActivityDescription, s.ActivityDepartment, s.PlannedManHour, s.InternalCode })
                            .Distinct(),
                        backgroundColor = setColor(tasksUsers.Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID).Select(s => s.PlannedManHour).Sum())

                    }).Distinct().ToList();
                var listcoipia = taskuser.ToList();
                foreach (var item in listcoipia)
                {
                    if(taskuser.Where(u=> u.userID == item.userID && u.start == item.start).Count() >=2)
                    {
                        taskuser.Remove(item);
                    }
                }
                return taskuser;
                
            }
            else
            {
                  var taskuser = tasksUsers.Where(u => u.UserDepartment == AllUserCalendar.DepName)
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
                            .Select(s => new { s.ScheduledDate, s.ActivityDescription, s.ActivityDepartment, s.PlannedManHour,s.InternalCode })
                            .Distinct(),
                        backgroundColor = setColor(tasksUsers.Where(s => s.ScheduledDate == u.ScheduledDate && s.UserID == u.UserID).Select(s => s.PlannedManHour).Sum())
                    }).Distinct().ToList();

                var Copyofthelist = taskuser.ToList();
                foreach (var task in Copyofthelist)
                {
                    if (taskuser.Where(u => u.userID == task.userID && u.start == task.start).Count() >=2)
                    {
                        taskuser.Remove(task);
                    }
                }
                return taskuser;

            }
            


        }
        public string setColor(double valor)
        {

            switch (valor)
            {

                case  <= 5:
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
    }
}

