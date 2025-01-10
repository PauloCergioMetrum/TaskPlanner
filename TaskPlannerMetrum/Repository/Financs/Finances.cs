using DocumentFormat.OpenXml.Wordprocessing;
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

            if (newfinance.Guarantee == null)
            {
                newfinance.Guarantee = false;
            }

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
                paymentCondition = newfinance.PaymentCondition,
                InvoicedValue = newfinance.InvoicedValue,
                Value = newfinance.Value,
                Guarantee = newfinance.Guarantee,
                GuaranteePeriod = newfinance.GuaranteePeriod,
                WorkSpaceID = newfinance.WorkSpaceID,
            });
            _context.SaveChanges();
            int FinanceID = _context.Finances.Where(f => f.ContractID == newfinance.ContractID).OrderBy(i => i.id).Select(f => f.id).LastOrDefault();

            if (_context.DepartmentProjects.Where(d => d.DepartmentID == newfinance.DepartmentID && d.ContractID == newfinance.ContractID).FirstOrDefault() == null)
            {
                CreateProject(new Model.DepartmentProjects
                {
                    DepartmentID = newfinance.DepartmentID,
                    ContractID = newfinance.ContractID,
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


            var blockContract = _context.Contracts.Where(i => i.id == contractId).FirstOrDefault();


            if (blockContract != null)
            {
                var financas = _context.vFinanceContract.Where(i => i.ContractID == contractId).ToList();
                var ClientId = blockContract.ClientID;
                var WorkSpace = _context.Workspace.Where(i => i.ID == blockContract.TagID).Select(n => n.Name).FirstOrDefault();

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
                double totalpercents = invoicevalues / value * 100;
                if (double.IsNaN(totalpercents))
                {
                    totalpercents = 0;
                }

                var contractfinances = new
                {
                    ContractName = blockContract.InternalCode,
                    TotalValue = value.ToString("N", new System.Globalization.CultureInfo("pt-BR")),
                    percents = totalpercents.ToString("0.00").Replace(",", "."),
                    ClientName = _context.Clients.Where(i => i.Id == ClientId).Select(n => n.Name).FirstOrDefault().ToString(),
                    totalbilled = invoicevalues.ToString("N", new System.Globalization.CultureInfo("pt-BR")),
                    WorkSpace = WorkSpace,
                    StatusID = blockContract.StatusID,
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
                        Status = setStatusDate(f.id),
                        StatusDpv = f.StatusDpv,
                        f.EndDate,
                        f.ExpectedInvoiceDate,
                        f.FinanceType,
                        f.Guarantee,
                        f.GuaranteePeriod,
                        f.DateExpectedGarantee,
                        f.StatusGuarantee,
                    })
                };
                return contractfinances;
            }
            else
            {

                return null;
            }



        }

        public string SetOnGoingDate(int id)
        {
            var finance = _context.Finances.Where(i => i.id == id).FirstOrDefault();

            if (finance == null)
            {
                return "ID não encontrado";
            }

            if (finance.InvoicedDate > new DateTime(1901, 1, 1))
            {
                return "FATURADO";
            }

            switch (finance.StatusDpv)
            {
                case "CANCELADO":
                case "ENCERRADO":
                case "PARALISADO":
                    return finance.StatusDpv;
                default:
                    return "EM ANDAMENTO";
            }
        }



        public string setStatusDate(int id)
        {
            var finance = _context.Finances.Where(i => i.id == id).FirstOrDefault();

            if (finance.Status == "CANCELADO" || finance.Status == "CANCELADO")
            {
                return finance.Status;
            }
            if (finance.Status == "BLOQUEADO" || finance.Status == "BLOQUEADO")
            {
                return finance.Status;
            }

            if (finance.EndDate.Date != Convert.ToDateTime("01/01/1901"))
            {

                if (finance.InvoicedDate.Date != Convert.ToDateTime("01/01/1901"))
                {

                    return "NO PRAZO";

                }

                else
                {
                    if (finance.EndDate.Date >= DateTime.Now.Date)
                    {
                        return "NO PRAZO";
                    }
                    else
                    {
                        return "ATRASADO";
                    }
                }
            }

            else
            {


                if (finance.InvoicedDate.Date != Convert.ToDateTime("01/01/1901"))
                {

                    if (finance.InvoicedDate > finance.BaseDate)
                    {
                        return "ATRASADO";
                    }
                    else
                    {
                        return "NO PRAZO";
                    }

                }
                else
                {

                    if (finance.BaseDate.Date >= DateTime.Now.Date)
                    {
                        return "NO PRAZO";
                    }
                    else
                    {
                        return "ATRASADO";
                    }
                }




            }


        }

        public dynamic GetContractInfo(int id)
        {

            var contract = _context.Contracts.Where(i => i.id == id).FirstOrDefault();
            var contractinfo = new
            {
                InternalCode = contract.InternalCode,
                statusID = contract.StatusID,
                id = contract.id,
                clientName = _context.Clients.Where(i => i.Id == contract.ClientID).Select(n => n.Name).FirstOrDefault(),
            };

            return contractinfo;
        }


        public bool UpdateFinances(Model.Finances finances)
        {
            var updatefinances = _context.Finances.FirstOrDefault(i => i.id == finances.id);

            if (updatefinances != null)
            {

                updatefinances.InvoicedValue = finances.InvoicedValue;
                updatefinances.Value = finances.Value;
                updatefinances.Status = finances.Status;
                updatefinances.Amount = finances.Amount;
                updatefinances.EndDate = finances.EndDate;
                updatefinances.BaseDate = finances.BaseDate;
                updatefinances.ContractID = finances.ContractID;
                updatefinances.Billing = finances.Billing;
                updatefinances.Description = finances.Description;
                updatefinances.InvoicedDate = finances.InvoicedDate;
                updatefinances.DepartmentID = finances.DepartmentID;
                updatefinances.ExpectedInvoiceDate = finances.ExpectedInvoiceDate;
                updatefinances.invoice = finances.invoice;
                updatefinances.paymentCondition = finances.paymentCondition;
                updatefinances.Guarantee = finances.Guarantee;
                updatefinances.GuaranteePeriod = finances.GuaranteePeriod;
                updatefinances.BusinessUnit = finances.BusinessUnit;



                updatefinances.StatusDpv = finances.StatusDpv;


                _context.Finances.Update(updatefinances);
                _context.SaveChanges();

                if (F_UpdateDepartamentID(updatefinances.ContractID, finances.id, finances.DepartmentID))
                {
                    return true;
                }
            }

            return false;
        }



        public bool F_UpdateDepartamentID(int contractID, int finanaceID, int financeDepartamentID)
        {
            try
            {
                var updateDepartmentProjects = _context.DepartmentProjects.FirstOrDefault(c => c.ContractID == contractID && c.FinancesID == finanaceID);

                if (updateDepartmentProjects != null)
                {
                    updateDepartmentProjects.DepartmentID = financeDepartamentID;
                    _context.DepartmentProjects.Update(updateDepartmentProjects);
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


        public dynamic getAllServices(string type)
        {
            return _context.Service.Where(i => i.Type == type).ToList();
        }

       



        public bool DeleteAllService(int ID)
        {
           var services = _context.Service.Where(i => i.ID == ID).ToList();        

            if (services.Any())            {
                _context.Service.RemoveRange(services); 
                _context.SaveChanges();
                return true;
            }

            return false; 

        }



        public string UpdateAllService(Service service)
        {
            var newService = new Service
            {
                Type = service.Type,
                Description = service.Description,
                ID = service.ID,
            };

            _context.Service.UpdateRange(newService);
            _context.SaveChanges();
            return "Atualização realizada com sucesso";
        }





        public string CreateAllService(Service service)
        {
            var newService = new Service
            {
                Type = service.Type,
                Description = service.Description,
            };

            _context.Service.Add(newService);
            _context.SaveChanges();
            return "Material criado com sucesso";  
        }

        public dynamic DuplicateFinance(DuplicateFinanceDTO Finance)
        {
            try
            {


                Model.Finances financeMatriz = _context.Finances.Where(i => i.id == Finance.id).FirstOrDefault();
                var duplicateFinance = new Model.Finances();

                if (financeMatriz != null)
                {
                    duplicateFinance.invoice = financeMatriz.invoice;
                    duplicateFinance.paymentCondition = financeMatriz.paymentCondition;
                    duplicateFinance.StatusDpv = financeMatriz.StatusDpv;
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

                    duplicateFinance.BusinessUnit = Finance.BusinessUnit;
                    duplicateFinance.BaseDate = Finance.BaseDate;
                    _context.Finances.Add(duplicateFinance);
                    _context.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

       
    }
}
