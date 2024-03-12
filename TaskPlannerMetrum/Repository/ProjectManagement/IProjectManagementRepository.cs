using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public interface IProjectManagementRepository
    {

        public Model.Contracts GetForecastByID(int id);

        public vContractList GetForecastByIDView(int id);

        public bool UpdateForecast(Model.Contracts forecast);


        public int CreateMilestonesItem(MilestonesItem milestones);

        public bool CreateMilestonesValue(MilestonesValue milestones);

        List<string> GetMilestonesNames(int contractID);

        public void DeleteMilestones(int contractID);



        public MilestonesValue GetMilestonesValueByID(int milestonesID);

        public bool UpdateMilesTones(MilestonesValue milestonesValue);


        public List<Positions> GetPositionsByGrup();

        public int CreateAcquisitionsItem(PMAcquisitionPlanned acquisitions);

        public bool ExistAcquisition(int ID);

        public bool DeleteAcquisition(int ID);

        public bool UpdateAcquisition(AcquisitionsDTO acquisitions);

        public int CreateAcquisitionMadeItem(PMAcquisitionMade acquisitionMade);

        public bool DeleteAcquisitionMade(int ID);

        public bool UpdateAcquisitionMadeItem(AcquisitionMadeDTO acquisitionMade);

        public bool ExistAcquisitionMade(int ID);

        public List<PMAcquisitionPlanned> GetAcquisitions(int ContractID);

        public List<PMAcquisitionMade> GetAcquisitionsMade(int AquisitionPlannedID);


    }
}
