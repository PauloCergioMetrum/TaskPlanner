using Microsoft.Extensions.WebEncoders.Testing;
using System;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
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
                    OrderInformationDTO orderInformation = new OrderInformationDTO
                    {
                        Client = OrderInformationByID.ClientName,
                        SalesOrder = OrderInformationByID.InternalCode,
                        Validity = validityStartDate.Value.Date.ToString("dd/MM/yyyy") + " -- " + validityEndDate.Value.Date.ToString("dd/MM/yyyy"),
                        BusinessUnit = OrderInformationByID.BusinessUnit,
                        PredictedSavings = OrderInformationByID.PredictedSavings.ToString(),
                        PredictedMarkup = OrderInformationByID.PredictedMarkup.ToString(),
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
    }
}
