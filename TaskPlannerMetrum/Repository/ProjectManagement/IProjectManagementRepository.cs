using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public interface IProjectManagementRepository
    {

        public Model.Contracts GetForecastByID(int id);

        public vContractList GetForecastByIDView(int id);

        public bool UpdateForecast(Model.Contracts forecast);


        public int CreateMilestonesItem(MilestonesItem milestones);

        public bool CreateMilestonesValue(MilestonesValue milestones);

        List<string> GetMilestonesNames(int contractID);

        public bool  DeleteMilestones(MilesTonesDTO ID);



        public MilestonesValue GetMilestonesValueByID(int milestonesID);

        public bool UpdateMilesTones(MilestonesValue milestonesValue);


        public List<Positions> GetPositionsByGrup();

        public bool CreateAcquisitionsItem(PMAcquisitionPlanned acquisitions);

        public bool ExistAcquisition(string ID);

        public bool DeleteAcquisition(string ID);

        public bool UpdateAcquisition(AcquisitionsDTO acquisitions);

        public bool CreateAcquisitionMadeItem(PMAcquisitionMade acquisitionMade);

        public bool DeleteAcquisitionMade(string ID, string AquisitionPlannedID);

        public bool UpdateAcquisitionMadeItem(AcquisitionMadeDTO acquisitionMade);

        public bool ExistAcquisitionMade(string ID);

        public List<vPMAcquisitionCombined> GetAcquisitions(int ContractID);

        public List<vPMAcquisitionCost> GetAcquisitionsMade(string AquisitionPlannedID);

        public bool CreateTypeOfCost(PmTypeCost pmTypeCost);

        public bool DeleteTypeOfCost(string ID);
        public List<PmTypeCost> GetTypeOfCost(int ContractID );
        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned);
        public bool DeletePredictedCost(string ID);
        public List<PmCostPlanned> GetPmCostPlanned(int ContractID);

        public bool CreateOrUpdateCostMade(PmCostMade pmCostMade);
        public bool DeleteCostMade(string ID);
        public List<PmCostMade> GetPmCostMade(string Pm_Cost_PlannedID);

        public List<vContractProject> GetAllContractProjectByTechLeader(int? TechLeaderID, string InspectorName);
       
    }
}
