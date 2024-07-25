using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace TaskPlannerMetrum.Model.DTO
{
    public class NumberOfContractsForBusinessUnit
    {
        public string label { get; set; }

        public int value { get; set; }
    }
}
