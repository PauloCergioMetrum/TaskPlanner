using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.VisualBasic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Repository.Calendar;
using TaskPlannerMetrum.Repository.Generic;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class CalendarBusiness : ICalendarBusiness
    {

        private readonly ICalendarRepository  _repository;
        public CalendarBusiness(ICalendarRepository  repository)
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


    }
}
