using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetAllProjectInfoGeneralDTO
    {

        public int ContractID { get; set; }

        public PM_TAP_Scope Scopes { get; set; }


        public PM_TAP_Resources Resources { get; set; }
        public ProjectManagementGeneralInfo ProjectInfo { get; set; }

    }
}
