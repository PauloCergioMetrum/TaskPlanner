using Microsoft.Extensions.WebEncoders.Testing;
using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using System.Collections.Generic;
using TaskPlannerMetrum.Repository.ProjectManagement;

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
    }
}
