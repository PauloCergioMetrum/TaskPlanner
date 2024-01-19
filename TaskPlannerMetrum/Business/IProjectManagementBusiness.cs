using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface IProjectManagementBusiness
    {



        public bool UpdateForecast(ProjectManagementDTO forecast);


        public OrderInformationDTO GetOrderInformation(int id);


        public bool CreateMilesTones(MilesTonesDTO milesTones);

        List<string> GetMilestonesNames(int contractID);









    }
}
