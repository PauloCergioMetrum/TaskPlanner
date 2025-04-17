using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public interface IProjectManagementRepository    {


        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned);
        public int CreateMilestonesItem(MilestonesItem milestones);
        public bool CreateMilestonesValue(MilestonesValue milestones);
        public bool CreateAcquisitionsItem(PMAcquisitionPlanned acquisitions);
        public bool CreateAcquisitionMadeItem(PMAcquisitionMade acquisitionMade);
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
        public bool CreateTapScope(PM_TAP_Scope pM_TAP_Scope);

       

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
        public bool IsExistMilesStone(MilesTonesDTO milesTonesDTO);
        public int existMilesStonesValue(string ID);
        public bool UpdateScopeInfo(int contractId, PM_TAP_Scope scope);
        public bool UpdateInfoContract(int contractId, ProjectManagementGeneralInfo infoContract);
        public bool UpdateInfoTap(int contractId, ProjectManagementGeneralInfo infoContractTap);

        public bool UpdateInfoGenralClients(int contractId, List<PM_Information_General> contactClients );
        public bool UpdateProjectScope(int ID, PM_TAP_Scope updatedScope);
        public bool UpdateResouces(int ContractID, PM_TAP_Resources UpdateResouces);
        public Model.Contracts GetForecastByID(int id);



        // LIST Methods



        public vContractList GetForecastByIDView(int id);
        public List<GetMilestones> GetMilestonesNames(int contractID);
        public List<MilestonesItem> GetMilestoneItem(int contractID);
        public MilestonesValue GetMilestonesValueByID(int milestonesID);
        public List<Positions> GetPositionsByGrup();
        public bool ExistAcquisition(string ID);
        public bool ExistAcquisitionMade(string ID);
        public List<vPMAcquisitionCombined> GetAcquisitions(int ContractID);
        public List<vPMAcquisitionCost> GetAcquisitionsMade(string AquisitionPlannedID); 
        public List<vPm_Cost_Planned> GetPmCostPlanned(int ContractID);
        public List<vPmCostMade> GetPmCostMade(string Pm_Cost_PlannedID);
        public List<vContractProject> GetAllContractProjectByTechLeader(int? TechLeaderID, string InspectorName);

        public List<vPM_Mobilization_Combined> GetMobilization(int contractID);
        public List<vPM_Mobilization_Made> GetMobilizationMade(string mobilizationPlannedID);
        public List<Pm_Type_OutsourcedServicesDTO> GetAllOutsourcedServiceNames();
        public List<vPM_OutsourcedServices_Combined> GetOutsourcedServicesCombined(int ContractID);
        public List<vPM_OutsourcedServices_Made_Combined> GetOutsourcedServicesCombinedMade(string ID_OutsourcedServices_Planned);
        public List<PM_Type_Hours> GetpmTypeHH();
        public List<PM_Function_HH> GetPMFunctionHHs();
        public List<vPM_Mam_Hours> GetpmHours(int ContractID);
        public List<PM_Scope_Traking> GetScopeTrajing(int ContractID);
        public List<PM_Scope_Change> GetScopeChanges(int ContractID);
        public List<GetFinanceMilestones> GetAllMilestonesItem(int contractID);
        public List<OrderManagementInfo> OrderManagementInfo(int contractID);


        public List<Functions> GetAllFunctions();
        public List<vPM_Functions_MilestoneType> GetAllFunctionsMilesstoneType(string MilesstoneTypeID);
        public List<DisplacementServices> GetAllDisplacementServices();
        public List<vPM_MilestoneType> GetAllPM_MilestonesType(string MilestonesValueID);
        public List<vMileStonesValue> GetAllMileStonesValue(int ContractID);
        public List<GetMilestoneType_HH_Details> GetHHByID(string MilestonesValueID);
        //public List<VgetMilestoneType_HH_Details> GetHHByIDTaskAsync(int ContractID);


        public List<PM_TAP_RiskLevel> GetAllProjectCharter();
        public List<UserVO> GetAllUsersGercon();
        public ProjectInfoGeneralDTO GetProjectInfoByContractId(int contractId);
        public List<BusinessUnitDto> GetAllBussinesUnit(int ContractID);
        public List<ActivityPlanHH> ActivityPlanHH(int contractID);
        public List<ActivityPlanHHTable> ActivityPlanHHTable(int contractID);
        public ActivityPlanHHDetail GetActivityPlanDetails(int contractID);
        public List<MilestonesItem> GetAllMilestones();
        public List<vRightCardValue>RightCardValues(int contractID);
        // STATUS REPORTS
        List<vIndirectcostChart> GetIndirectCostChartsByContract(int contractID);
        List<vAcquisitionChart> GetStatusReportsGraph(int contractID);
        List<vhhGraphicDetail> GetHhGraphicDetail(int contractID);   

        // DELETE Methods

        public bool MilesTonesDelete(int ID);
        public bool DeleteMilestones(string ID, int MilestonesID);
        public bool DeleteAcquisition(string ID);
        public bool DeleteAcquisitionMade(string ID, string AquisitionPlannedID);
        public bool DeleteTypeOfCost(int ID);
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
        public bool DeletePM_MilestonesType(string ID);

        Task<List<GetAllMilesStones>> GetMilestonesAsync(string milestoneId);


        Task<List<GetMilestonesExpected>> GetMilestoneNoExpected(string milestoneId);

        Task<List<GetMilestonesExpected>> GetMilestonesExpected(string milestoneId);


        public List<CombinedMilestonesData> vCombinedMilestonesData(int contractID);












    }
}
