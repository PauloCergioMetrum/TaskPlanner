using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Globalization;
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

        public bool DeleteMilestones(string ID);



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
        public List<PmTypeCost> GetTypeOfCost(int ContractID);
        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned);
        public bool DeletePredictedCost(string ID);
        public List<PmCostPlanned> GetPmCostPlanned(int ContractID);

        public bool CreateOrUpdateCostMade(PmCostMade pmCostMade);
        public bool DeleteCostMade(string ID);
        public List<PmCostMade> GetPmCostMade(string Pm_Cost_PlannedID);

        public List<vContractProject> GetAllContractProjectByTechLeader(int? TechLeaderID, string InspectorName);

        public List<vMileStonesValue> GetAllMilestonesItem(int ContractID);
        public bool IsExistMilesStone(MilesTonesDTO milesTonesDTO);

        public int existMilesStonesValue(string ID);
        public bool UpdateMilesValue(MilesTonesDTO milesTonesDTO, int ID);


        // MOBILIZAÇÃO 
        public List<vPM_Mobilization_Combined> GetMobilization(int contractID);
        public List<PM_Mobilization_Made> GetMobilizationMade(string mobilizationPlannedID);

        public bool CreateMobilizationPlanned(PM_Mobilization_Planned mobilizationPlanned);

        public bool CreateMobilizationMade(PM_Mobilization_Made mobilizationMade);
        public bool UpdateMobilization(string ID);
        public bool UpdateMobilizationMaded(string ID);

        public bool DeleteMobilization(string ID);
        public bool DeleteMobilizationMade(string ID);



        // SERVIÇOES TERCEIRIZADO 

        public List<Pm_Type_OutsourcedServicesDTO> GetAllOutsourcedServiceNames();
        public List<vPM_OutsourcedServices_Combined> GetOutsourcedServicesCombined( int ContractID);
        public List<vPM_OutsourcedServices_Made_Combined> GetOutsourcedServicesCombinedMade(string ID_OutsourcedServices_Planned);
        public bool CreateoutsourcedServicePlanned( PM_OutsourcedServices_Planned createoutsourcedServicePlanned);
        public bool CreateoutsourcedServiceMade(PM_OutsourcedServices_Made createoutsourcedServiceMade);



        public bool UpdateoutsourcedServicePlanned(PM_OutsourcedServices_Planned updatedService);

        public bool UpdateoutsourcedServiceMade(PM_OutsourcedServices_Made updatedServiceMade);


        public bool DeleteOutsourcedServicesPlanned(string ID);

        public bool DeleteOutsourcedServicesMade(string ID);

    }
}
