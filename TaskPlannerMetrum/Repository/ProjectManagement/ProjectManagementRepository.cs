using Memt.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var existMilesTones = _context.MilestonesItem
                    .FirstOrDefault(n => n.Name == milestones.Name && n.ContractID == milestones.ContractID);

                if (existMilesTones == null)
                {
                    _context.MilestonesItem.Add(milestones);
                    _context.SaveChanges();

                    return _context.MilestonesItem
                        .Where(n => n.Name == milestones.Name && n.ContractID == milestones.ContractID)
                        .Select(i => i.ID)
                        .FirstOrDefault();
                }
                else
                {
                    return existMilesTones.ID;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while creating the MilestonesItem.", ex);
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

        public void DeleteMilestones(int contractID)
        {

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

        public bool UpdateMilesTones(MilestonesValue milestonesValue)
        {
            _context.MilestonesValue.Update(milestonesValue);
            _context.SaveChanges();
            return true;
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
            var typeOfCostToRemove = _context.Pm_Type_Cost.SingleOrDefault( t => t.ID == ID);

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

        public List<PmCostPlanned> GetPmCostPlanned( int ContractID )
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
    }
}
