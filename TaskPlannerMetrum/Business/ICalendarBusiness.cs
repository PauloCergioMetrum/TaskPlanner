using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface ICalendarBusiness
    {

        public dynamic UsersCalender(string depID);

        public dynamic TaskforUsers(AllUserCalendar userID);

        public dynamic UsersForProjects(int contractID);

        public dynamic GetAllContracts();


        public List<CalendarEquipament> GetEquipamentCalendar(List<int> equipaments);

        public List<Equipment> GetAllEquipament();

    }


}
