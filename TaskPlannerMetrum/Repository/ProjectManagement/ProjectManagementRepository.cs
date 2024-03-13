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
            var existAcquisitions = _context.PM_Acquisition_Planned.FirstOrDefault(n => n.Description == acquisitions.Description && n.ID == acquisitions.ID);

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
                acquisitionEntity.Amount = acquisitions.Amount;
                acquisitionEntity.Description = acquisitions.Description;
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int CreateAcquisitionMadeItem(PMAcquisitionMade acquisitionMade)
        {

            var existAcquisitionsMade = _context.PM_Acquisition_Made.Where(n => n.Value == acquisitionMade.Value && n.ID == acquisitionMade.ID).FirstOrDefault();
            if (existAcquisitionsMade == null)
            {
                _context.PM_Acquisition_Made.Add(acquisitionMade);
                _context.SaveChanges();
                return _context.PM_Acquisition_Made.Where(n => n.Value == acquisitionMade.Value && n.StatusAcquistionID == acquisitionMade.ID).Select(i => i.ID).FirstOrDefault();

            }
            else
            {
                return existAcquisitionsMade.ID;
            }
        }

        public bool ExistAcquisitionMade(int ID)
        {
            return _context.PM_Acquisition_Made.Any(n => n.ID == ID);
        }

        public bool DeleteAcquisitionMade(int ID)
        {
            try
            {
                var AcquisitionsMadeItem = _context.PM_Acquisition_Made.Where(u => u.ID == ID).FirstOrDefault();
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

            var existMilesTones = _context.MilestonesItem.Where(n => n.Name == milestones.Name && n.ContractID == milestones.ContractID).FirstOrDefault();
            if (existMilesTones == null)
            {
                _context.MilestonesItem.Add(milestones);
                _context.SaveChanges();
                return _context.MilestonesItem.Where(n => n.Name == milestones.Name && n.ContractID == milestones.ContractID).Select(i => i.ID).FirstOrDefault();
            }
            else
            {
                return existMilesTones.ID;
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

        public List<PMAcquisitionPlanned> GetAcquisitions(int ContractID)
        {
            var AcquisitionList = _context.PM_Acquisition_Planned.Where(r => r.ContractID == ContractID).ToList();
            return AcquisitionList;
        }

        public List<PMAcquisitionMade> GetAcquisitionsMade(int AquisitionPlannedID)
        {
            var AcquisitionsMadeList = _context.PM_Acquisition_Made.Where(r => r.AquisitionPlannedID == AquisitionPlannedID).ToList();
            return AcquisitionsMadeList;
        }
    }
}
