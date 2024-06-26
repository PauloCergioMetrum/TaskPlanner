using Memt.Logger;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.ActiviesScope;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public class ProjectManagementRepository : IProjectManagementRepository
    {
        private readonly MSSQLContext _context;

        public ProjectManagementRepository(MSSQLContext context)
        {
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

        public bool UpdateAcquisitionMadeItem(AcquisitionMadeDTO acquisitionMade)
        {
            try
            {
                var acquisitionMadeEntity = _context.PM_Acquisition_Made.Find(acquisitionMade.ID);
                acquisitionMadeEntity.ID = acquisitionMade.ID;
                acquisitionMadeEntity.StatusAcquistionID = acquisitionMade.StatusAcquistionID;
                acquisitionMadeEntity.Amount = acquisitionMade.Amount;
                acquisitionMadeEntity.Value = acquisitionMade.Value;
                acquisitionMadeEntity.DateAcquisition = acquisitionMade.DateAcquisition;
                acquisitionMadeEntity.DateAcquisitionDelivery = acquisitionMade.DateAcquisitionDelivery;
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





        public bool DeleteMilestones(string ID)
        {
            var milestone = _context.MilestonesValue.FirstOrDefault(m => m.ID == ID);
            if (milestone != null)
            {
                _context.MilestonesValue.Remove(milestone);
                _context.SaveChanges();
                return true;
            }
            return false;

        }


        public Model.Contracts GetForecastByID(int id)
        {

            return _context.Contracts.Where(i => i.id == id).FirstOrDefault();
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

        public bool CreateTypeOfCost(PmTypeCost pmTypeCost)

        {

            try

            {

                _context.Database.OpenConnection();

                var existingTypeOfCost = _context.Pm_Type_Cost.Find(pmTypeCost.ID);

                if (existingTypeOfCost != null)

                {

                    existingTypeOfCost.Name = pmTypeCost.Name;

                    existingTypeOfCost.isDefault = pmTypeCost.isDefault;

                    existingTypeOfCost.ContractID = pmTypeCost.ContractID;

                }

                else

                {

                    _context.Pm_Type_Cost.Add(pmTypeCost);

                }

                _context.SaveChanges();

                return true;

            }

            finally

            {

                _context.Database.CloseConnection();

            }

        }


        public bool DeleteTypeOfCost(string ID)
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

        public List<PmTypeCost> GetTypeOfCost(int ContractID)
        {
            var ListGetTypeOfCost = _context.Pm_Type_Cost.ToList();
            return ListGetTypeOfCost;
        }


        public bool CreateOrUpdatePredictedCost(PmCostPlanned pmCostPlanned)
        {
            var existingCost = _context.Pm_Cost_Planned.FirstOrDefault(c => c.Id == pmCostPlanned.Id);

            if (existingCost != null)
            {

                existingCost.TypeCostID = pmCostPlanned.TypeCostID;
                existingCost.Amount = pmCostPlanned.Amount;
                existingCost.ValueUnit = pmCostPlanned.ValueUnit;
                existingCost.Description = pmCostPlanned.Description;
                existingCost.ContractID = pmCostPlanned.ContractID;

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

        public List<PmCostPlanned> GetPmCostPlanned(int ContractID)
        {
            var ListPmCostPlanned = _context.Pm_Cost_Planned.Where(i => i.ContractID == ContractID).ToList();

            //var ListPmCostPlanned = _context.Pm_Cost_Planned.ToList();
            return ListPmCostPlanned;
        }


        public bool CreateOrUpdateCostMade(PmCostMade pmCostMade)
        {
            try
            {
                var existingCostMade = _context.PM_Cost_Made.FirstOrDefault(c => c.Id == pmCostMade.Id);

                if (existingCostMade != null)
                {

                    existingCostMade.Amount = pmCostMade.Amount;
                    existingCostMade.ValueUnit = pmCostMade.ValueUnit;
                    existingCostMade.Description = pmCostMade.Description;
                    existingCostMade.Pm_Cost_PlannedID = pmCostMade.Pm_Cost_PlannedID;
                    //existingCostMade.TypeCostID = pmCostMade.TypeCostID = "sjkbgdhufgsdhf";

                    _context.SaveChanges();
                    return true;
                }
                else
                {

                    _context.PM_Cost_Made.Add(pmCostMade);
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
            var RemoveCost = _context.PM_Cost_Made.SingleOrDefault(t => t.Id == ID);
            if (RemoveCost != null)
            {
                _context.PM_Cost_Made.Remove(RemoveCost);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<PmCostMade> GetPmCostMade(string Pm_Cost_PlannedID)
        {
            var ListGetCostMade = _context.PM_Cost_Made.Where(i => i.Pm_Cost_PlannedID == Pm_Cost_PlannedID).ToList();
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
            UpdateMilesStonesValue.Description = milesTonesDTO.Description;
            _context.Update(UpdateMilesStonesValue);
            _context.SaveChanges();
            return true;



        }


        // MOBILIZAÇÃO  PLANNED
        public List<vPM_Mobilization_Combined> GetMobilization(int contractID)
        {
            return _context.vPM_Mobilization_Combined.Where(a => a.ContractID == contractID).ToList();
        }

        public List<PM_Mobilization_Made> GetMobilizationMade(string mobilizationPlannedID)
        {
            return _context.PM_Mobilization_Made.Where(a => a.MobilizationPlannedID == mobilizationPlannedID).ToList();
        }

        //Atualiza se existir, caso contrário, cria um novo registro de Mobilization Planned
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





        public List<MilestonesItem> GetMilestonesNames(int contractID)
        {
            return _context.MilestonesItem.Where(a => a.ContractID == contractID).ToList();
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

        // ACOMPANHAMENTO DE ESCOPO 





        public bool CreatScopeTraking(PM_Scope_Traking pMScopeTraking)
        {
            var existingScopeTracking = _context.PM_Scope_Traking.FirstOrDefault(a => a.ID == pMScopeTraking.ID);



            if (existingScopeTracking != null)
            {
                existingScopeTracking.Local = pMScopeTraking.Local;
                existingScopeTracking.ProjectReviewsPlanned = pMScopeTraking.ProjectReviewsPlanned;
                existingScopeTracking.Item = pMScopeTraking.Item;
                existingScopeTracking.Remote = pMScopeTraking.Remote;
                existingScopeTracking.ProjectReviewsMade = pMScopeTraking.ProjectReviewsMade;
                existingScopeTracking.PeportReviewsPlanned = pMScopeTraking.PeportReviewsPlanned;
                existingScopeTracking.PeportReviewsMade = pMScopeTraking.PeportReviewsMade;
                existingScopeTracking.DevelopSystemReviewsPlanned = pMScopeTraking.DevelopSystemReviewsPlanned;
                existingScopeTracking.DevelopSystemReviewsMade = pMScopeTraking.DevelopSystemReviewsMade;
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
                existingScopeTracking.Local = pMScopeTraking.Local;
                existingScopeTracking.ProjectReviewsPlanned = pMScopeTraking.ProjectReviewsPlanned;
                existingScopeTracking.Item = pMScopeTraking.Item;
                existingScopeTracking.Remote = pMScopeTraking.Remote;
                existingScopeTracking.ProjectReviewsMade = pMScopeTraking.ProjectReviewsMade;
                existingScopeTracking.PeportReviewsPlanned = pMScopeTraking.PeportReviewsPlanned;
                existingScopeTracking.PeportReviewsMade = pMScopeTraking.PeportReviewsMade;
                existingScopeTracking.DevelopSystemReviewsPlanned = pMScopeTraking.DevelopSystemReviewsPlanned;
                existingScopeTracking.DevelopSystemReviewsMade = pMScopeTraking.DevelopSystemReviewsMade;
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
                ExistiScopeChange.Coin = pM_Scope_Change.Coin;
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
                ExistiScopeChangeUpdate.Coin = pM_Scope_Change.Coin;
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
            //apagardepois
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
                existingMilesType.ValueHour = dto.Hours;


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
            return _context.vMileStonesValue.Where(a => a.ContractID == ContractID).ToList();   
        }

       
    }
}



