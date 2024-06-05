using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

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





        public List<string> GetMilestonesNames(int contractID)
        {
            return _context.MilestonesItem.Where(c => c.ContractID == contractID).OrderBy(c => c.Name).Select(c => c.Name).ToList();
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

        public List<vMileStonesValue> GetAllMilestonesItem(int ContractID)
        {
            var GetAllMileStones = _context.vMileStonesValue.Where(i => i.ContractID == ContractID).ToList();

            return GetAllMileStones;
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




        //Atualiza se existir, caso contrário, cria um novo registro de Mobilization Made
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
        public bool CreateoutsourcedServicePlanned(PM_OutsourcedServices_Planned createoutsourcedServicePlanned)
        {
            var OutsourcedServicePlannedCreate = _context.PM_OutsourcedServices_Planned.Where(a => a.ID == createoutsourcedServicePlanned.ID);
            if (OutsourcedServicePlannedCreate != null)
            {

                _context.PM_OutsourcedServices_Planned.Add(createoutsourcedServicePlanned);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }



        public bool UpdateoutsourcedServicePlanned(string ID)
        {
            var outSourcedServiceUpdate = _context.PM_OutsourcedServices_Planned.FirstOrDefault(a => a.ID == ID);
            if (outSourcedServiceUpdate != null)
            {
                _context.PM_OutsourcedServices_Planned.Update(outSourcedServiceUpdate);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
