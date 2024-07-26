using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IReportsPlannedExecutedViewewBussines
    {
        public ReportPlannedExecuted GetPlannedExecuted(ReportPlannedExecutedDTO reportPlannedExecuted);

        public ProjectOperational OperationalProjectReport(OperationalReportReportDTO OperationalReportReportDTO);

        public OptionsListFilter OptionsListFilter();


        public ReportInvoiceDetails InvoiceReport(ReportInvoice parameters);



    }
}
