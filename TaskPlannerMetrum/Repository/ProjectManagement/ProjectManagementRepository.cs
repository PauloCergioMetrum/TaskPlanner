
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using Memt.Logger;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public class ProjectManagementRepository : IProjectManagementRepository
    {
        private readonly MSSQLContext _context;
        public IConfiguration Configuration { get; }
        public ProjectManagementRepository(MSSQLContext context, IConfiguration configuration)
        {
            Configuration = configuration;
            _context = context;
        }
        public bool CreateAcquisitionsItem(PMAcquisitionPlanned acquisitions)
        {
            var existAcquisitions = _context.PM_Acquisition_Planned.FirstOrDefault(n => n.TypeAcquisitionID == acquisitions.TypeAcquisitionID && n.ID == acquisitions.ID);

            if (existAcquisitions == null)
            {
                _context.PM_Acquisition_Planned.Add(acquisitions);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool ExistAcquisition(string ID)
        {
            return _context.PM_Acquisition_Planned.Any(n => n.ID == ID);
        }

        public bool DeleteAcquisition(string ID)
        {
            try
            {
                var AcquisitionsItem = _context.PM_Acquisition_Planned.Where(u => u.ID == ID).FirstOrDefault();
                if (AcquisitionsItem != null)
                {
                    _context.PM_Acquisition_Planned.Remove(AcquisitionsItem);
                    _context.SaveChanges();
                    return true;
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

        public bool UpdateAcquisition(AcquisitionsDTO acquisitions)
        {
            try
            {
                var acquisitionEntity = _context.PM_Acquisition_Planned.Find(acquisitions.ID);
                acquisitionEntity.TypeAcquisitionID = acquisitions.TypeAcquisitionID;
                acquisitionEntity.AmountPlanned = acquisitions.AmountPlanned;
                acquisitionEntity.AmountValue = acquisitions.AmountValue;
                acquisitionEntity.Description = acquisitions.Description;
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateAcquisitionMadeItem(PMAcquisitionMade acquisitionMade)
        {

            var existAcquisitionsMade = _context.PM_Acquisition_Made.FirstOrDefault(n => n.Value == acquisitionMade.Value && n.ID == acquisitionMade.ID);
            if (existAcquisitionsMade == null)
            {
                _context.PM_Acquisition_Made.Add(acquisitionMade);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool ExistAcquisitionMade(string ID)
        {
            return _context.PM_Acquisition_Made.Any(n => n.ID == ID);
        }

        public bool DeleteAcquisitionMade(string ID, string AquisitionPlannedID)
        {
            try
            {
                if (!string.IsNullOrEmpty(AquisitionPlannedID))
                {
                    var AcquisitionsMadeItems = _context.PM_Acquisition_Made.Where(u => u.AquisitionPlannedID == AquisitionPlannedID).ToList();
                    if (AcquisitionsMadeItems.Any())
                    {
                        _context.PM_Acquisition_Made.RemoveRange(AcquisitionsMadeItems);
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (!string.IsNullOrEmpty(ID))
                {
                    var AcquisitionsMadeItem = _context.PM_Acquisition_Made.FirstOrDefault(u => u.ID == ID);
                    if (AcquisitionsMadeItem != null)
                    {
                        _context.PM_Acquisition_Made.Remove(AcquisitionsMadeItem);
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
        public List<GetMilestones> GetMilestonesNames(int contractID)
        {

            return _context.GetMilestones(contractID).ToList();
        }
        public bool UpdateAcquisitionMadeItem(AcquisitionMadeDTO acquisitionMade)
        {
            try
            {
                var acquisitionMadeEntity = _context.PM_Acquisition_Made.Find(acquisitionMade.ID);
                acquisitionMadeEntity.ID = acquisitionMade.ID;
                //acquisitionMadeEntity.StatusAcquistionID = acquisitionMade.StatusAcquistionID;
                acquisitionMadeEntity.Amount = acquisitionMade.Amount;
                acquisitionMadeEntity.Value = acquisitionMade.Value;
                acquisitionMadeEntity.DateAcquisition = acquisitionMade.DateAcquisition;
                acquisitionMadeEntity.DateAcquisitionDelivery = acquisitionMade.DateAcquisitionDelivery;
                acquisitionMadeEntity.Description = acquisitionMade.Description;
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }




        public int CreateMilestonesItem(MilestonesItem milestones)
        {
            if (milestones == null)
            {
                throw new ArgumentNullException(nameof(milestones));
            }

            try
            {
                var existingMilestones = _context.MilestonesItem
                  .FirstOrDefault(n => n.Name == milestones.Name && n.ContractID == milestones.ContractID);

                if (existingMilestones == null)
                {
                    _context.MilestonesItem.Add(milestones);
                    _context.SaveChanges();

                    return milestones.ID;
                }
                else
                {
                    return existingMilestones.ID;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao criar ou atualizar o MilestonesItem.", ex);
            }
        }


        public bool CreateMilestonesValue(MilestonesValue milestones)
        {
            try
            {
                _context.MilestonesValue.Add(milestones);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public bool UpdateMilesTones(MilestonesValue milestonesValue)
        {
            if (milestonesValue != null)
            {
                _context.MilestonesValue.Update(milestonesValue);
                _context.SaveChanges();
                return true;
            }
            return false;
        }


        public bool DeleteMilestones(string ID, int MilestonesID)
        {

            var milestone = _context.MilestonesValue.FirstOrDefault(m => m.ID == ID);


            if (milestone != null)
            {

                int milestoneId = milestone.MilestonesID;


                _context.MilestonesValue.Remove(milestone);
                _context.SaveChanges();


                int remainingMilestonesCount = _context.MilestonesValue.Count(m => m.MilestonesID == milestoneId);


                int milestonesItemCount = _context.MilestonesItem.Count(m => m.ID == MilestonesID);

                if (remainingMilestonesCount > 0 || milestonesItemCount > 1)
                {
                    return true;
                }

                return MilesTonesDelete(MilestonesID);
            }

            return false;
        }

        public bool MilesTonesDelete(int ID)
        {
            var relatedMilestones = _context.MilestonesValue.Where(m => m.MilestonesID == ID).ToList();

            if (relatedMilestones.Any())
            {
                _context.MilestonesValue.RemoveRange(relatedMilestones);
            }

            var milestoneItem = _context.MilestonesItem.FirstOrDefault(m => m.ID == ID);
            if (milestoneItem != null)
            {
                _context.MilestonesItem.Remove(milestoneItem);
                _context.SaveChanges();
                return true;
            }

            return false;
        }








        public vContractList GetForecastByIDView(int id)
        {
            return _context.vContractList.Where(i => i.ContractID == id).FirstOrDefault();
        }

        public bool UpdateForecast(Model.Contracts forecast)
        {
            try
            {


                _context.Update(forecast);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public MilestonesValue GetMilestonesValueByID(int milestonesID)
        {
            return _context.MilestonesValue.Where(i => i.MilestonesID == milestonesID).FirstOrDefault();
        }



        public List<Positions> GetPositionsByGrup()
        {
            try
            {

                List<Positions> positions = _context.Positions
                  .OrderBy(i => i.PositionName)
                  .ToList();

                return positions;
            }
            catch (Exception ex)
            {

                Logger.Log(ex.Message, ELoggerType.Debug);
                return new List<Positions>();
            }
        }






        public bool DeleteTypeOfCost(int ID)
        {
            var typeOfCostToRemove = _context.Pm_Type_Cost.SingleOrDefault(t => t.ID == ID);

            if (typeOfCostToRemove != null)
            {
                _context.Pm_Type_Cost.Remove(typeOfCostToRemove);
                _context.SaveChanges();
                return true;
            }
            return false;
        }



        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned)
        {
            var existingCost = _context.Pm_Cost_Planned.FirstOrDefault(c => c.Id == pmCostPlanned.Id);

            if (existingCost != null)
            {


                existingCost.Amount = pmCostPlanned.Amount;
                existingCost.ValueUnit = pmCostPlanned.ValueUnit;
                existingCost.Description = pmCostPlanned.Description;
                existingCost.ContractID = pmCostPlanned.ContractID;
                //existingCost.TotalPlanned = pmCostPlanned.TotalPlanned;

                _context.SaveChanges();
                return true;
            }
            else
            {

                _context.Pm_Cost_Planned.Add(pmCostPlanned);
                _context.SaveChanges();
                return true;
            }
        }
        public bool DeletePredictedCost(string ID)
        {
            var RemovePredictedCost = _context.Pm_Cost_Planned.SingleOrDefault(t => t.Id == ID);
            if (RemovePredictedCost != null)
            {
                _context.Pm_Cost_Planned.Remove(RemovePredictedCost);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<vPm_Cost_Planned> GetPmCostPlanned(int ContractID)
        {
            var listPmCostPlanned = _context.vPm_Cost_Planned
              .Where(i => i.ContractID == ContractID)
              .Select(pm => new vPm_Cost_Planned
              {
                  Id = pm.Id,
                  TypeID = pm.TypeID,
                  Amount = pm.Amount,
                  ValueUnit = pm.ValueUnit,
                  Description = pm.Description,
                  ContractID = pm.ContractID,
                  Total = pm.Total ?? 0,
                  TotalPlanned = pm.TotalPlanned,
                  Difference = pm.Difference ?? 0
              })
              .ToList();

            return listPmCostPlanned;
        }



        public bool CreateOrUpdateCostMade(PmCostMade pmCostMade)
        {
            try
            {
                var existingCostMade = _context.PmCostMade.FirstOrDefault(c => c.Id == pmCostMade.Id);
                if (existingCostMade != null)
                {

                    existingCostMade.Amount = pmCostMade.Amount;
                    existingCostMade.ValueUnit = pmCostMade.ValueUnit;
                    existingCostMade.Description = pmCostMade.Description;
                    existingCostMade.Pm_Cost_Planned_Id = pmCostMade.Pm_Cost_Planned_Id;
                    _context.SaveChanges();
                    return true;
                }
                else
                {

                    _context.PmCostMade.Add(pmCostMade);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Erro ao salvar as alterações no banco de dados: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Exceção interna: " + ex.InnerException.Message);
                }
                return false;
            }
        }

        public bool DeleteCostMade(string ID)
        {
            var RemoveCost = _context.PmCostMade.SingleOrDefault(t => t.Id == ID);
            if (RemoveCost != null)
            {
                _context.PmCostMade.Remove(RemoveCost);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<vPmCostMade> GetPmCostMade(string Pm_Cost_PlannedID)
        {
            var ListGetCostMade = _context.vPmCostMade.Where(i => i.Pm_Cost_Planned_Id == Pm_Cost_PlannedID).ToList();
            return ListGetCostMade;
        }
        public List<vContractProject> GetAllContractProjectByTechLeader(int? TechLeaderID, string InspectorName)
        {
            throw new NotImplementedException();
        }


        public bool IsExistMilesStone(MilesTonesDTO milesTonesDTO)
        {

            return _context.MilestonesItem.Any(i => i.ContractID == milesTonesDTO.ContractID && i.Name == milesTonesDTO.Description);



        }

        public int existMilesStonesValue(string ID)

        {

            var teste = _context.MilestonesValue.Where(a => a.ID == ID).FirstOrDefault();


            if (teste != null)
            {
                return teste.MilestonesID;
            }
            else
            {
                return 0;
            }


        }

        public bool UpdateMilesValue(MilesTonesDTO milesTonesDTO, int ID)
        {
            var teste = _context.MilestonesItem.Where(a => a.ID == ID).FirstOrDefault();
            teste.Name = milesTonesDTO.Name;
            _context.Update(teste);
            _context.SaveChanges();
            var UpdateMilesStonesValue = _context.MilestonesValue.Where(a => a.MilestonesID == ID).FirstOrDefault();
            UpdateMilesStonesValue.ScheduledDate = milesTonesDTO.ScheduledDate;
            UpdateMilesStonesValue.Description = milesTonesDTO.Description;
            UpdateMilesStonesValue.RescheduledDate = milesTonesDTO.RescheduledDate;
            UpdateMilesStonesValue.StartDateMilestones = milesTonesDTO.StartDateMilestones;
            UpdateMilesStonesValue.Description = milesTonesDTO.Description;
            UpdateMilesStonesValue.TechLeadID = milesTonesDTO.TechLeadID;
            UpdateMilesStonesValue.BusinessUnitID = milesTonesDTO.BusinessUnitID;
            UpdateMilesStonesValue.Value = milesTonesDTO.Value;
            UpdateMilesStonesValue.ExecutedDate = milesTonesDTO.ExecutedDate;
            _context.Update(UpdateMilesStonesValue);
            _context.SaveChanges();
            return true;



        }


        // MOBILIZAÇÃO  PLANNED
        public List<vPM_Mobilization_Combined> GetMobilization(int contractID)
        {
            return _context.vPM_Mobilization_Combined.Where(a => a.ContractID == contractID).ToList();
        }

        public List<vPM_Mobilization_Made> GetMobilizationMade(string mobilizationPlannedID)
        {
            return _context.vPM_Mobilization_Made.Where(a => a.MobilizationPlannedID == mobilizationPlannedID).ToList();
        }

        public bool UpdateMobilization(string ID)
        {
            var mobilizationToUpdate = _context.PM_Mobilization_Planned.FirstOrDefault(a => a.ID == ID);
            if (mobilizationToUpdate != null)
            {
                _context.PM_Mobilization_Planned.Update(mobilizationToUpdate);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CreateMobilizationPlanned(PM_Mobilization_Planned mobilizationPlanned)
        {
            var existingMobilization = _context.PM_Mobilization_Planned.FirstOrDefault(a => a.ID == mobilizationPlanned.ID);
            if (existingMobilization != null)
            {

                existingMobilization.CountMobilization = mobilizationPlanned.CountMobilization;
                existingMobilization.CountAccommodation = mobilizationPlanned.CountAccommodation;
                existingMobilization.CountFood = mobilizationPlanned.CountFood;
                existingMobilization.CountAirTransport = mobilizationPlanned.CountAirTransport;
                existingMobilization.CountGroundTransport = mobilizationPlanned.CountGroundTransport;
                existingMobilization.CountOthers = mobilizationPlanned.CountOthers;
                _context.PM_Mobilization_Planned.Update(existingMobilization);
            }
            else
            {

                _context.PM_Mobilization_Planned.Add(mobilizationPlanned);
            }

            _context.SaveChanges();
            return true;
        }




        public bool CreateMobilizationMade(PM_Mobilization_Made mobilizationMade)
        {

            var exixtMolizationMade = _context.PM_Mobilization_Made.FirstOrDefault(a => a.ID == mobilizationMade.ID);

            if (exixtMolizationMade != null)
            {
                exixtMolizationMade.CountMobilization = mobilizationMade.CountMobilization;
                exixtMolizationMade.CountAccommodation = mobilizationMade.CountAccommodation;

                exixtMolizationMade.CountFood = mobilizationMade.CountFood;
                exixtMolizationMade.CountAirTransport = mobilizationMade.CountAirTransport;
                exixtMolizationMade.CountGroundTransport = mobilizationMade.CountGroundTransport;
                exixtMolizationMade.CountOthers = mobilizationMade.CountOthers;
                exixtMolizationMade.DateStart = mobilizationMade.DateStart;
                exixtMolizationMade.DateEnd = mobilizationMade.DateEnd;
                exixtMolizationMade.Description = mobilizationMade.Description;
                exixtMolizationMade.MobilizationPlannedID = mobilizationMade.MobilizationPlannedID;
                //exixtMolizationMade.TotalValue = mobilizationMade.TotalValue;


            }
            else
            {
                _context.PM_Mobilization_Made.Add(mobilizationMade);
            }
            _context.SaveChanges();
            return true;
        }

        public bool UpdateMobilizationMaded(string ID)
        {

            var mobilizationToUpdatMade = _context.PM_Mobilization_Made.FirstOrDefault(a => a.ID == ID);
            if (mobilizationToUpdatMade != null)
            {
                _context.PM_Mobilization_Made.Update(mobilizationToUpdatMade);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteMobilization(string ID)
        {
            try
            {
                var MobilizationItem = _context.PM_Mobilization_Planned.Where(u => u.ID == ID).FirstOrDefault();
                if (MobilizationItem != null)
                {
                    _context.PM_Mobilization_Planned.Remove(MobilizationItem);
                    _context.SaveChanges();
                    return true;
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

        public bool DeleteMobilizationMade(string ID)
        {
            try
            {
                var MobilizationMadeItem = _context.PM_Mobilization_Made.Where(u => u.ID == ID).FirstOrDefault();
                if (MobilizationMadeItem != null)
                {
                    _context.PM_Mobilization_Made.Remove(MobilizationMadeItem);
                    _context.SaveChanges();
                    return true;
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
        //  SERVIÇO TERCEIRIZADO

        public List<Pm_Type_OutsourcedServicesDTO> GetAllOutsourcedServiceNames()
        {
            try
            {
                var result = _context.PM_Type_OutsourcedServices
                  .Select(a => new Pm_Type_OutsourcedServicesDTO { Name = a.Name, ID = a.ID })
                  .ToList();

                return result;
            }
            catch (Exception)
            {

                return new List<Pm_Type_OutsourcedServicesDTO>();
            }
        }

        public List<vPM_OutsourcedServices_Combined> GetOutsourcedServicesCombined(int ContractID)
        {
            return _context.vPM_OutsourcedServices_Combined.Where(a => a.ContractID == ContractID).ToList();
        }


        public List<vPM_OutsourcedServices_Made_Combined> GetOutsourcedServicesCombinedMade(string ID_OutsourcedServices_Planned)
        {
            return _context.vPM_OutsourcedServices_Made_Combined.Where(a => a.ID_OutsourcedServices_Planned == ID_OutsourcedServices_Planned).ToList();
        }


        public bool CreateoutsourcedServicePlanned(PM_OutsourcedServices_Planned createoutsourcedServicePlanned)
        {
            var existingService = _context.PM_OutsourcedServices_Planned.FirstOrDefault(a => a.ID == createoutsourcedServicePlanned.ID);
            if (existingService != null)
            {
                _context.Entry(existingService).CurrentValues.SetValues(createoutsourcedServicePlanned);
                _context.SaveChanges();
                return true;
            }
            else
            {
                _context.PM_OutsourcedServices_Planned.Add(createoutsourcedServicePlanned);
                _context.SaveChanges();
                return true;
            }
        }

        public bool UpdateoutsourcedServicePlanned(PM_OutsourcedServices_Planned updatedService)
        {
            var existingService = _context.PM_OutsourcedServices_Planned.FirstOrDefault(a => a.ID == updatedService.ID);
            if (existingService != null)
            {


                _context.Entry(existingService).CurrentValues.SetValues(updatedService);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CreateoutsourcedServiceMade(PM_OutsourcedServices_Made createoutsourcedServiceMade)
        {
            var existingService = _context.PM_OutsourcedServices_Made.FirstOrDefault(a => a.ID == createoutsourcedServiceMade.ID);
            if (existingService != null)
            {

                _context.Entry(existingService).CurrentValues.SetValues(createoutsourcedServiceMade);
                _context.SaveChanges();
                return true;
            }
            else
            {
                _context.PM_OutsourcedServices_Made.Add(createoutsourcedServiceMade);
                _context.SaveChanges();
                return true;
            }
        }


        public bool UpdateoutsourcedServiceMade(PM_OutsourcedServices_Made updatedServiceMade)
        {
            var existingService = _context.PM_OutsourcedServices_Made.FirstOrDefault(a => a.ID == updatedServiceMade.ID);
            if (existingService != null)
            {

                _context.Entry(existingService).CurrentValues.SetValues(updatedServiceMade);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteOutsourcedServicesPlanned(string ID)
        {
            try
            {
                var OutsourcedServicesPlannedDelete = _context.PM_OutsourcedServices_Planned.Where(a => a.ID == ID).FirstOrDefault();

                if (OutsourcedServicesPlannedDelete != null)
                {

                    var relatedRecords = _context.PM_OutsourcedServices_Made.Where(a => a.ID_OutsourcedServices_Planned == ID).ToList();
                    _context.PM_OutsourcedServices_Made.RemoveRange(relatedRecords);


                    _context.PM_OutsourcedServices_Planned.Remove(OutsourcedServicesPlannedDelete);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteOutsourcedServicesMade(string ID)
        {
            try
            {
                var OutsourcedServicesPlannedDelete = _context.PM_OutsourcedServices_Made.Where(a => a.ID == ID).FirstOrDefault();

                if (OutsourcedServicesPlannedDelete != null)
                {
                    _context.PM_OutsourcedServices_Made.Remove(OutsourcedServicesPlannedDelete);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        //HH
        public List<PM_Type_Hours> GetpmTypeHH()
        {
            var resultHH = _context.PM_Type_Hours.Select(a => new PM_Type_Hours { Type = a.Type, ID = a.ID }).ToList();
            return resultHH;

        }

        public List<PM_Function_HH> GetPMFunctionHHs()
        {
            var resiultFunctionHH = _context.PM_Function_HH.Select(a => new PM_Function_HH
            {

                ID = a.ID,
                FunctionHH = a.FunctionHH,
            }).ToList();
            return resiultFunctionHH;
        }








        public bool CreateHH(PM_Man_Hours createHH)
        {
            var existingHH = _context.PM_Man_Hours.FirstOrDefault(a => a.ID == createHH.ID);

            if (existingHH != null)
            {

                existingHH.Departmemt = createHH.Departmemt;
                existingHH.FunctionHH = createHH.FunctionHH;
                existingHH.Type = createHH.Type;
                existingHH.QuantityHH = createHH.QuantityHH;

                _context.SaveChanges();
            }
            else
            {

                _context.PM_Man_Hours.Add(createHH);
                _context.SaveChanges();
            }

            return true;
        }

        public bool UpdateHH(PM_Man_Hours updateHH)
        {
            var existingHH = _context.PM_Man_Hours.FirstOrDefault(a => a.ID == updateHH.ID);
            if (existingHH != null)
            {

                existingHH.Departmemt = updateHH.Departmemt;
                existingHH.FunctionHH = updateHH.FunctionHH;
                existingHH.Type = updateHH.Type;
                existingHH.QuantityHH = updateHH.QuantityHH;


                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteHH(string ID)
        {
            var RemoveHH = _context.PM_Man_Hours.Where(a => a.ID == ID).FirstOrDefault();
            if (RemoveHH != null)
            {
                _context.PM_Man_Hours.Remove(RemoveHH);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<vPM_Mam_Hours> GetpmHours(int ContractID)
        {
            return _context.vPM_Mam_Hours.Where(a => a.ContractID == ContractID).ToList();

        }


        public bool CreatScopeTraking(PM_Scope_Traking pMScopeTraking)
        {
            var existingScopeTracking = _context.PM_Scope_Traking.FirstOrDefault(a => a.ID == pMScopeTraking.ID);



            if (existingScopeTracking != null)
            {
                existingScopeTracking.ID = pMScopeTraking.ID;
                existingScopeTracking.ContractID = pMScopeTraking.ContractID;
                existingScopeTracking.Item = pMScopeTraking.Item;
                existingScopeTracking.DateField = pMScopeTraking.DateField;



                _context.SaveChanges();

            }
            else
            {
                _context.PM_Scope_Traking.Add(pMScopeTraking);
                _context.SaveChanges();
            }
            return true;
        }

        public bool UpdatepMScopeTraking(PM_Scope_Traking pMScopeTraking)
        {
            var existingScopeTracking = _context.PM_Scope_Traking.FirstOrDefault(r => r.ID == pMScopeTraking.ID);

            if (existingScopeTracking != null)
            {

                existingScopeTracking.ID = pMScopeTraking.ID;
                existingScopeTracking.ContractID = pMScopeTraking.ContractID;
                existingScopeTracking.Item = pMScopeTraking.Item;
                existingScopeTracking.DateField = pMScopeTraking.DateField;
                _context.SaveChanges();

            }
            else
            {
                _context.PM_Scope_Traking.Add(pMScopeTraking);
                _context.SaveChanges();
            }
            return true;
        }

        public bool DeletepMScopeTraking(string ID)
        {
            var RemovepMScopeTraking = _context.PM_Scope_Traking.Where(e => e.ID == ID).FirstOrDefault();
            if (RemovepMScopeTraking != null)
            {
                _context.PM_Scope_Traking.Remove(RemovepMScopeTraking);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<PM_Scope_Traking> GetScopeTrajing(int ContractID)
        {
            return _context.PM_Scope_Traking.Where(a => a.ContractID == ContractID).ToList();
        }

        public bool CreateScopeChange(PM_Scope_Change pM_Scope_Change)
        {
            var ExistiScopeChange = _context.PM_Scope_Change.FirstOrDefault(a => a.ID == pM_Scope_Change.ID);
            if (ExistiScopeChange != null)
            {

                ExistiScopeChange.ID = pM_Scope_Change.ID;
                ExistiScopeChange.ContractID = pM_Scope_Change.ContractID;
                ExistiScopeChange.Description = pM_Scope_Change.Description;
                ExistiScopeChange.Reason = pM_Scope_Change.Reason;
                ExistiScopeChange.Type = pM_Scope_Change.Type;
                ExistiScopeChange.Value = pM_Scope_Change.Value;
                ExistiScopeChange.Date = pM_Scope_Change.Date;
                _context.SaveChanges();

            }
            else
            {
                _context.PM_Scope_Change.Add(pM_Scope_Change);
                _context.SaveChanges();
            }
            return true;
        }

        public bool UpdateScopeChange(PM_Scope_Change pM_Scope_Change)
        {
            var ExistiScopeChangeUpdate = _context.PM_Scope_Change.FirstOrDefault(a => a.ID == pM_Scope_Change.ID);
            if (ExistiScopeChangeUpdate != null)
            {

                ExistiScopeChangeUpdate.ID = pM_Scope_Change.ID;
                ExistiScopeChangeUpdate.ContractID = pM_Scope_Change.ContractID;
                ExistiScopeChangeUpdate.Description = pM_Scope_Change.Description;
                ExistiScopeChangeUpdate.Reason = pM_Scope_Change.Reason;
                ExistiScopeChangeUpdate.Type = pM_Scope_Change.Type;
                ExistiScopeChangeUpdate.Value = pM_Scope_Change.Value;
                ExistiScopeChangeUpdate.Date = pM_Scope_Change.Date;
                _context.SaveChanges();

            }
            else
            {
                _context.PM_Scope_Change.Add(pM_Scope_Change);
                _context.SaveChanges();
            }
            return true;
        }


        public bool DeleteScopeChange(string ID)
        {
            var RemoveScopeChange = _context.PM_Scope_Change.FirstOrDefault(r => r.ID == ID);
            if (RemoveScopeChange != null)
            {
                _context.PM_Scope_Change.Remove(RemoveScopeChange);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<PM_Scope_Change> GetScopeChanges(int ContractID)
        {
            return _context.PM_Scope_Change.Where(g => g.ContractID == ContractID).ToList();

        }


        public List<GetFinanceMilestones> GetAllMilestonesItem(int contractID)
        {
            return _context.GetFinanceMilestones(contractID).ToList();
        }








        public List<Functions> GetAllFunctions()
        {
            return _context.Functions.ToList();
        }


        public bool UpdateFunctionID(string ID, PM_Functions_MilestoneType dto)
        {

            var existingFunction = _context.PM_Functions_MilestoneType.FirstOrDefault(f => f.ID == ID);

            if (existingFunction != null)
            {

                existingFunction.FunctionID = dto.FunctionID;
                existingFunction.MilesstoneTypeID = dto.MilesstoneTypeID;
                _context.PM_Functions_MilestoneType.Update(existingFunction);
            }
            else
            {

                var newFunction = new PM_Functions_MilestoneType
                {
                    FunctionID = dto.FunctionID,
                    MilesstoneTypeID = dto.MilesstoneTypeID,
                    ID = dto.ID,
                };

                _context.PM_Functions_MilestoneType.Add(newFunction);
            }


            _context.SaveChanges();

            return true;
        }

        public bool CreateFunctions_MilestoneType(PM_Functions_MilestoneType dto)
        {
            var existingFunction = _context.PM_Functions_MilestoneType
              .FirstOrDefault(f => f.FunctionID == dto.FunctionID && f.MilesstoneTypeID == dto.MilesstoneTypeID);

            if (existingFunction == null)
            {
                var newFunction = new PM_Functions_MilestoneType
                {
                    FunctionID = dto.FunctionID,
                    MilesstoneTypeID = dto.MilesstoneTypeID,
                    ID = dto.ID,
                };

                _context.PM_Functions_MilestoneType.Add(newFunction);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public List<vPM_Functions_MilestoneType> GetAllFunctionsMilesstoneType(string MilesstoneTypeID)
        {
            return _context.vPM_Functions_MilestoneType
              .Where(a => a.MilesstoneTypeID == MilesstoneTypeID)
              .ToList();
        }

        public bool DeleteFunctionID(string ID)
        {
            var removeFunctions = _context.PM_Functions_MilestoneType.SingleOrDefault(a => a.ID == ID);
            if (removeFunctions != null)
            {
                _context.PM_Functions_MilestoneType.Remove(removeFunctions);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DisplacementServices> GetAllDisplacementServices()
        {
            return _context.DisplacementServices.ToList();

        }




        public bool CreatePM_MilestonesType(PM_MilestonesType dto)
        {
            var existingMilesType = _context.PM_MilestonesType.FirstOrDefault(f => f.ID == dto.ID);

            if (existingMilesType != null)
            {

                existingMilesType.DepartmentID = dto.DepartmentID;
                existingMilesType.Hours = dto.Hours;
                existingMilesType.MilestonesValueID = dto.MilestonesValueID;
                existingMilesType.DisplacementServicesID = dto.DisplacementServicesID;
                existingMilesType.FunctionID = dto.FunctionID;
                existingMilesType.ValueHour = dto.ValueHour;



            }
            else
            {

                var newMilesType = new PM_MilestonesType
                {
                    ID = dto.ID,
                    DepartmentID = dto.DepartmentID,
                    Hours = dto.Hours,
                    DisplacementServicesID = dto.DisplacementServicesID,
                    MilestonesValueID = dto.MilestonesValueID,
                    FunctionID = dto.FunctionID,
                    ValueHour = dto.ValueHour,

                };
                _context.PM_MilestonesType.Add(newMilesType);
            }

            _context.SaveChanges();

            return true;
        }


        public List<vPM_MilestoneType> GetAllPM_MilestonesType(string MilestonesValueID)
        {
            return _context.vPM_MilestoneType.Where(f => f.MilestonesValueID == MilestonesValueID).ToList();
        }

        public bool DeletePM_MilestonesType(string ID)
        {
            var DeleteMilestonesType = _context.PM_MilestonesType.Where(a => a.ID == ID).FirstOrDefault();

            if (DeleteMilestonesType != null)
            {
                _context.PM_MilestonesType.Remove(DeleteMilestonesType);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }
        public List<GetMilestoneType_HH_Details> GetHHByID(string MilestonesValueID)
        {
            return _context.GetMilestoneType_HH_Details(MilestonesValueID).ToList();
        }

        public List<vMileStonesValue> GetAllMileStonesValue(int ContractID)
        {
            return _context.vMileStonesValue
              .Where(a => a.ContractID == ContractID && a.MilestonesTypeID != 2)
              .ToList();
        }

        public List<vMilestoneStatusCalculation> GetMilestoneStatusCalculation(int ContractID)
        {
            return _context.vMilestoneStatusCalculation
              .AsNoTracking()
              .Where(a => a.ContractId == ContractID)
              .GroupBy(m => m.MilestoneId)
              .Select(g => g.First())
              .ToList();


        }


        public string SetMilestoneFinalizedStatus(FinalizedStatusDto dto)
        {
            var record = _context.MilestoneStatusManual
              .FirstOrDefault(m => m.MilestoneID == dto.MilestoneID && m.ContractID == dto.ContractID);

            if (dto.IsFinalized)
            {

                //var hasOpenTasks = _context.ActivityPlan.Any(ap =>
                //    ap.ContractID == dto.ContractID &&
                //    ap.MilestonesID == dto.MilestoneID &&
                //    ap.Status != "1" && ap.Status != "9"
                //);

                //if (hasOpenTasks)
                //    return "Não é possível finalizar: existem tarefas não concluídas.";

                if (record == null)
                {

                    record = new MilestoneStatusManual
                    {
                        MilestoneID = dto.MilestoneID,
                        ContractID = dto.ContractID,
                        IsFinalized = true,
                        FinalizedBy = dto.FinalizedBy,
                        FinalizedDate = DateTime.Now
                    };
                    _context.MilestoneStatusManual.Add(record);
                }
                else
                {

                    record.IsFinalized = true;
                    record.FinalizedBy = dto.FinalizedBy;
                    record.FinalizedDate = DateTime.Now;
                }

                _context.SaveChanges();
                return "Marco finalizado com sucesso.";
            }
            else
            {

                if (record != null)
                {
                    record.IsFinalized = false;
                    record.FinalizedBy = null;
                    record.FinalizedDate = null;
                }

                _context.SaveChanges();
                return "Marco reaberto com sucesso.";
            }
        }




        public bool CreateTapScope(PM_TAP_Scope pM_TAP_Scope)
        {
            var existingScope = _context.PM_TAP_Scope.FirstOrDefault(s => s.ID == pM_TAP_Scope.ID);

            if (existingScope != null)
            {
                _context.Entry(existingScope).CurrentValues.SetValues(pM_TAP_Scope);
            }
            else
            {
                _context.PM_TAP_Scope.Add(pM_TAP_Scope);
            }

            _context.SaveChanges();
            return true;
        }


        public bool UpdateProjectScope(int ID, PM_TAP_Scope updatedScope)
        {
            var existingScope = _context.PM_TAP_Scope.FirstOrDefault(s => s.ID == updatedScope.ID && s.ID != ID);

            if (existingScope != null)
            {

                return false;
            }

            var scope = _context.PM_TAP_Scope.FirstOrDefault(s => s.ID == ID);
            if (scope != null)
            {
                _context.Entry(scope).CurrentValues.SetValues(updatedScope);
                _context.SaveChanges();
                return true;
            }

            return false;
        }



        public Model.Contracts GetForecastByID(int id)
        {

            return _context.Contracts.Where(i => i.id == id).FirstOrDefault();
        }





        public List<PM_TAP_RiskLevel> GetAllProjectCharter()
        {
            return _context.PM_TAP_RiskLevel.ToList();
        }


        public List<UserVO> GetAllUsersGercon()
        {
            var filteredUsers = _context.Users.Where(a => a.DepartmentId == 14).Select(a => new UserVO
            {
                Id = a.Id,
                UserName = a.UserName,


            }).ToList();

            return filteredUsers;
        }


        // TAP
        public ProjectInfoGeneralDTO GetProjectInfoByContractId(int contractId)
        {

            var contractInfo = _context.Contracts
              .Where(c => c.id == contractId)
              .FirstOrDefault();
            var tapInfo = _context.PM_TAP_General_Info
              .Where(t => t.ContractID == contractId)
              .FirstOrDefault();
            var contactClients = _context.PM_Information_General
              .Where(c => c.ContractID == contractId)
              .ToList();
            var projectInfo = new ProjectInfoGeneralDTO
            {
                ContractID = contractId,
                Scopes = _context.PM_TAP_Scope.FirstOrDefault(s => s.ContractID == contractId),
                Resources = _context.PM_TAP_Resources.FirstOrDefault(r => r.ContractID == contractId),
                ProjectInfo = new ProjectManagementGeneralInfo
                {
                    Id = contractInfo?.id ?? 0,
                    PredictedSavings = contractInfo?.PredictedSavings,
                    PredictedMarkup = contractInfo?.PredictedMarkup,
                    ValidityStartDate = contractInfo?.ValidityStartDate,
                    ValidityEndDate = contractInfo?.ValidityEndDate,
                    Local = tapInfo?.Local,
                    RiskLevelID = tapInfo?.RiskLevelID ?? 0,
                    ConsultantID = tapInfo?.ConsultantID ?? 0,
                    ContactClients = contactClients
                }
            };

            return projectInfo;
        }







        public bool UpdateScopeInfo(int contractId, PM_TAP_Scope scope)
        {
            try
            {
                PM_TAP_Scope scopeResult = _context.PM_TAP_Scope.Where(s => s.ContractID == contractId).FirstOrDefault();
                if (scopeResult != null)
                {
                    scopeResult.Scope = scope.Scope;
                    scopeResult.OutScope = scope.OutScope;
                    scopeResult.GeneralRisks = scope.GeneralRisks;
                    scopeResult.Premises = scope.Premises;
                    scopeResult.Deliveries = scope.Deliveries;
                    scopeResult.Goal = scope.Goal;
                    _context.PM_TAP_Scope.Update(scopeResult);
                }
                else
                {
                    scope.ContractID = contractId;
                    _context.PM_TAP_Scope.Add(scope);
                }
                _context.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }



        }

        public bool UpdateInfoContract(int contractId, ProjectManagementGeneralInfo infoContract)
        {
            try
            {
                var contract = _context.Contracts.Where(c => c.id == contractId).FirstOrDefault();
                contract.PredictedMarkup = infoContract.PredictedMarkup;
                contract.PredictedSavings = infoContract.PredictedSavings;
                contract.ValidityStartDate = infoContract.ValidityStartDate;
                contract.ValidityEndDate = infoContract.ValidityEndDate;
                _context.Contracts.Update(contract);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool UpdateResouces(int ContractID, PM_TAP_Resources UpdateResouces)
        {
            try
            {
                var resources = _context.PM_TAP_Resources.Where(a => a.ContractID == ContractID).FirstOrDefault();
                if (resources != null)
                {
                    resources.ThirdPartyServices = UpdateResouces.ThirdPartyServices;
                    resources.Acquitions = UpdateResouces.Acquitions;
                    resources.ExpectedEquipment = UpdateResouces.ExpectedEquipment;
                    resources.Mobilizations = UpdateResouces.Mobilizations;
                    resources.ThirdPartyServicesValue = UpdateResouces.ThirdPartyServicesValue;
                    resources.AcquitionsValue = UpdateResouces.AcquitionsValue;
                    resources.ExpectedEquipmentValue = UpdateResouces.ExpectedEquipmentValue;
                    resources.MobilizationsValue = UpdateResouces.MobilizationsValue;
                    resources.Mobilizations = UpdateResouces.Mobilizations;



                    return true;
                }
                else
                {

                    UpdateResouces.ContractID = ContractID;
                    _context.PM_TAP_Resources.Add(UpdateResouces);




                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


        public bool UpdateInfoTap(int contractId, ProjectManagementGeneralInfo infoContractTap)
        {
            try
            {
                var infoTap = _context.PM_TAP_General_Info.Where(t => t.ContractID == contractId).FirstOrDefault();
                if (infoTap != null)
                {
                    infoTap.Local = infoContractTap.Local;
                    infoTap.RiskLevelID = infoContractTap.RiskLevelID;
                    infoTap.ConsultantID = infoContractTap.ConsultantID;
                    _context.Update(infoTap);
                }
                else
                {

                    _context.Add(new PM_TAP_General_Info { ContractID = contractId, RiskLevelID = infoContractTap.RiskLevelID, Local = infoContractTap.Local, ConsultantID = infoContractTap.ConsultantID });

                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public bool UpdateInfoGenralClients(int contractId, List<PM_Information_General> contactClients)
        {
            try
            {
                foreach (var contactClient in contactClients)
                {
                    var client = _context.PM_Information_General.Where(c => c.ContractID == contractId && c.Type == contactClient.Type).FirstOrDefault();
                    if (client != null)
                    {
                        client.Email = contactClient.Email;
                        client.Name = contactClient.Name;
                        client.PhoneNumber = contactClient.PhoneNumber;
                        client.Role = contactClient.Role;
                        _context.PM_Information_General.Update(client);
                    }
                    else
                    {
                        contactClient.ContractID = contractId;
                        _context.PM_Information_General.Add(contactClient);
                    }


                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<BusinessUnitDto> GetAllBussinesUnit(int ContractID)
        {
            return _context.Finances
              .Where(f => f.ContractID == ContractID)
              .GroupBy(f => f.BusinessUnit)
              .Select(g => new BusinessUnitDto
              {
                  ContractID = g.First().ContractID,
                  ID = g.First().id,
                  BusinessUnit = g.First().BusinessUnit
              })
              .ToList();
        }

        public List<MilestonesItem> GetMilestoneItem(int contractID)
        {
            return _context.MilestonesItem
              .Where(a => a.ContractID == contractID &&
                a.Name != "REEMBOLSO" &&
                a.Name != "ADIANTAMENTO")
              .ToList();
        }

        public List<ActivityPlanHH> ActivityPlanHH(int contractID)
        {
            return _context.ActivityPlanHH(contractID).ToList();
        }

        public List<ActivityPlanHHTable> ActivityPlanHHTable(int contractID)
        {
            return _context.ActivityPlanHHTable(contractID).ToList();
        }


        public ActivityPlanHHDetail GetActivityPlanDetails(int contractID)
        {
            var activityPlanHH = _context.ActivityPlanHH(contractID).FirstOrDefault();
            var activityPlanHHTable = _context.ActivityPlanHHTable(contractID).ToList();


            var activityPlanDetail = new ActivityPlanHHDetail
            {
                ActivityPlanHH = activityPlanHH,
                ActivityPlanHHTable = activityPlanHHTable
            };

            return activityPlanDetail;
        }


        public List<MilestonesItem> GetAllMilestones()
        {
            return _context.MilestonesItem.ToList();
        }


        public List<vRightCardValue> RightCardValues(int contractID)
        {
            return _context.vRightCardValue.Where(a => a.ContractID == contractID).ToList();
        }


        public List<vPMAcquisitionCombined> GetAcquisitions(int ContractID)
        {
            var AcquisitionList = _context.vPM_Acquisition_Combined.Where(r => r.ContractID == ContractID).ToList();
            return AcquisitionList;
        }


        public List<vPMAcquisitionCost> GetAcquisitionsMade(string AquisitionPlannedID)
        {
            var AcquisitionsMadeList = _context.vPM_Acquisition_Cost.Where(r => r.AquisitionPlannedID == AquisitionPlannedID).ToList();
            return AcquisitionsMadeList;
        }

        public List<vIndirectcostChart> GetIndirectCostChartsByContract(int contractID)
        {
            return _context.vIndirectcostChart
              .Where(a => a.ContractID == contractID)
              .ToList();
        }

        public List<vContractExecutionHHCost> vContractExecutionHHCost(int contractID)
        {
            return _context.vContractExecutionHHCost
              .Where(a => a.ContractID == contractID)
              .ToList();
        }

        public List<vAcquisitionChart> GetStatusReportsGraph(int contractID)
        {
            return _context.vAcquisitionChart
              .Where(a => a.ContractID == contractID)
              .ToList();
        }
        public List<OrderManagementInfo> OrderManagementInfo(int contractID)
        {
            return _context.OrderManagementInfo.Where(a => a.ContractID == contractID).ToList();
        }

        public List<vhhGraphicDetail> GetHhGraphicDetail(int contractID)
        {
            return _context.vhhGraphicDetail.Where(a => a.ContractID == contractID).ToList();
        }

        public List<vMilestonesStatistics> vMilestonesStatistics(int contractID)
        {
            return _context.vMilestonesStatistics
              .Where(a => a.ContractID == contractID)
              .ToList();
        }


        public async Task<List<GetAllMilesStones>> GetMilestonesAsync(string milestoneId)
        {
            return await _context.GetAllMilesStones
              .FromSqlRaw("EXEC GetAllMilesStones @MilestonesID={0}", milestoneId)
              .ToListAsync();
        }

        public async Task<List<GetMilestonesExpected>> GetMilestoneNoExpected(string milestoneId)
        {
            try
            {
                var config = Configuration["MSSQLServerSQLConnection:MSSQLServerSQLConnectionString"];
                List<GetMilestonesExpected> getMilestoneNoExpecteds = new List<GetMilestonesExpected>();

                using (var conn = new SqlConnection(config))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand("GetMilestoneNoExpected", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MilestonesValueID", milestoneId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                getMilestoneNoExpecteds.Add(new GetMilestonesExpected
                                {
                                    FunctionName = reader["FunctionName"]?.ToString(),
                                    DepartmentName = reader["DepartmentName"]?.ToString(),
                                    DisplacementServiceName = reader["DisplacementServiceName"]?.ToString(),
                                    Hours = reader["Hours"] != DBNull.Value ? Convert.ToDecimal(reader["Hours"]) : (decimal?)null,
                                    TotalMilestones = reader["TotalMilestones"] != DBNull.Value ? Convert.ToDecimal(reader["TotalMilestones"]) : (decimal?)null,
                                    TotalExecutedManHour = reader["TotalExecutedManHour"] != DBNull.Value ? Convert.ToDecimal(reader["TotalExecutedManHour"]) : (decimal?)null,
                                    TotalPlannedManHour = reader["TotalPlannedManHour"] != DBNull.Value ? Convert.ToDecimal(reader["TotalPlannedManHour"]) : (decimal?)null,
                                    CustoPlanejado = reader["CustoPlanejado"] != DBNull.Value ? Convert.ToDecimal(reader["CustoPlanejado"]) : (decimal?)null,
                                    CustoExecutado = reader["CustoExecutado"] != DBNull.Value ? Convert.ToDecimal(reader["CustoExecutado"]) : (decimal?)null
                                });
                            }
                        }
                    }
                }

                return getMilestoneNoExpecteds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<GetMilestonesExpected>();
            }
        }


        public async Task<List<GetMilestonesExpected>> GetMilestonesExpected(string milestoneId)
        {
            try
            {
                var config = Configuration["MSSQLServerSQLConnection:MSSQLServerSQLConnectionString"];
                List<GetMilestonesExpected> getMilestoneExpecteds = new List<GetMilestonesExpected>();

                using (var conn = new SqlConnection(config))
                {
                    await conn.OpenAsync();

                    using (var cmd = new SqlCommand("GetMilestonesExpected", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MilestonesValueID", milestoneId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                getMilestoneExpecteds.Add(new GetMilestonesExpected
                                {
                                    FunctionName = reader["FunctionName"]?.ToString(),
                                    DepartmentName = reader["DepartmentName"]?.ToString(),
                                    DisplacementServiceName = reader["DisplacementServiceName"]?.ToString(),
                                    Hours = reader["Hours"] != DBNull.Value ? Convert.ToDecimal(reader["Hours"]) : (decimal?)null,
                                    ValueHour = reader["ValueHour"] != DBNull.Value ? Convert.ToDecimal(reader["ValueHour"]) : (decimal?)null,
                                    TotalMilestones = reader["TotalMilesStone"] != DBNull.Value ? Convert.ToDecimal(reader["TotalMilesStone"]) : (decimal?)null,
                                    TotalExecutedManHour = reader["TotalExecutedManHour"] != DBNull.Value ? Convert.ToDecimal(reader["TotalExecutedManHour"]) : (decimal?)null,
                                    TotalPlannedManHour = reader["TotalPlannedManHour"] != DBNull.Value ? Convert.ToDecimal(reader["TotalPlannedManHour"]) : (decimal?)null,
                                    CustoPlanejado = reader["CustoPlanejado"] != DBNull.Value ? Convert.ToDecimal(reader["CustoPlanejado"]) : (decimal?)null,
                                    CustoExecutado = reader["CustoExecutado"] != DBNull.Value ? Convert.ToDecimal(reader["CustoExecutado"]) : (decimal?)null
                                });
                            }
                        }
                    }
                }

                return getMilestoneExpecteds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<GetMilestonesExpected>();
            }
        }
        public CardsHHHours CardsHHHours(int contractId)
        {
            return _context.CardsHHHours
              .AsNoTracking()
              .FirstOrDefault(a => a.ContractID == contractId);
        }

        public Task<List<GetHhCostChart>> GetHhCostChartAsync(int contractId, DateTime startDate, DateTime endDate)
        {
            return _context.GetHhCostChart
              .FromSqlRaw("EXEC GetHhCostChart @ContractID={0}, @StartDate={1}, @EndDate={2}", contractId, startDate, endDate)
              .ToListAsync();
        }

        public Task<List<GetMilestoneFullReportByContract>> GetMilestoneFullReportByContract(int contractId)
        {
            return _context
              .Set<GetMilestoneFullReportByContract>()
              .FromSqlRaw("EXEC GetMilestoneFullReportByContract @ContractID = {0}", contractId)
              .ToListAsync();
        }

        public async Task<List<StatusReportGraphHH>> GetStatusReportGraphHH(int contractId)
        {
            return await _context.StatusReportsGraphHH.Where(r => r.ContractID == contractId).ToListAsync();

        }

        public async Task<MonitoringResponseDto?> GetCardsByContractIdAsync(int contractId)
        {
            var card = await _context.vw_MonitoringHoursCosts
              .AsNoTracking()
              .Where(r => r.ContractID == contractId)
              .Select(r => new MonitoringCardsDto
              {
                  ContractID = r.ContractID,
                  ExpectedHours = r.ExpectedHours,
                  PlannedHours = r.PlannedHours,
                  ExecutedHours = r.ExecutedHours,
                  HoursDifference = r.HoursDifference,

                  ExpectedCost = r.ExpectedCost,
                  PlannedCost = r.PlannedCost,
                  ExecutedCost = r.ExecutedCost,
                  CostDifference = r.CostDifference
              })
              .FirstOrDefaultAsync();

            if (card == null) return null;

            var graphic = await _context.vw_MilestonesData
              .AsNoTracking()
              .Where(r => r.ContractID == contractId)
              .Select(r => new MilestoneChartDto
              {
                  ContractID = r.ContractID,
                  MilestoneName = r.MilestonesName,
                  Status = r.CalculatedMilestoneStatus,
                  Hours = new HoursDto
                  {
                      Expected = r.ExpectedHours,
                      Planned = r.PlannedHours,
                      Executed = r.ExecutedHours
                  },
                  Costs = new costDto
                  {
                      Expected = r.ExpectedCost,
                      Planned = r.PlannedCost,
                      Executed = r.ExecutedCost
                  }
              })
                  .ToListAsync();

            var table = await _context.vw_MilestonesPerMilestoneDetail
              .AsNoTracking()
              .Where(r => r.ContractID == contractId)
              .Select(r => new MilestoneBreakdownDto
              {
                  ContractID = r.ContractID,
                  MilestonesID = r.MilestonesID,
                  MilestoneName = r.MilestonesName,

                  ExpectedHours = r.ExpectedHours ?? 0,
                  PlannedHours = r.PlannedHours ?? 0,
                  ExecutedHours = r.ExecutedHours ?? 0,
                  HoursDifference = r.HoursDifference ?? 0,

                  ExpectedCost = r.ExpectedCost ?? 0,
                  PlannedCost = r.PlannedCost ?? 0,
                  ExecutedCost = r.ExecutedCost ?? 0,
                  CostDifference = r.CostDifference ?? 0,
                  Status = r.CalculatedMilestoneStatus
              })
              .ToListAsync();
            var temporalTrend = await _context.vw_temporal_trend
                .AsNoTracking()
                .Where(r => r.ContractID == contractId)
                .ToListAsync();



            var groupedTrend = temporalTrend
     .GroupBy(r => r.Period.Date)
     .Select(g =>
     {
         var first = g.First(); 
         return new vw_temporal_trend
         {
             ContractID = first.ContractID,
             Period = g.Key,

             PlannedHoursBaseline = g.Max(x => x.PlannedHoursBaseline),
             PlannedCostBaseline = g.Max(x => x.PlannedCostBaseline),

             ScheduledHours = g.Sum(x => x.ScheduledHours),
             ScheduledCost = g.Sum(x => x.ScheduledCost),

             ActualHours = g.Sum(x => x.ActualHours),
             ActualCost = g.Sum(x => x.ActualCost),

             TotalBaselineHours = g.Max(x => x.TotalBaselineHours),
             TotalBaselineCost = g.Max(x => x.TotalBaselineCost),

            
             MonthYearPtBr = first.MonthYearPtBr
         };
     })
     .OrderBy(r => r.Period)
     .ToList();






            return new MonitoringResponseDto
            {
                Cards = card,
                Graphic = graphic,
                Table = table,
                TemporalTrend = temporalTrend,

            };
        }

        public async Task<vw_FinancialProgressSCurve> GetFinancialProgressSCurveAsync(int contractId)
        {
            return await _context.vw_FinancialProgressSCurve.FirstOrDefaultAsync(p => p.ContractID == contractId);
        }
    }
}




