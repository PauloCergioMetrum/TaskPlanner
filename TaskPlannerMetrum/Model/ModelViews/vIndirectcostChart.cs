using System;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vIndirectcostChart
    {
        public int ContractID { get; set; }
        public string? PlannedId { get; set; }       
        public string? MadeId { get; set; }          
        public int? TypeID { get; set; }          
        public string? TypeDescription { get; set; }
        public double? PlannedTotal { get; set; }     
        public double? MadeTotal { get; set; }
    }

    public class vStatusReportsGraph {
        public int TypeAcquisitionID { get; set; } // int
        public string TypeAcquisitionDescription { get; set; } // varchar(15)
        public int AmountPlanned { get; set; } // int
        public double PredictedTotal { get; set; } // float -> double
        public double TotalCost { get; set; } // float -> double
        public double AmountValue { get; set; } // float -> double
        public string AcquisitionDescription { get; set; } // nvarchar(max)
        public int ContractID { get; set; } // int
        public double Amount { get; set; } // float -> double
        public string AquisitionPlannedID { get; set; } // nvarchar(max)
        public double Value { get; set; } // float -> double
        public DateTime DateAcquisition { get; set; } // datetime
        public DateTime DateAcquisitionDelivery { get; set; } // datetime
        public double TotalMade { get; set; } // float -> double




    }

}
