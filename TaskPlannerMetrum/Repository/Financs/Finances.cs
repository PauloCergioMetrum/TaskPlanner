using log4net.Util;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.Financs
{
    public class Finances : IFinances
    {
        private MSSQLContext _context;

        public Finances(MSSQLContext context) { _context = context; }

        public bool Create(Model.DTO.financeDTO newfinance)
        {
            newfinance.Billing = "";
            newfinance.StatusDpv = "EM ANDAMENTO";

            _context.Finances.Add(new Model.Finances
            {
                Amount = newfinance.Amount,
                BaseDate = newfinance.BaseDate,
                Billing = newfinance.Billing,
                BusinessUnit = newfinance.BusinessUnit,
                ContractID = newfinance.ContractID,
                DepartmentID = newfinance.DepartmentID,
                Description = newfinance.Description,
                EndDate = newfinance.EndDate,
                ExpectedInvoiceDate = newfinance.ExpectedInvoiceDate,
                FinanceType = newfinance.FinanceType,
                invoice = newfinance.invoice,
                InvoicedDate = newfinance.InvoicedDate,
                Status = newfinance.Status,
                StatusDpv = newfinance.StatusDpv,
                paymentCondition = newfinance.paymentCondition,
                InvoicedValue = newfinance.InvoicedValue,
                Value = newfinance.Value,
                WorkSpaceID = newfinance.WorkSpaceID,

            });
            _context.SaveChanges();
            int FinanceID = _context.Finances.Where(f => f.ContractID== newfinance.ContractID).OrderBy(i => i.id).Select(f => f.id).LastOrDefault();

            if(_context.DepartmentProjects.Where(d=> d.DepartmentID == newfinance.DepartmentID && d.ContractID == newfinance.ContractID).FirstOrDefault() == null)
            {
                CreateProject(new Model.DepartmentProjects
                {
                    DepartmentID= newfinance.DepartmentID,
                    ContractID= newfinance.ContractID,
                    FinancesID = FinanceID,
                    ExpectedHour = 0,
                });
            }
            

            return true;

        }

        public bool CreateProject(Model.DepartmentProjects project)
        {


            _context.Add(project);
            _context.SaveChanges();
            return true;

        }

        public bool DeleteFinance(int id)
        {


            var delete = _context.Finances.FirstOrDefault(i => i.id == id);
            if (delete != null)
            {
                _context.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;
           
        }

        public List<Model.ModelViews.vFinanceContract> GetAllFinances()
        {
            return _context.vFinanceContract.ToList();
        }
        public dynamic GetFinancesById(int contractId)
        {

            var financas = _context.vFinanceContract.Where(i => i.ContractID == contractId).ToList();
            var Contract = _context.Contracts.Where(i => i.id == contractId).FirstOrDefault();
            var ClientId = _context.Contracts.Where(i => i.id == contractId).Select(n => n.ClientID).FirstOrDefault();
            var WorkSpace = _context.Workspace.Where(i => i.ID == Contract.TagID).Select(n => n.Name).FirstOrDefault();

            double value = 0;
            double invoicevalues = 0;

            foreach (var valuefinanc in financas)
            {
                value = value + valuefinanc.Value;
            }
            foreach (var invoicevalue in financas)
            {
                invoicevalues = invoicevalues + invoicevalue.InvoicedValue;
            }
            double totalpercents = invoicevalues/value * 100;
            if (Convert.ToString(totalpercents) =="NaN")
            {
                totalpercents = 0;
            }
            var contractfinances = new
            {
                ContractName = Contract.InternalCode,
                TotalValue = value.ToString("N", new System.Globalization.CultureInfo("pt-BR")),
                percents = totalpercents.ToString("0.00").Replace(",", "."),
                ClientName = _context.Clients.Where(i => i.Id==ClientId).Select(n => n.Name).FirstOrDefault().ToString(),
                totalbilled = invoicevalues.ToString("N", new System.Globalization.CultureInfo("pt-BR")),
                WorkSpace = WorkSpace,
                Finances = financas.Select(f => new
                {
                    f.id,
                    f.ContractID,
                    f.InvoicedDate,
                    f.BaseDate,
                    f.Amount,
                    f.Billing,
                    f.invoice,
                    f.InvoicedValue,
                    f.paymentCondition,
                    f.Value,
                    f.BusinessUnit,
                    f.ContractName,
                    f.DepartmentName,
                    f.DepartmentID,
                    f.Description,
                    f.Status,
                    f.EndDate,
                    f.ExpectedInvoiceDate,
                    f.FinanceType,

                    StatusDpv = f.InvoicedDate > Convert.ToDateTime("0001-01-01 00:00:00.0000000") ? "FATURADO" : f.StatusDpv
                })


            };
            return contractfinances;


        }
        public dynamic GetContractInfo(int id)
        {
            //var query = _context.Contracts.Where(i => i.id == id).FirstOrDefault();
            var contract = _context.Contracts.Where(i => i.id == id).FirstOrDefault();


            var contractinfo = new
            {
                InternalCode = _context.Contracts.Where(i => i.id == id).Select(n => n.InternalCode).FirstOrDefault(),
                id = _context.Contracts.Where(i => i.id == id).Select(n => n.id).FirstOrDefault(),
                clientName = _context.Clients.Where(i => i.Id == contract.ClientID).Select(n => n.Name).FirstOrDefault()
            };
            return contractinfo;
        }
        public bool UpdateFinances(Model.Finances finances)
        {

            var updatefinances = _context.Finances.Where(i => i.id == finances.id).FirstOrDefault();
            updatefinances.InvoicedValue = finances.InvoicedValue;
            updatefinances.Value = finances.Value;
            updatefinances.Status = finances.Status;
            updatefinances.Amount = finances.Amount;
            updatefinances.EndDate = finances.EndDate;
            updatefinances.BaseDate = finances.BaseDate;
            updatefinances.ContractID = finances.ContractID;
            updatefinances.Billing= finances.Billing;
            updatefinances.id = finances.id;
            updatefinances.Description = finances.Description;
            updatefinances.InvoicedDate = finances.InvoicedDate;
            updatefinances.DepartmentID = finances.DepartmentID;
            updatefinances.ExpectedInvoiceDate = finances.ExpectedInvoiceDate;
            updatefinances.invoice = finances.invoice;
            updatefinances.paymentCondition = finances.paymentCondition;
            updatefinances.StatusDpv = finances.StatusDpv;
            _context.Finances.Update(updatefinances);
            _context.SaveChanges();

            var updateDepartmentProjects = _context.DepartmentProjects.Where(c => c.ContractID == updatefinances.ContractID && updatefinances.DepartmentID == c.DepartmentID).FirstOrDefault();
            updateDepartmentProjects.DepartmentID= finances.DepartmentID;
            


            return true;

        }

        public dynamic getAllServices(string type)
        {
            return _context.Service.Where(i => i.Type == type).ToList();
        }

        public dynamic DuplicateFinance(DuplicateFinanceDTO Finance)
        {
            try
            {
                //Dados do objeto Matriz que será utilizado para duplicar a Finança
                var financeMatriz = _context.Finances.Where(i => i.id == Finance.id).FirstOrDefault();


                var duplicateFinance =  new Model.Finances();

                //Nao foi possivel fazer dessa forma por que o ID duplica 
                // var duplicateFinance = financeMatriz;
                // duplicateFinance.BusinessUnit = Finance.BusinessUnit;
                // duplicateFinance.BaseDate = Finance.BaseDate;




                //Solução
                duplicateFinance.invoice = financeMatriz.invoice;
                duplicateFinance.paymentCondition = financeMatriz.paymentCondition;
                duplicateFinance.StatusDpv= financeMatriz.StatusDpv;
                duplicateFinance.Amount = financeMatriz.Amount;
                duplicateFinance.InvoicedDate = financeMatriz.InvoicedDate;
                duplicateFinance.EndDate = financeMatriz.EndDate;
                duplicateFinance.Billing = financeMatriz.Billing;
                duplicateFinance.ContractID = financeMatriz.ContractID;
                duplicateFinance.DepartmentID = financeMatriz.DepartmentID;
                duplicateFinance.Description = financeMatriz.Description;
                duplicateFinance.ExpectedInvoiceDate = financeMatriz.ExpectedInvoiceDate;
                duplicateFinance.FinanceType = financeMatriz.FinanceType;
                duplicateFinance.Value = financeMatriz.Value;
                duplicateFinance.InvoicedValue = financeMatriz.InvoicedValue;
                duplicateFinance.Status = financeMatriz.Status;
                duplicateFinance.WorkSpaceID = financeMatriz.WorkSpaceID;


                //Dados que o front enviou 
                duplicateFinance.BusinessUnit = Finance.BusinessUnit;
                duplicateFinance.BaseDate = Finance.BaseDate;


                _context.Finances.Add(duplicateFinance);    
                _context.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return ex.Message.ToString();
            }

        }
    }
}
