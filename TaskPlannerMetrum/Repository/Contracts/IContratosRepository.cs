using System;
using System.Collections.Generic;
using System.Security;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.ModelViews;


namespace TaskPlannerMetrum.Repository.Contracts
{
    public interface IContratosRepository
    {

        public dynamic GetAllContracts();

        public bool UpdateContract(Model.Contracts contract);

        public List<Model.User> GetFiscGest();

        public List<Model.User> GetSeller();

        public bool Create(Model.DTO.ContractDTO newcontract);
        public List<Workspace> GetAllWorkSpace();

        public List<Model.ModelViews.vContractList> GetContractForProject();

        public dynamic GetAllProjectsContracts();

        public bool DesableProject(int id);

        public dynamic ContractDashboard(string year, float value);

        public bool UpdateObservation(int ID, string Observation);

        public dynamic ContractDasboardDate(DateTime date);

        public bool CompareDate(int ContractID);

      
        public bool ObservationCreate(Model.Observation observation);

        public bool DeleteObservation(int ContractID);


        //public List<Model.Observation> AllObservation( int ContractID);

        public dynamic AllObservation (int  contractID);


        public bool ObservationUpdate(int ContractID, string Datails, DateTime Date);





    }
}
