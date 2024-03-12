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



        public bool UpdateMilesTones(MilesTonesDTO milesTonesDTO);


        public List<Positions> GetPositionsByGrup();

        public bool CreateAcquisitionsPlanned(AcquisitionsDTO acquisitions);

        public bool DeleteAcquisition(int ID);

        public bool UpdateAcquisition(AcquisitionsDTO acquisitions);

        public bool CreateAcquisitionsMade(AcquisitionMadeDTO acquisitionMade);

        public bool DeleteAcquisitionMade(int ID);

        public bool UpdateAcquisitionMadeItem(AcquisitionMadeDTO acquisitionMade);

        public List<PMAcquisitionPlanned> GetAcquisitions(int ContractID);

        public List<PMAcquisitionMade> GetAcquisitionsMade(int AquisitionPlannedID);
    }
}
