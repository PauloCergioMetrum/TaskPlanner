using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Repository.Calendar
{
    public interface ICalendarRepository
    {
        public dynamic UsersCalender(string depID);

        public dynamic TaskforUsers(AllUserCalendar AllUserCalendar);

    }
}
