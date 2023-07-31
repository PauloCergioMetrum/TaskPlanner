using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Data.Converter.Implementations;

using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Repository.Generic;
using TaskPlannerMetrum.Repository.Users;
using System.Security.Cryptography;
using TaskPlannerMetrum.Repository.DepartmentProjects;
using TaskPlannerMetrum.Repository.Projects;
using TaskPlannerMetrum.Repository.Contracts;

namespace TaskPlannerMetrum.Business.Implementations
{


    public class ContractBusinessImplementation : IContractBusiness
    {

        private readonly IContratosRepository  _contractRepository;
      



        public ContractBusinessImplementation(IContratosRepository contractRepository)
        {
            _contractRepository = contractRepository;
        
        }
        public dynamic GetAllContracts()
        {
            return _contractRepository.GetAllContracts();   
        }

        public bool UpdateContract(Contracts contract)
        {
            return _contractRepository.UpdateContract(contract);
        }

        public dynamic GetFiscGest()
        {
            return _contractRepository.GetFiscGest();
        }

        public dynamic GetSeller()
        {
            return _contractRepository.GetSeller();
        }

        public bool Create(Model.DTO.ContractDTO contract)
        {
            return _contractRepository.Create(contract);
        }

        public dynamic GetAllWorkSpace()
        {
            return _contractRepository.GetAllWorkSpace();  
        }

        public dynamic GetContractForProject()
        {
            return _contractRepository.GetContractForProject();
        }

        public dynamic GetAllProjectsContracts()
        {
            return _contractRepository.GetAllProjectsContracts();
        }

        public bool DesableProject(int id)
        {
            return _contractRepository.DesableProject(id);
        }

        public dynamic ContractDashboard(string year, float value)
        {
            return _contractRepository.ContractDashboard(year, value);
        }

        public bool UpdateObservation(int ID, string Observation)
        {
            return _contractRepository.UpdateObservation(ID, Observation);
        }

        public dynamic ContractDasboardDate(DateTime date)
        {
            return _contractRepository.ContractDasboardDate(date);
        }
    }
}
