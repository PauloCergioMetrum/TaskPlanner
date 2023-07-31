using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Business
{
    public interface ICalendarBusiness
    {

        public dynamic UsersCalender(string depID);

        public dynamic TaskforUsers(AllUserCalendar userID);
    }


}
