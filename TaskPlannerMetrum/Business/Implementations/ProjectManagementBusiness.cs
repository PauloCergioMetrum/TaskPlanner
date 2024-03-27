using Microsoft.Extensions.WebEncoders.Testing;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using System.Collections.Generic;
using TaskPlannerMetrum.Repository.ProjectManagement;
using TaskPlannerMetrum.Repository.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ProjectManagementBusiness : IProjectManagementBusiness
    {
        private readonly IProjectManagementRepository _projectmanagementRepository;

        public ProjectManagementBusiness(IProjectManagementRepository projectmanagementBusiness)
        {
            _projectmanagementRepository = projectmanagementBusiness;
        }

        public bool UpdateForecast(ProjectManagementDTO forecast)
        {
            try
            {
                var UpdateForecast = _projectmanagementRepository.GetForecastByID(forecast.id);
                if (UpdateForecast.id != null)
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


        public bool CreateMilesTones(MilesTonesDTO milesTonesDTO)
        {

            try
            {
                var CreateMilesTonesItem = _projectmanagementRepository.CreateMilestonesItem(new MilestonesItem
                {
                    ContractID = milesTonesDTO.ContractID,
                    Name = milesTonesDTO.March
                });


                _projectmanagementRepository.CreateMilestonesValue(new MilestonesValue
                {
                    MilestonesID = CreateMilesTonesItem,
                    Baseline = milesTonesDTO.Baseline,
                    Description = milesTonesDTO.Detail,
                    ExecutedDate = milesTonesDTO.DatePerformed,
                    RescheduledDate = milesTonesDTO.ReplannedDate,
                    ScheduledDate = milesTonesDTO.PlannedDate,

                });
                return true;




            }
            catch
            {
                return false;
            }


        }


        public bool UpdateMilesTones(MilesTonesDTO milesTonesDTO)
        {
            var MalesTonesValues = _projectmanagementRepository.GetMilestonesValueByID(milesTonesDTO.ID);
            if (milesTonesDTO.ReplannedDate.Date != MalesTonesValues.RescheduledDate.Value.Date || milesTonesDTO.DatePerformed.Date != MalesTonesValues.ExecutedDate.Value.Date)
            {
                MalesTonesValues.RescheduledDate = milesTonesDTO.ReplannedDate;
                MalesTonesValues.ExecutedDate = milesTonesDTO.DatePerformed;
                MalesTonesValues.Description = milesTonesDTO.Detail;
                MalesTonesValues.Baseline = MalesTonesValues.Baseline + 1;
                return _projectmanagementRepository.UpdateMilesTones(MalesTonesValues);
            }
            else
            {
                MalesTonesValues.RescheduledDate = milesTonesDTO.ReplannedDate;
                MalesTonesValues.ExecutedDate = milesTonesDTO.DatePerformed;
                MalesTonesValues.Description = milesTonesDTO.Detail;
                MalesTonesValues.Baseline = MalesTonesValues.Baseline;
                return _projectmanagementRepository.UpdateMilesTones(MalesTonesValues);
            }
        }

        public List<string> GetMilestonesNames(int contractID)
        {
            return _projectmanagementRepository.GetMilestonesNames(contractID);
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
            catch (Exception ex)
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
            catch (Exception ex)
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
                        StatusAcquistionID = acquisitionMade.StatusAcquistionID,
                        Amount = acquisitionMade.Amount,
                        AquisitionPlannedID = acquisitionMade.AquisitionPlannedID,
                        Value = acquisitionMade.Value,
                        DateAcquisition = acquisitionMade.DateAcquisition,
                        DateAcquisitionDelivery = acquisitionMade.DateAcquisitionDelivery,
                    });
                    return true;

                }


            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {

                return false;
            }
        }


        public List<vPMAcquisitionCombined> GetAcquisitions(int ContractID)
        {
            return _projectmanagementRepository.GetAcquisitions(ContractID);
        }

        public List<PMAcquisitionMade> GetAcquisitionsMade(string AquisitionPlannedID)
        {
            return _projectmanagementRepository.GetAcquisitionsMade(AquisitionPlannedID);
        }

        public bool CreateTypeOfCost(PmTypeCost pmTypeCost)
        {
            return _projectmanagementRepository.CreateTypeOfCost((PmTypeCost)pmTypeCost);
        }

        public bool DeleteTypeOfCost(int ID)
        {
            return _projectmanagementRepository.DeleteTypeOfCost(ID);
        }

        public List<PmTypeCost> GetTypeOfCost()
        {
            return (_projectmanagementRepository.GetTypeOfCost());
        }

        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned)
        {
            return (_projectmanagementRepository.CreateOrUpdatePredictedCost((pmCostPlanned)));
        }
        public bool DeletePredictedCost(string ID)
        {
            return _projectmanagementRepository.DeletePredictedCost(ID);
        }

        public List<PmCostPlanned> GetPmCostPlanned()
        {
            return _projectmanagementRepository.GetPmCostPlanned();
        }

        public bool CreateOrUpdateCostMade(PmCostMade pmCostMade)
        {
            return _projectmanagementRepository.CreateOrUpdateCostMade(pmCostMade);
        }


        public bool DeleteCostMade(string ID)
        {
            return _projectmanagementRepository.DeleteCostMade(ID);
        }


        public List<PmCostMade> GetPmCostMade()
        {
            return _projectmanagementRepository.GetPmCostMade();
        }
    }
}

