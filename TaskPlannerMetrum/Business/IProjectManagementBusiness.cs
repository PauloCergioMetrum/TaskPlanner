using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IProjectManagementBusiness
    {


        public OrderInformationDTO GetOrderInformation(int id);


        public bool CreateMilesTones(MilesTonesDTO milesTones);
        public List<GetMilestones> GetMilestonesNames(int contractID);
        public bool UpdateMilesTones(MilesTonesDTO milesTonesDTO);
        public bool DeleteMilestones(string ID, int MilestonesID);
        public bool MilesTonesDelete(int ID);
        public List<Positions> GetPositionsByGrup();
        public bool CreateAcquisitionsPlanned(AcquisitionsDTO acquisitions);
        public bool DeleteAcquisition(string ID);
        public bool UpdateAcquisition(AcquisitionsDTO acquisitions);
        public bool CreateAcquisitionsMade(AcquisitionMadeDTO acquisitionMade);
        public bool DeleteAcquisitionMade(string ID, string AquisitionPlannedID);
        public bool UpdateAcquisitionMadeItem(AcquisitionMadeDTO acquisitionMade);
        public List<vPMAcquisitionCombined> GetAcquisitions(int ContractID);
        public List<vPMAcquisitionCost> GetAcquisitionsMade(string AquisitionPlannedID);
        public bool DeleteTypeOfCost(int ID); 
        public bool DeletePredictedCost(string ID);
        public List<vPm_Cost_Planned> GetPmCostPlanned(int ContractID);

        public bool CreateOrUpdateCostMade(PmCostMade pmCostMade);
        public bool DeleteCostMade(string ID);

        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned);
        public List<vPmCostMade> GetPmCostMade(string Pm_Cost_PlannedID);
        public List<vContractProject> GetAllContractProjectByTechLeader(int? TechLeaderID, string InspectorName);

        public List<BusinessUnitDto> GetAllBussinesUnit(int ContractID);
        public List<MilestonesItem> GetMilestoneItem(int contractID);
        public int existMilesStonesValue(string ID);
        public bool UpdateMilesValue(MilesTonesDTO milesTonesDTO, int ID);


        //MOBILIZAÇÃO 
        public List<vPM_Mobilization_Combined> GetMobilization(int contractID);

        public List<vPM_Mobilization_Made> GetMobilizationMade(string mobilizationPlannedID);

        public bool CreateMobilizationPlanned(PM_Mobilization_Planned mobilizationPlanned);

        public bool CreateMobilizationMade(PM_Mobilization_Made mobilizationMade);

        public bool UpdateMobilization(string ID);
        public bool UpdateMobilizationMaded(string ID);
        public bool DeleteMobilization(string ID);
        public bool DeleteMobilizationMade(string ID);

        public List<Pm_Type_OutsourcedServicesDTO> GetAllOutsourcedServiceNames();
        public List<ActivityPlanHH> ActivityPlanHH(int contractID);
        public List<ActivityPlanHHTable> ActivityPlanHHTable(int contractID);
        public ActivityPlanHHDetail GetActivityPlanDetails(int contractID);


        public List<vPM_OutsourcedServices_Combined> GetOutsourcedServicesCombined(int ContractID);

        public List<vPM_OutsourcedServices_Made_Combined> GetOutsourcedServicesCombinedMade(string ID_OutsourcedServices_Planned);

        public bool CreateoutsourcedServicePlanned(PM_OutsourcedServices_Planned createoutsourcedServicePlanned);
        public bool UpdateoutsourcedServicePlanned(PM_OutsourcedServices_Planned updatedService);

        public bool UpdateoutsourcedServiceMade(PM_OutsourcedServices_Made updatedServiceMade);
        public bool CreateoutsourcedServiceMade(PM_OutsourcedServices_Made createoutsourcedServiceMade);

        public bool DeleteOutsourcedServicesPlanned(string ID);

        public bool DeleteOutsourcedServicesMade(string ID);


        //HH
        public List<PM_Type_Hours> GetpmTypeHH();

        public List<PM_Function_HH> GetPMFunctionHHs();

        public bool CreateHH(PM_Man_Hours createHH);

        public bool UpdateHH(PM_Man_Hours updateHH);
        public bool DeleteHH(string ID);
        public List<vPM_Mam_Hours> GetpmHours(int ContractID);


        // ACOMPANHAMENTO DE ESCOPO 

        public bool CreatScopeTraking(PM_Scope_Traking pMScopeTraking);
        public bool UpdatepMScopeTraking(PM_Scope_Traking pMScopeTraking);

        public bool DeletepMScopeTraking(string ID);
        public List<PM_Scope_Traking> GetScopeTrajing(int ContractID);
        //public List<MilestoneType_HH_Details> ManagementSalesOrderDetailsHH(string MilestonesValueID);




        //  ACOMPANHAMENTO DE ESCOPO  Mudança de Escopo 

        public bool CreateScopeChange(PM_Scope_Change pM_Scope_Change);
        public bool UpdateScopeChange(PM_Scope_Change pM_Scope_Change);

        public bool DeleteScopeChange(string ID);
        public List<PM_Scope_Change> GetScopeChanges(int ContractID);


        public List<GetFinanceMilestones> GetAllMilestonesItem(int contractID);


        //LISTA DE FUNÇÃO , 
        public List<Functions> GetAllFunctions();
        public bool CreateFunctions_MilestoneType(PM_Functions_MilestoneType dto);
        public List<vPM_Functions_MilestoneType> GetAllFunctionsMilesstoneType(string MilesstoneTypeID);

        public bool DeleteFunctionID(string ID);

        public bool UpdateFunctionID(string ID, PM_Functions_MilestoneType dto);

        public List<DisplacementServices> GetAllDisplacementServices();

        public bool CreatePM_MilestonesType(PM_MilestonesType dto);
        public List<vPM_MilestoneType> GetAllPM_MilestonesType(string MilestonesValueID);

        public bool DeletePM_MilestonesType(string ID);

        //hh

        public List<vMileStonesValue> GetAllMileStonesValue(int ContractID);
        public List<GetMilestoneType_HH_Details> GetHHByID(string MilestonesValueID);
        public List<VgetMilestoneType_HH_Details> GetHHByIDTaskAsync(int ContractID);




        //TAP

        public bool UpdateInfoGeneral(ProjectInfoGeneralDTO request);
        public bool CreateTapScope(PM_TAP_Scope pM_TAP_Scope);
        public List<PM_TAP_RiskLevel> GetAllProjectCharter();
        public bool UpdateProjectScope(int ID, PM_TAP_Scope updatedScope);
        public List<UserVO> GetAllUsersGercon();

        // GET ALL TAP  MODAL DE CADASTRDO
        public ProjectInfoGeneralDTO GetProjectInfoByContractId(int contractId);
        public List<OrderManagementInfo> OrderManagementInfo(int contractID);
        public List<vRightCardValue> RightCardValues(int contractID);

        // STATUS REPORTS


        CombinedChartsResult GetCombinedCharts(int contractId);

        Task<List<GetAllMilesStones>> GetMilestonesAsync(string milestoneId);







    }




}



