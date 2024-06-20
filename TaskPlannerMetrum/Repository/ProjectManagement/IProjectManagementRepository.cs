using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public interface IProjectManagementRepository
    {




        // CREATE Methods
        public int CreateMilestonesItem(MilestonesItem milestones);
        public bool CreateMilestonesValue(MilestonesValue milestones);
        public bool CreateAcquisitionsItem(PMAcquisitionPlanned acquisitions);
        public bool CreateAcquisitionMadeItem(PMAcquisitionMade acquisitionMade);
        public bool CreateTypeOfCost(PmTypeCost pmTypeCost);
        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned);
        public bool CreateOrUpdateCostMade(PmCostMade pmCostMade);
        public bool CreateMobilizationPlanned(PM_Mobilization_Planned mobilizationPlanned);
        public bool CreateMobilizationMade(PM_Mobilization_Made mobilizationMade);
        public bool CreateoutsourcedServicePlanned(PM_OutsourcedServices_Planned createoutsourcedServicePlanned);
        public bool CreateoutsourcedServiceMade(PM_OutsourcedServices_Made createoutsourcedServiceMade);
        public bool CreateHH(PM_Man_Hours createHH);
        public bool CreatScopeTraking(PM_Scope_Traking pMScopeTraking);
        public bool CreateScopeChange(PM_Scope_Change pM_Scope_Change);
        public bool CreateFunctions_MilestoneType(PM_Functions_MilestoneType dto);
        public bool CreatePM_MilestonesType(PM_MilestonesType dto);

        public List<PM_MilestonesType> GetAllPM_MilestonesType(string ID);
        //public bool PM_DisplacementService_MilestonesType(PM_DisplacementService_MilestonesType pM_DisplacementService_MilestonesType);


        // UPDATE Methods
        public bool UpdateForecast(Model.Contracts forecast);
        public bool UpdateMilesTones(MilestonesValue milestonesValue);
        public bool UpdateAcquisition(AcquisitionsDTO acquisitions);
        public bool UpdateAcquisitionMadeItem(AcquisitionMadeDTO acquisitionMade);
        public bool UpdateMobilization(string ID);
        public bool UpdateMobilizationMaded(string ID);
        public bool UpdateoutsourcedServicePlanned(PM_OutsourcedServices_Planned updatedService);
        public bool UpdateoutsourcedServiceMade(PM_OutsourcedServices_Made updatedServiceMade);
        public bool UpdateHH(PM_Man_Hours updateHH);
        public bool UpdatepMScopeTraking(PM_Scope_Traking pMScopeTraking);
        public bool UpdateScopeChange(PM_Scope_Change pM_Scope_Change);
        public bool UpdateFunctionID(string ID, PM_Functions_MilestoneType dto);
        public bool UpdateMilesValue(MilesTonesDTO milesTonesDTO, int ID);

        // LIST Methods
        public Model.Contracts GetForecastByID(int id);
        public vContractList GetForecastByIDView(int id);
        List<string> GetMilestonesNames(int contractID);
        public MilestonesValue GetMilestonesValueByID(int milestonesID);
        public List<Positions> GetPositionsByGrup();
        public bool ExistAcquisition(string ID);
        public bool ExistAcquisitionMade(string ID);
        public List<vPMAcquisitionCombined> GetAcquisitions(int ContractID);
        public List<vPMAcquisitionCost> GetAcquisitionsMade(string AquisitionPlannedID);
        public List<PmTypeCost> GetTypeOfCost(int ContractID);
        public List<PmCostPlanned> GetPmCostPlanned(int ContractID);
        public List<PmCostMade> GetPmCostMade(string Pm_Cost_PlannedID);
        public List<vContractProject> GetAllContractProjectByTechLeader(int? TechLeaderID, string InspectorName);
        public bool IsExistMilesStone(MilesTonesDTO milesTonesDTO);
        public int existMilesStonesValue(string ID);
        public List<vPM_Mobilization_Combined> GetMobilization(int contractID);
        public List<PM_Mobilization_Made> GetMobilizationMade(string mobilizationPlannedID);
        public List<Pm_Type_OutsourcedServicesDTO> GetAllOutsourcedServiceNames();
        public List<vPM_OutsourcedServices_Combined> GetOutsourcedServicesCombined(int ContractID);
        public List<vPM_OutsourcedServices_Made_Combined> GetOutsourcedServicesCombinedMade(string ID_OutsourcedServices_Planned);
        public List<PM_Type_Hours> GetpmTypeHH();
        public List<PM_Function_HH> GetPMFunctionHHs();
        public List<vPM_Mam_Hours> GetpmHours(int ContractID);
        public List<PM_Scope_Traking> GetScopeTrajing(int ContractID);
        public List<PM_Scope_Change> GetScopeChanges(int ContractID);
        public List<GetFinanceMilestones> GetAllMilestonesItem(int contractID);
        public List<Functions> GetAllFunctions();
        public List<vPM_Functions_MilestoneType> GetAllFunctionsMilesstoneType(string MilesstoneTypeID);
        public List<DisplacementServices> GetAllDisplacementServices();

        // DELETE Methods
        public bool DeleteMilestones(string ID);
        public bool DeleteAcquisition(string ID);
        public bool DeleteAcquisitionMade(string ID, string AquisitionPlannedID);
        public bool DeleteTypeOfCost(string ID);
        public bool DeletePredictedCost(string ID);
        public bool DeleteCostMade(string ID);
        public bool DeleteMobilization(string ID);
        public bool DeleteMobilizationMade(string ID);
        public bool DeleteOutsourcedServicesPlanned(string ID);
        public bool DeleteOutsourcedServicesMade(string ID);
        public bool DeleteHH(string ID);
        public bool DeletepMScopeTraking(string ID);
        public bool DeleteScopeChange(string ID);
        public bool DeleteFunctionID(string ID);



    }
}
