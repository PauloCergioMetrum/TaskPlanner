using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

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

        public bool DeleteAcquisition(string ID);

        public bool UpdateAcquisition(AcquisitionsDTO acquisitions);

        public bool CreateAcquisitionsMade(AcquisitionMadeDTO acquisitionMade);

        public bool DeleteAcquisitionMade(string ID, string AquisitionPlannedID);

        public bool UpdateAcquisitionMadeItem(AcquisitionMadeDTO acquisitionMade);

        public List<vPMAcquisitionCombined> GetAcquisitions(int ContractID);

        public List<PMAcquisitionMade> GetAcquisitionsMade(string AquisitionPlannedID);

        public bool CreateTypeOfCost(PmTypeCost pmTypeCost);
        public bool DeleteTypeOfCost(int ID);
        public List<PmTypeCost> GetTypeOfCost();

        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned);
        public bool DeletePredictedCost(string ID);
        public List<PmCostPlanned> GetPmCostPlanned();

        public bool CreateOrUpdateCostMade(PmCostMade pmCostMade);
        public bool DeleteCostMade(string ID);
        public List<PmCostMade> GetPmCostMade();


    }
}
