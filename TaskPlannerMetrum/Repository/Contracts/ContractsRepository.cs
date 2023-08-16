using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.ModelViews;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace TaskPlannerMetrum.Repository.Contracts
{
    public class ContractsRepository : IContratosRepository
    {
        private MSSQLContext _context;

        public ContractsRepository(MSSQLContext context) { _context = context; }
        
        public dynamic GetAllContracts()
        {
            return _context.vContractList.Select(s => new { s.ContractID, s.EnableProject, s.PaymentMethod, s.InspectorName, s.ClientName, s.InternalCode, s.VendorName, s.StartDate, ValueTotal = s.ValueTotal.ToString("N", new System.Globalization.CultureInfo("pt-BR")), s.ClientOrder, InvoicedValueTotal = s.InvoicedValueTotal.ToString("N", new System.Globalization.CultureInfo("pt-BR")), s.Condition, s.BusinessUnit, s.Observation }).OrderBy(s => s.StartDate).ToList();
        }

        public bool UpdateContract(Model.Contracts contract)
        {
            try
            {
                Model.Contracts newContract = new Model.Contracts();
                var updateCotract = _context.Contracts.Where(i => i.id == contract.id).FirstOrDefault();
                updateCotract.StartDate = contract.StartDate;
                updateCotract.VendorID = contract.VendorID;
                updateCotract.PaymentMethod = contract.PaymentMethod;
                updateCotract.ClientID = contract.ClientID;
                updateCotract.Condition = contract.Condition;
                updateCotract.TagID = contract.TagID;
                updateCotract.InternalCode = contract.InternalCode;
                updateCotract.inspectorID= contract.inspectorID;
                updateCotract.id= contract.id;
                updateCotract.Observation= contract.Observation;
                updateCotract.ClientOrder= contract.ClientOrder;


                _context.Contracts.Update(updateCotract);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        public List<Model.User> GetFiscGest()
        {

            var pmo = _context.Department.Where(n => n.Name == "PMO").Select(i => i.Id).FirstOrDefault();
            var cnt = _context.Department.Where(n => n.Name == "DEPCNT").Select(i => i.Id).FirstOrDefault();
            List<Model.User> retorno = new List<Model.User>();
            var result = _context.Users.Where(d => d.DepartmentId == pmo || d.DepartmentId == cnt).OrderBy(i=> i.UserName).ToList();
            foreach (var item in result)
            {
                retorno.Add(new Model.User()
                {
                    FullName = item.FullName,
                    Id= item.Id,
                    DepartmentId = item.DepartmentId,
                });
            }
            return retorno;

        }
        public List<Model.User> GetSeller()
        {
            var id = _context.Department.Where(n => n.Name == "GERCOM").Select(i => i.Id).FirstOrDefault();
            List<Model.User> retorno = new List<Model.User>();
            var result = _context.Users.Where(d => d.DepartmentId == id).OrderBy(i=> i.UserName).ToList();
            foreach (var item in result)
            {
                retorno.Add(new Model.User()
                {
                    FullName = item.FullName,
                    Id= item.Id,
                    DepartmentId = item.DepartmentId,
                });
            }
            return retorno;

        }

        public bool Create(Model.DTO.ContractDTO newcontract)
        {
 
            try
            {
                _context.Contracts.Add(new Model.Contracts
                {
                    inspectorID = newcontract.inspectorID,
                    VendorID = newcontract.VendorID,
                    ClientID = newcontract.ClientID,
                    ClientOrder = newcontract.ClientOrder,
                    Condition = newcontract.Condition,
                    EnableProject = false,
                    InternalCode = newcontract.InternalCode,
                    Observation = newcontract.Observation,
                    PaymentMethod = newcontract.PaymentMethod,
                    StartDate = newcontract.StartDate,
                    TagID = newcontract.TagID,
                });
                _context.SaveChanges();
                var contractID = _context.Contracts.Select(i => i.id).Max();
                var pmoID = _context.Department.Where(n=> n.Name == "PMO").Select(i=> i.ID).FirstOrDefault();   
                if(newcontract.HoursPMO == true)
                {
                    _context.DepartmentProjects.Add(new Model.DepartmentProjects
                    {
                        ContractID = contractID,
                        DepartmentID = pmoID,
                       
                        
                    });
                    _context.SaveChanges();
                }
           
                return true ;
            }
            catch
            {
                return false;
            }
           
            
            
        }
        public bool CreateProject(Model.Contracts newProject)
        {
            Model.ProjectsNew project = new Model.ProjectsNew();
            project.ContractID = _context.Contracts.Where(i => i.InternalCode == newProject.InternalCode).Select(i => i.id).FirstOrDefault();
            project.PlannedManHour = 0;
            project.ExecutedManHour = 0;
            project.ExpectedManHor = 0;
            project.Status = "1";

                _context.Add(project);
                _context.SaveChanges();
                return true;
            

        }
        public List<Workspace> GetAllWorkSpace()
        {
            return _context.Workspace.OrderBy(n => n.Name).ToList();

        }

        public List<Model.ModelViews.vContractList> GetContractForProject()
        {
            var query = _context.vContractList.Where( c => c.EnableProject == false).Select(c => c);
            return query.ToList();

        }


        public dynamic GetAllProjectsContracts()
        {

            var contractsProjects = _context.vContractProject.Where(a=> a.EnableProject == true).ToList();

            return contractsProjects.Select(c => new
            {
                id = c.id,
                InternalCode = c.InternalCode,
                ClientName = c.ClientName,
                Expectedhours = c.Expectedhour,
                PlannedManHour = c.PlannedMenHour,
                ExecutedManHour = c.ExecutedMenHour,
                InspectorName = c.InspectorName,
                Progress = c.Progress,
                StartDate = c.StartDate,
                latesActivities = CountLateActivities(c.id)
            }).OrderByDescending(s => s.StartDate);



            return contractsProjects; 
            //var allContracts = _context.vContractProject.Where(e => e.EnableProject == true).OrderBy(n=> n.InternalCode).ToList();
            //List<dynamic> contractlist = new List<dynamic>();
            //foreach (var contract in allContracts)
            //{
            //    var expectedHour = _context.DepartmentProjects.Where(i => i.ContractID == contract.id).Select(h => h.ExpectedHour).Sum();
            //    var plannedhours = _context.ActivityPlan.Where(c => c.ContractID == contract.id).Select(p => p.PlannedManHour).Sum();
            //    var executedManHour = _context.ActivityPlan.Where(c => c.ContractID == contract.id).Select(p => p.ExecutedManHour).Sum();
            //    var taskcompleted = _context.ActivityPlan.Where(p => p.ContractID == contract.id &&( p.Status =="1"  || p.Status =="6")).Select(p => p.Status).ToList().Count();
            //    double allTask = _context.ActivityPlan.Where(p => p.ContractID == contract.id ).Select(p => p.Status).ToList().Count();
            //    double progress = (taskcompleted / allTask) * 100;
                
            //    if (taskcompleted == 0)
            //    {
            //        progress = 0;
            //    }

            //    var project = new
            //    {
            //        id = contract.id,
            //        InternalCode = contract.InternalCode,
            //        ClientName = contract.ClientName,
            //        Expectedhours = contract.Expectedhours,
            //        PlannedManHour = contract.PlannedManHour,
            //        ExecutedManHour = contract.ExecutedManHour,
            //        InspectorName = contract.InspectorName,
            //        Progress = contract.progresss Convert.ToDouble(progress.ToString("0.00")),
            //        StartDate = contract.StartDate,
            //    };
            //    contractlist.Add(project);
            //}
            //return contractlist;

        }

        public int CountLateActivities (int contractID)
        {
            return  _context.ActivityPlan.Where(c => c.ContractID ==  contractID && c.ScheduledDate < DateTime.Now && c.Status !="6" && c.Status !="1").Count();
          
        }

        public bool DesableProject(int id)
        {
            var project = _context.Contracts.Where(i => i.id== id).FirstOrDefault();
            project.EnableProject=false;
           
                _context.Update(project);
                _context.SaveChanges();
                return true;
            
        }

        public dynamic ContractDashboard(string year, float value)
        {
            bool yearExist  = _context.Goals.Where(y => y.Year== year).Any();
            if (!yearExist)
            {
                _context.Add(new Goals
                {
                    Value = value,
                    Year = year,
                });
                _context.SaveChanges();
            }
            if(yearExist && value != 0) 
            {
                var UpdateValue = _context.Goals.Where(y => y.Year == year).FirstOrDefault();   
                UpdateValue.Value = value;
                _context.Update(UpdateValue);
                _context.SaveChanges();

            }
            double balance = 0;
            double allfinanes = _context.Finances.Where(i => i.InvoicedValue != 0 && i.InvoicedDate.Year.ToString() == year).Select(i => i.InvoicedValue).Sum();
            double totalbillable = _context.Goals.Where(y => y.Year == year).Select(v => v.Value).FirstOrDefault();
            double goaltoinvoice = (_context.Goals.Where(y => y.Year == year).Select(v => v.Value).FirstOrDefault() - allfinanes);

            if(allfinanes >=totalbillable)
            {
                balance = allfinanes - totalbillable;
                goaltoinvoice = 0;
            }

            var retorno = new
            {
                goalachieved = allfinanes.ToString("N", new System.Globalization.CultureInfo("pt-BR")),
                totalbillable = totalbillable.ToString("N", new System.Globalization.CultureInfo("pt-BR")),
                goaltoinvoice = goaltoinvoice.ToString("N", new System.Globalization.CultureInfo("pt-BR")),
                balance = balance.ToString("N", new System.Globalization.CultureInfo("pt-BR")),

            };

            return retorno;

        }

        public dynamic ContractDasboardDate(DateTime date)
        {
            var annualgoal = _context.Goals.Where(y => y.Year == date.Year.ToString()).Select(v=> v.Value).FirstOrDefault();
            double monthgoal = (annualgoal/12);
            var billedmonth = _context.Finances.Where(i=>  i.InvoicedDate.Month == date.Month && i.InvoicedDate.Year == date.Year).Select(i=> i.InvoicedValue).Sum();

            var retorno = new
            {
                goalachieved = billedmonth.ToString("N", new System.Globalization.CultureInfo("pt-BR")) ,
                totalbillable = monthgoal.ToString("N", new System.Globalization.CultureInfo("pt-BR")) ,
                goaltoinvoice = (monthgoal> billedmonth) ? (monthgoal-billedmonth).ToString("N", new System.Globalization.CultureInfo("pt-BR")): 0.ToString("N", new System.Globalization.CultureInfo("pt-BR")),
                balance = (monthgoal > billedmonth) ? 0.ToString("N", new System.Globalization.CultureInfo("pt-BR")) : (billedmonth - monthgoal).ToString("N", new System.Globalization.CultureInfo("pt-BR")),

            };
            return retorno;
        }

        public bool UpdateObservation (int ID, string Observation)
        {
           
                var updateContract = _context.Contracts.Where(i => i.id == ID).FirstOrDefault();
                updateContract.Observation = Observation;
                _context.Update(updateContract);
                _context.SaveChanges();
                return true;
            
        }
    }

    

}
