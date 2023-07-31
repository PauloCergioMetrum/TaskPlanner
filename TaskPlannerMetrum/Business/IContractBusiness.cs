using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IContractBusiness
    {
        dynamic GetAllContracts();

        public bool UpdateContract(Model.Contracts contract);

        dynamic GetFiscGest();

        dynamic GetSeller();
        bool Create(Model.DTO.ContractDTO contract);

        dynamic GetAllWorkSpace();
        dynamic GetContractForProject();
        public dynamic GetAllProjectsContracts();

        public bool DesableProject(int id);

        public dynamic ContractDashboard(string year, float value);

        public bool UpdateObservation(int ID, string Observation);

        public dynamic ContractDasboardDate(DateTime date);

    }
}
