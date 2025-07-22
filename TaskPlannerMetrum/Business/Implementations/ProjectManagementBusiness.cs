using Microsoft.Extensions.WebEncoders.Testing;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

using TaskPlannerMetrum.Repository.ProjectManagement;
using TaskPlannerMetrum.Repository.Generic;
using TaskPlannerMetrum.Model.ModelViews;
using System.Diagnostics.Contracts;
using TaskPlannerMetrum.Data.VO;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Threading.Tasks;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ProjectManagementBusiness : IProjectManagementBusiness
    {
        private readonly IProjectManagementRepository _projectmanagementRepository;
        //private int dynamic;

        public ProjectManagementBusiness(IProjectManagementRepository projectmanagementBusiness)
        {
            _projectmanagementRepository = projectmanagementBusiness;
        }

        public bool UpdateForecast(ProjectManagementGeneralInfo forecast)
        {
            try
            {
                var UpdateForecast = _projectmanagementRepository.GetForecastByID(forecast.Id);
                if (UpdateForecast != null)
                {
                    UpdateForecast.ValidityEndDate = forecast.ValidityEndDate;
                    UpdateForecast.ValidityStartDate = forecast.ValidityStartDate;
                    UpdateForecast.PredictedMarkup = forecast.PredictedMarkup;
                    UpdateForecast.PredictedSavings = forecast.PredictedSavings;
                    return _projectmanagementRepository.UpdateForecast(UpdateForecast);


                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }


        }

        public OrderInformationDTO GetOrderInformation(int id)
        {

            try
            {
                var OrderInformationByID = _projectmanagementRepository.GetForecastByIDView(id);
                if (OrderInformationByID != null)
                {
                    DateTime? validityStartDate = OrderInformationByID.ValidityStartDate;
                    DateTime? validityEndDate = OrderInformationByID.ValidityEndDate;
                    string validityDateFormatter = validityStartDate == null ? " " : validityStartDate.Value.Date.ToString("dd/MM/yyyy") + "-" + validityEndDate == null ? "" : validityEndDate.Value.Date.ToString("dd/MM/yyyy");
                    OrderInformationDTO orderInformation = new OrderInformationDTO
                    {
                        Client = OrderInformationByID.ClientName,
                        SalesOrder = OrderInformationByID.InternalCode,
                        Validity = validityDateFormatter,
                        BusinessUnit = OrderInformationByID.BusinessUnit,
                        WorkspaceName = OrderInformationByID.WorkspaceName,

                        PredictedSavings = OrderInformationByID.PredictedSavings == null ? "" : OrderInformationByID.PredictedSavings.ToString(),


                        PredictedMarkup = OrderInformationByID.PredictedMarkup == null ? "" : OrderInformationByID.PredictedMarkup.ToString(),

                        ValidityStartDate = validityStartDate,
                        ValidityEndDate = validityEndDate,
                    };
                    return orderInformation;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }



        public int existMilesStonesValue(string ID)
        {
            return _projectmanagementRepository.existMilesStonesValue(ID);
        }
        public bool MilesTonesDelete(int ID)
        {
            return _projectmanagementRepository.MilesTonesDelete(ID);
        }

        public bool UpdateMilesValue(MilesTonesDTO milesTonesDTO, int ID)
        {
            return _projectmanagementRepository.UpdateMilesValue(milesTonesDTO, ID);
        }



        public bool CreateMilesTones(MilesTonesDTO milesTonesDTO)
        {
            try
            {

                var ExixteValue = _projectmanagementRepository.existMilesStonesValue(milesTonesDTO.ID);

                if (ExixteValue == 0)
                {
                    int milestonesID = _projectmanagementRepository.CreateMilestonesItem(new MilestonesItem
                    {
                        ContractID = milesTonesDTO.ContractID,
                        Name = milesTonesDTO.Name,
                    });

                    _projectmanagementRepository.CreateMilestonesValue(new MilestonesValue
                    {
                        ID = milesTonesDTO.ID,
                        MilestonesID = milestonesID,
                        Baseline = milesTonesDTO.Baseline,
                        Description = milesTonesDTO.Description,
                        ExecutedDate = milesTonesDTO.ExecutedDate,
                        RescheduledDate = milesTonesDTO.RescheduledDate,
                        ScheduledDate = milesTonesDTO.ScheduledDate,
                        TypeID = milesTonesDTO.TypeID,
                        Value = milesTonesDTO.Value,
                        TypeMilestonesID = milesTonesDTO.TypeMilestonesID,
                        TechLeadID = milesTonesDTO.TechLeadID,
                        BusinessUnitID = milesTonesDTO.BusinessUnitID,



                    });
                }
                else
                {
                    _projectmanagementRepository.UpdateMilesValue(milesTonesDTO, ExixteValue);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }



        public List<GetMilestones> GetMilestonesNames(int contractID)
        {
            return _projectmanagementRepository.GetMilestonesNames(contractID);
        }


        public bool DeleteMilestones(string ID, int MilestonesID)
        {
            return _projectmanagementRepository.DeleteMilestones(ID, MilestonesID);
        }


        public bool UpdateMilesTones(MilesTonesDTO milesTonesDTO)
        {
            if (milesTonesDTO != null)
            {
                _projectmanagementRepository.UpdateMilesTones(new MilestonesValue
                {
                    MilestonesID = milesTonesDTO.MilestonesID,
                    ID = milesTonesDTO.ID,
                    Baseline = milesTonesDTO.Baseline,
                    Description = milesTonesDTO.Description,
                    ExecutedDate = milesTonesDTO.ExecutedDate,
                    RescheduledDate = milesTonesDTO.RescheduledDate,
                    ScheduledDate = milesTonesDTO.ScheduledDate,
                    TypeID = milesTonesDTO.TypeID,
                    TypeMilestonesID = milesTonesDTO.TypeMilestonesID,
                    TechLeadID = milesTonesDTO.TechLeadID,
                    BusinessUnitID = milesTonesDTO.BusinessUnitID,





                });

                return true;

            }
            return false;
        }




        public List<Positions> GetPositionsByGrup()
        {
            return _projectmanagementRepository.GetPositionsByGrup();

        }

        public bool CreateAcquisitionsPlanned(AcquisitionsDTO acquisitionsDTO)
        {
            try
            {


                if (_projectmanagementRepository.ExistAcquisition(acquisitionsDTO.ID))
                {

                    return _projectmanagementRepository.UpdateAcquisition(acquisitionsDTO);
                }
                else
                {
                    var CreateAcquisitionsItem = _projectmanagementRepository.CreateAcquisitionsItem(new PMAcquisitionPlanned
                    {
                        ID = acquisitionsDTO.ID,
                        TypeAcquisitionID = acquisitionsDTO.TypeAcquisitionID,
                        AmountPlanned = acquisitionsDTO.AmountPlanned,
                        Description = acquisitionsDTO.Description,
                        ContractID = acquisitionsDTO.ContractID,
                        AmountValue = acquisitionsDTO.AmountValue,
                    });
                    return true;

                }


            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateAcquisition(AcquisitionsDTO acquisitionsDTO)
        {
            try
            {
                _projectmanagementRepository.UpdateAcquisition(acquisitionsDTO);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        public bool CreateAcquisitionsMade(AcquisitionMadeDTO acquisitionMade)
        {
            try
            {

                if (_projectmanagementRepository.ExistAcquisitionMade(acquisitionMade.ID))
                {

                    return _projectmanagementRepository.UpdateAcquisitionMadeItem(acquisitionMade);
                }
                else
                {
                    var CreateAcquisitionsItem = _projectmanagementRepository.CreateAcquisitionMadeItem(new PMAcquisitionMade
                    {
                        ID = acquisitionMade.ID,
                        //StatusAcquistionID = acquisitionMade.StatusAcquistionID,
                        Amount = acquisitionMade.Amount,
                        AquisitionPlannedID = acquisitionMade.AquisitionPlannedID,
                        Value = acquisitionMade.Value,
                        DateAcquisition = acquisitionMade.DateAcquisition,
                        DateAcquisitionDelivery = acquisitionMade.DateAcquisitionDelivery,
                        Description = acquisitionMade.Description,
                    });
                    return true;

                }


            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateAcquisitionMadeItem(AcquisitionMadeDTO acquisitionMade)
        {
            try
            {
                _projectmanagementRepository.UpdateAcquisitionMadeItem(acquisitionMade);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteAcquisitionMade(string ID, string AquisitionPlannedID)
        {
            try
            {
                _projectmanagementRepository.DeleteAcquisitionMade(ID, AquisitionPlannedID);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteAcquisition(string ID)
        {
            try
            {
                _projectmanagementRepository.DeleteAcquisition(ID);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }





        public bool DeleteTypeOfCost(int ID)
        {
            return _projectmanagementRepository.DeleteTypeOfCost(ID);
        }



        public bool DeletePredictedCost(string ID)
        {
            return _projectmanagementRepository.DeletePredictedCost(ID);
        }

        public List<vPm_Cost_Planned> GetPmCostPlanned(int ContractID)
        {
            return _projectmanagementRepository.GetPmCostPlanned(ContractID);
        }

        public bool CreateOrUpdateCostMade(PmCostMade pmCostMade)
        {
            return _projectmanagementRepository.CreateOrUpdateCostMade(pmCostMade);
        }


        public bool DeleteCostMade(string ID)
        {
            return _projectmanagementRepository.DeleteCostMade(ID);
        }


        public List<vPmCostMade> GetPmCostMade(string Pm_Cost_PlannedID)
        {
            return _projectmanagementRepository.GetPmCostMade(Pm_Cost_PlannedID);
        }

        public List<vContractProject> GetAllContractProjectByTechLeader(int? TechLeaderID, string InspectorName)
        {
            return _projectmanagementRepository.GetAllContractProjectByTechLeader(TechLeaderID, InspectorName);
        }




        // MOBILIZAÇÃO
        public List<vPM_Mobilization_Combined> GetMobilization(int contractID)
        {
            return _projectmanagementRepository.GetMobilization(contractID);
        }

        public List<vPM_Mobilization_Made> GetMobilizationMade(string mobilizationPlannedID)
        {
            return _projectmanagementRepository.GetMobilizationMade(mobilizationPlannedID);
        }

        public bool CreateMobilizationPlanned(PM_Mobilization_Planned mobilizationPlanned)
        {

            return _projectmanagementRepository.CreateMobilizationPlanned(mobilizationPlanned);
        }

        public bool CreateMobilizationMade(PM_Mobilization_Made mobilizationMade)
        {
            return _projectmanagementRepository.CreateMobilizationMade(mobilizationMade);
        }

        public bool UpdateMobilization(string ID)
        {
            return _projectmanagementRepository.UpdateMobilization(ID);
        }

        public bool UpdateMobilizationMaded(string ID)
        {
            return _projectmanagementRepository.UpdateMobilizationMaded(ID);
        }

        public bool DeleteMobilization(string ID)
        {
            try
            {
                _projectmanagementRepository.DeleteMobilization(ID);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMobilizationMade(string ID)
        {
            try
            {
                _projectmanagementRepository.DeleteMobilizationMade(ID);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        // SERVIÇO TERCERIZADO



        public List<Pm_Type_OutsourcedServicesDTO> GetAllOutsourcedServiceNames()
        {
            return _projectmanagementRepository.GetAllOutsourcedServiceNames();
        }

        public List<vPM_OutsourcedServices_Combined> GetOutsourcedServicesCombined(int ContractID)
        {
            return _projectmanagementRepository.GetOutsourcedServicesCombined(ContractID);
        }


        public List<vPM_OutsourcedServices_Made_Combined> GetOutsourcedServicesCombinedMade(string ID_OutsourcedServices_Planned)
        {
            return _projectmanagementRepository.GetOutsourcedServicesCombinedMade(ID_OutsourcedServices_Planned);
        }

        public bool CreateoutsourcedServicePlanned(PM_OutsourcedServices_Planned createoutsourcedServicePlanned)
        {
            try
            {
                _projectmanagementRepository.CreateoutsourcedServicePlanned(createoutsourcedServicePlanned);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateoutsourcedServicePlanned(PM_OutsourcedServices_Planned updatedService)
        {
            try
            {
                _projectmanagementRepository.UpdateoutsourcedServicePlanned(updatedService);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool UpdateoutsourcedServiceMade(PM_OutsourcedServices_Made updatedServiceMade)
        {
            try
            {
                return _projectmanagementRepository.UpdateoutsourcedServiceMade(updatedServiceMade);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateoutsourcedServiceMade(PM_OutsourcedServices_Made createoutsourcedServiceMade)
        {
            try
            {
                return _projectmanagementRepository.CreateoutsourcedServiceMade(createoutsourcedServiceMade);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOutsourcedServicesPlanned(string ID)
        {
            return _projectmanagementRepository.DeleteOutsourcedServicesPlanned(ID);
        }

        public bool DeleteOutsourcedServicesMade(string ID)
        {
            return _projectmanagementRepository.DeleteOutsourcedServicesMade(ID);
        }


        //HH
        public List<PM_Type_Hours> GetpmTypeHH()
        {
            return _projectmanagementRepository.GetpmTypeHH();
        }

        public List<PM_Function_HH> GetPMFunctionHHs()
        {
            return _projectmanagementRepository.GetPMFunctionHHs();
        }

        public bool CreateHH(PM_Man_Hours createHH)
        {
            return _projectmanagementRepository.CreateHH(createHH);
        }

        public bool UpdateHH(PM_Man_Hours updateHH)
        {
            return _projectmanagementRepository.UpdateHH(updateHH);
        }

        public bool DeleteHH(string ID)
        {
            return _projectmanagementRepository.DeleteHH(ID);
        }

        public List<vPM_Mam_Hours> GetpmHours(int ContractID)
        {
            return _projectmanagementRepository.GetpmHours(ContractID);
        }


        // ACOMPNHAMENTO DE ESCOPO 
        public bool CreatScopeTraking(PM_Scope_Traking pMScopeTraking)
        {
            return _projectmanagementRepository.CreatScopeTraking(pMScopeTraking);
        }

        public bool UpdatepMScopeTraking(PM_Scope_Traking pMScopeTraking)
        {
            return _projectmanagementRepository.UpdatepMScopeTraking(pMScopeTraking);
        }

        public bool DeletepMScopeTraking(string ID)
        {
            return _projectmanagementRepository.DeletepMScopeTraking(ID);
        }

        public List<PM_Scope_Traking> GetScopeTrajing(int ContractID)
        {
            return _projectmanagementRepository.GetScopeTrajing(ContractID);
        }



        //  ACOMPANHAMENTO DE ESCOPO  Mudança de Escopo 
        public bool CreateScopeChange(PM_Scope_Change pM_Scope_Change)
        {
            return _projectmanagementRepository.CreateScopeChange(pM_Scope_Change);
        }

        public bool UpdateScopeChange(PM_Scope_Change pM_Scope_Change)
        {
            return _projectmanagementRepository.UpdateScopeChange(pM_Scope_Change);
        }

        public bool DeleteScopeChange(string ID)
        {
            return _projectmanagementRepository.DeleteScopeChange(ID);
        }

        public List<PM_Scope_Change> GetScopeChanges(int ContractID)
        {
            return _projectmanagementRepository.GetScopeChanges(ContractID);
        }

        public List<GetFinanceMilestones> GetAllMilestonesItem(int contractID)
        {
            return _projectmanagementRepository.GetAllMilestonesItem(contractID);
        }





        public List<Functions> GetAllFunctions()
        {
            return _projectmanagementRepository.GetAllFunctions();
        }

        public bool CreateFunctions_MilestoneType(PM_Functions_MilestoneType dto)
        {
            return _projectmanagementRepository.CreateFunctions_MilestoneType(dto);
        }

        public List<vPM_Functions_MilestoneType> GetAllFunctionsMilesstoneType(string MilesstoneTypeID)
        {
            return _projectmanagementRepository.GetAllFunctionsMilesstoneType(MilesstoneTypeID);
        }

        public bool DeleteFunctionID(string ID)
        {
            return _projectmanagementRepository.DeleteFunctionID(ID);
        }

        public bool UpdateFunctionID(string ID, PM_Functions_MilestoneType dto)
        {
            return _projectmanagementRepository.UpdateFunctionID(ID, dto);
        }

        public List<DisplacementServices> GetAllDisplacementServices()
        {
            return _projectmanagementRepository.GetAllDisplacementServices();
        }

        public bool CreatePM_MilestonesType(PM_MilestonesType dto)
        {
            return _projectmanagementRepository.CreatePM_MilestonesType(dto);
        }

        public List<vPM_MilestoneType> GetAllPM_MilestonesType(string MilestonesValueID)
        {
            return _projectmanagementRepository.GetAllPM_MilestonesType(MilestonesValueID);
        }

        public bool DeletePM_MilestonesType(string ID)
        {
            return _projectmanagementRepository.DeletePM_MilestonesType(ID);
        }



        public List<vMileStonesValue> GetAllMileStonesValue(int ContractID)
        {

            return _projectmanagementRepository.GetAllMileStonesValue(ContractID);
        }



        public string SetMilestoneFinalizedStatus(FinalizedStatusDto dto)
        {
            return _projectmanagementRepository.SetMilestoneFinalizedStatus(dto);
        }

        public List<vMilestoneStatusCalculation> GetMilestoneStatusCalculation(int ContractID)
        {
            return _projectmanagementRepository.GetMilestoneStatusCalculation(ContractID);
        }

        public List<GetMilestoneType_HH_Details> GetHHByID(string MilestonesValueID)
        {
            return _projectmanagementRepository.GetHHByID(MilestonesValueID);
        }
        // TAP
        public bool UpdateInfoGeneral(ProjectInfoGeneralDTO infoGneral)
        {
            //atualizar escopo
            if (_projectmanagementRepository.UpdateScopeInfo(infoGneral.ContractID, infoGneral.Scopes) == false) return false;

            if (_projectmanagementRepository.UpdateResouces(infoGneral.ContractID, infoGneral.Resources) == false) return false;


            //Atualizar informações gerais do contrato
            if (_projectmanagementRepository.UpdateInfoContract(infoGneral.ContractID, infoGneral.ProjectInfo) == false) return false;
            //Atualizar informações gerais da TAP
            if (_projectmanagementRepository.UpdateInfoTap(infoGneral.ContractID, infoGneral.ProjectInfo) == false) return false;
            //Atualizar informações gerais Partes Interesadas
            if (_projectmanagementRepository.UpdateInfoGenralClients(infoGneral.ContractID, infoGneral.ProjectInfo.ContactClients) == false) return false;





            return true;
        }

        public bool CreateTapScope(PM_TAP_Scope pM_TAP_Scope)
        {
            return _projectmanagementRepository.CreateTapScope(pM_TAP_Scope);
        }

        public List<PM_TAP_RiskLevel> GetAllProjectCharter()
        {
            return _projectmanagementRepository.GetAllProjectCharter();
        }


        public List<UserVO> GetAllUsersGercon()
        {
            return _projectmanagementRepository.GetAllUsersGercon();
        }

        public bool UpdateProjectScope(int ID, PM_TAP_Scope updatedScope)
        {
            return _projectmanagementRepository.UpdateProjectScope(ID, updatedScope);
        }

        public ProjectInfoGeneralDTO GetProjectInfoByContractId(int contractId)
        {
            return _projectmanagementRepository.GetProjectInfoByContractId(contractId);
        }

        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned)
        {
            return _projectmanagementRepository.CreateOrUpdatePredictedCost(pmCostPlanned);
        }

        public List<BusinessUnitDto> GetAllBussinesUnit(int ContractID)
        {
            return _projectmanagementRepository.GetAllBussinesUnit(ContractID);
        }

        public List<MilestonesItem> GetMilestoneItem(int contractID)
        {
            return _projectmanagementRepository.GetMilestoneItem(contractID);
        }

        public List<ActivityPlanHH> ActivityPlanHH(int contractID)
        {
            return _projectmanagementRepository.ActivityPlanHH(contractID);
        }
        public List<ActivityPlanHHTable> ActivityPlanHHTable(int contractID)
        {
            return _projectmanagementRepository.ActivityPlanHHTable(contractID);
        }

        public ActivityPlanHHDetail GetActivityPlanDetails(int contractID)
        {
            List<MilestonesItem> milesTonesList = _projectmanagementRepository.GetAllMilestones();
            List<MilestonesItem> milesTonesByContractID = milesTonesList.Where(c => c.ContractID == contractID).ToList();
            var activitPlanDetaisByContractID = _projectmanagementRepository.GetActivityPlanDetails(contractID);
            List<string> seniorityLevelList = activitPlanDetaisByContractID.ActivityPlanHHTable.Select(p => p.ExecutorSeniorityLevel).Distinct().ToList();
            List<ActivicPlannGrupByMilesTone> ListTotals = new List<ActivicPlannGrupByMilesTone>();
            List<Details> detailsList = new List<Details>();

            List<ActivicPlannGrupByMilesTone> milesTonesLsit = new List<ActivicPlannGrupByMilesTone>();
            foreach (var milestones in milesTonesByContractID)
            {
                ActivicPlannGrupByMilesTone milesTone = new ActivicPlannGrupByMilesTone
                {
                    MilestonesID = milestones.ID,
                    MilestoneName = milestones.Name,
                    Executers = activitPlanDetaisByContractID.ActivityPlanHHTable?
                        .Where(ex => ex.MilestoneName == milestones.Name)
                        .Select(ex => new Executors
                        {
                            Executor = ex.ExecutorUserName ?? "Unknown",
                            TotalHours = ex.TotalHours ?? 0,
                            MilesTonesName = milestones.Name,
                            Details = activitPlanDetaisByContractID.ActivityPlanHHTable?
                                .Where(n => n.MilestoneName == milestones.Name)
                                .Select(dt => new Model.Details
                                {
                                    BusinesUnit = dt.BusinessUnit ?? "N/A",
                                    ExecutedManHour = dt.ExecutedManHour ?? 0,
                                    ExecutorSeniorityLevel = dt.ExecutorSeniorityLevel ?? "Unknown",
                                    MilesTonesName = dt.MilestoneName ?? "Unknown",
                                    PlannedManHour = dt.PlannedManHour ?? 0,
                                    SeniorLevel = dt.ExecutorSeniorityLevel ?? "Unknown"
                                }).ToList()
                        }).ToList()
                };
                milesTonesLsit.Add(milesTone);
            }

            activitPlanDetaisByContractID.ActivicPlannGrupByMilesTone = milesTonesLsit;
            return activitPlanDetaisByContractID;

        }

        public List<OrderManagementInfo> OrderManagementInfo(int contractID)
        {
            return _projectmanagementRepository.OrderManagementInfo(contractID);
        }

        public List<vRightCardValue> RightCardValues(int contractID)
        {
            return _projectmanagementRepository.RightCardValues(contractID);
        }



        public List<vPMAcquisitionCombined> GetAcquisitions(int ContractID)
        {
            return _projectmanagementRepository.GetAcquisitions(ContractID);
        }

        public List<vPMAcquisitionCost> GetAcquisitionsMade(string AquisitionPlannedID)
        {
            return _projectmanagementRepository.GetAcquisitionsMade(AquisitionPlannedID);
        }

        public CombinedChartsResult GetCombinedCharts(int contractID)
        {
            var indirectGrouped = GetIndirectCostsGrouped(contractID);
            var mobilizationGrouped = GetMobilizationGrouped(contractID);
            var statusGrouped = GetStatusReportsGrouped(contractID);
            var orderManagementGrouped = GetOrderManagementGrouped(contractID);
            var outsourcedGrouped = GetOutsourcedServicesGrouped(contractID);
            var hhGraphicGrouped = GetHhGraphicGrouped(contractID);

            return new CombinedChartsResult
            {
                IndirectCosts = indirectGrouped,
                Mobilization = mobilizationGrouped,
                StatusReports = statusGrouped,
                OrderManagement = orderManagementGrouped,
                OutsourcedServices = outsourcedGrouped,
                HhGraphicDetails = hhGraphicGrouped
            };
        }

        private List<IndirectCostsResult> GetIndirectCostsGrouped(int contractID)
        {
            var indirectCosts = _projectmanagementRepository.GetIndirectCostChartsByContract(contractID);

            return indirectCosts
                .GroupBy(chart => new
                {
                    chart.ContractID,
                    chart.PlannedId,
                    chart.TypeID,
                    chart.TypeDescription,
                    chart.TotalPlanned
                })
                .Select(group => new IndirectCostsResult
                {
                    ContractID = group.Key.ContractID,
                    PlannedId = group.Key.PlannedId,
                    TypeID = group.Key.TypeID,
                    TypeDescription = group.Key.TypeDescription,
                    PlannedTotal = group.Key.TotalPlanned ?? 0.0,
                    MadeDetails = group.Select(x => new MadeDetailsResult
                    {
                        AcquisitionMadeDTO = x,
                        MadeTotal = x.MadeTotal ?? 0.0
                    }).ToList()
                })
                .ToList();
        }
        private List<MobilizationResult> GetMobilizationGrouped(int contractID)
        {
            var mobilizationData = _projectmanagementRepository.GetMobilization(contractID);

            return mobilizationData
                .GroupBy(m => m.ContractID)
                .Select(group => new MobilizationResult
                {
                    AccommodationPlanned = group.Sum(x => x.CountAccommodation),
                    FoodPlanned = group.Sum(x => x.CountFood),
                    AirTransportPlanned = group.Sum(x => x.CountAirTransport),
                    GroundTransportPlanned = group.Sum(x => x.CountGroundTransport),
                    OthersPlanned = group.Sum(x => x.CountOthers),
                    MobilizationMade = mobilizationData
                        .SelectMany(m => _projectmanagementRepository.GetMobilizationMade(m.ID.ToString()))
                        .GroupBy(m => m.MobilizationPlannedID)
                        .Select(madeGroup => new MobilizationMadeResult
                        {
                            AccommodationActual = madeGroup.Sum(x => x.CountAccommodation),
                            FoodActual = madeGroup.Sum(x => x.CountFood),
                            AirTransportActual = madeGroup.Sum(x => x.CountAirTransport),
                            GroundTransportActual = madeGroup.Sum(x => x.CountGroundTransport),
                            OthersActual = madeGroup.Sum(x => x.CountOthers)
                        })
                        .FirstOrDefault()
                })
                .ToList();
        }

        private List<StatusReportsResult> GetStatusReportsGrouped(int contractID)
        {
            var statusReports = _projectmanagementRepository.GetStatusReportsGraph(contractID);

            // Agrupamento dos relatórios
            var groupedResults = statusReports
                .GroupBy(report => new
                {
                    report.ContractID,
                    report.TypeAcquisitionID,
                    report.TypeAcquisitionDescription
                })
                .Select(group => new StatusReportsResult
                {
                    TypeID = group.Key.TypeAcquisitionID,
                    TypeDescription = group.Key.TypeAcquisitionDescription,
                    PlannedTotal = group.Sum(x => (double?)x.PredictedTotal ?? 0.0),
                    TotalPlanned = group.Sum(x => (double?)x.PredictedTotal ?? 0.0),
                    TotalMade = group.Sum(x => (double?)x.TotalCost ?? 0.0),
                    MadeDetails = group.Select(x => new MadeDetailsStatusResult
                    {
                        MadeId = x.TypeAcquisitionID,
                        MadeTotal = x.TotalCost
                    }).ToList()
                })
                .ToList();
            var totalPlannedSum = groupedResults.Sum(result => result.TotalPlanned ?? 0.0);
            var totalMadeSum = groupedResults.Sum(result => result.TotalMade ?? 0.0);
            groupedResults.Add(new StatusReportsResult
            {

                TypeDescription = "Resumo Geral",
                TotalPlanned = totalPlannedSum,
                TotalMade = totalMadeSum
            });

            return groupedResults;
        }



        public List<OrderManagementResult> GetOrderManagementGrouped(int contractID)
        {
            var orderManagementData = _projectmanagementRepository.OrderManagementInfo(contractID);

            return orderManagementData
                .GroupBy(info => info.ContractID)
                .Select(group => new OrderManagementResult
                {
                    ContractID = group.Key,
                    TotalDifferenceExpectedExecuted = (double)(group.Sum(x => x.TotalCostExpected ?? 0) - group.Sum(x => x.TotalCostExecuted ?? 0)),
                    TotalCostExpected = (double)group.Sum(x => x.TotalCostExpected ?? 0),
                    ManagementGrandTotal = (double)group.Sum(x => x.ManagementGrandTotal ?? 0),
                    TotalCostHoursExecuted = (double)group.Sum(x => x.TotalCostHoursExecuted ?? 0)
                })
                .ToList();
        }




        private List<OutsourcedServicesResult> GetOutsourcedServicesGrouped(int contractID)
        {
            var outsourcedServices = _projectmanagementRepository.GetOutsourcedServicesCombined(contractID);
            return outsourcedServices
                .GroupBy(service => service.ContractID)
                .Select(group => new OutsourcedServicesResult
                {
                    ContractID = group.Key,
                    TotalOutsourcedServicesPlannedSum = group.Sum(x => x.TotalOutsourcedServices_Planned),
                    TotalOutsourcedServicesMadeSum = group.Sum(x => x.TotalOutsourcedServices_Made),
                    Services = group.Select(x => new OutsourcedServiceDetailsResult
                    {
                        ID = x.ID,
                        ValueUnit = x.ValueUnit,
                        Amount = x.Amount,
                        TypeID = x.TypeID,
                        Description = x.Description,
                        Subcontracting = x.Subcontracting.HasValue ? x.Subcontracting.Value.ToString() : null,
                        TotalOutsourcedServices_Planned = x.TotalOutsourcedServices_Planned,
                        TotalOutsourcedServices_Made = x.TotalOutsourcedServices_Made,
                        Difference = x.Difference
                    }).ToList()
                })
                .ToList();
        }

        private List<HhGraphicDetailsResult> GetHhGraphicGrouped(int contractID)
        {
            var hhGraphicDetails = _projectmanagementRepository.GetHhGraphicDetail(contractID);

            return hhGraphicDetails
                .GroupBy(detail => detail.ContractID ?? 0)
                .Select(group => new HhGraphicDetailsResult
                {
                    ContractID = group.Key,
                    Details = group.Select(detail => new HhGraphicDetailItemResult
                    {
                        MilestoneTypeID = detail.MilestoneTypeID,
                        MilestonesValueID = detail.MilestonesValueID,
                        MilestonesID = detail.MilestonesID ?? 0,
                        DisplacementServiceName = detail.DisplacementServiceName,
                        DisplacementServicesID = detail.DisplacementServicesID ?? 0,
                        ValueHour = detail.ValueHour ?? 0,
                        HoursExpected = detail.HoursExpected ?? 0,
                        HoursPlanned = detail.HoursPlanned ?? 0,
                        HoursExecuted = detail.HoursExecuted ?? 0,
                        CostHoursExpected = detail.CostHoursExpected ?? 0,
                        CostHoursPlanned = detail.CostHoursPlanned ?? 0,
                        CostHoursExecuted = detail.CostHoursExecuted ?? 0
                    }).ToList()
                })
                .ToList();
        }


        public List<vMilestonesStatistics> vMilestonesStatistics(int contractID)
        {
          return _projectmanagementRepository.vMilestonesStatistics(contractID);
        }

        public Task<List<GetAllMilesStones>> GetMilestonesAsync(string milestoneId)
        {
            return _projectmanagementRepository.GetMilestonesAsync(milestoneId);    
        }

        public Task<List<GetMilestonesExpected>> GetMilestoneNoExpected(string milestoneId)
        {
            return _projectmanagementRepository.GetMilestoneNoExpected(milestoneId);
        }

        public Task<List<GetMilestonesExpected>> GetMilestonesExpected(string milestoneId)
        {
            return _projectmanagementRepository.GetMilestonesExpected(milestoneId);
        }

        public List<vContractExecutionHHCost> vContractExecutionHHCost(int contractID)
        {
           return _projectmanagementRepository.vContractExecutionHHCost(contractID);
        }

        public async Task<List<GetMilestonesExpected>> GetCombinedMilestones(string milestoneId)
        {

            List<GetMilestonesExpected> MilestonesExpectedsAndNoExpeecteds = new List<GetMilestonesExpected>();

            var expected = await GetMilestonesExpected(milestoneId);
            if (expected != null)
            {
                MilestonesExpectedsAndNoExpeecteds.AddRange(expected);
            }


            var noExpected = await GetMilestoneNoExpected(milestoneId);
            if (noExpected != null)
            {
                foreach (var noExp in noExpected)
                {

                    bool check = MilestonesExpectedsAndNoExpeecteds.Any(s => s.DepartmentName == noExp.DepartmentName && s.DisplacementServiceName == noExp.DisplacementServiceName && s.FunctionName == noExp.FunctionName);

                    if (!check)
                    {
                        MilestonesExpectedsAndNoExpeecteds.Add(noExp);
                    }



                }

            }           
            return MilestonesExpectedsAndNoExpeecteds;
        }

        public CardsHHHours CardsHHHours(int contractId)
        {
            return _projectmanagementRepository.CardsHHHours(contractId);
        }

        public Task<List<GetHhCostChart>> GetHhCostChartAsync(int contractId, DateTime startDate, DateTime endDate)
        {
           return _projectmanagementRepository.GetHhCostChartAsync(contractId, startDate, endDate); 
        }

        public Task<List<GetMilestoneFullReportByContract>> GetMilestoneFullReportByContract(int contractId)
        {
            return _projectmanagementRepository.GetMilestoneFullReportByContract(contractId);
        }

       
    }
}











