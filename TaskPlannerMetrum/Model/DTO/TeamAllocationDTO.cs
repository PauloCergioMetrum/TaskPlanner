using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class TeamAllocationResponseDTO
    {
        public List<TeamAllocationDTO> TeamAllocations { get; set; }
        public List<TeamAllocationDTO.TeamAllocationGraphicDTO> TeamAllocationGraphics { get; set; }
        public List<TeamAllocationDTO.GetTeamAllocationGraphicFunctions> TeamAllocationGraphicFunctions { get; set; }
        public List<TeamAllocationDTO.GetTeamAllocationCards> GetTeamAllocationCards { get; set; }

        public string[] BusinessUnits { get; set; }
    }

    public class TeamAllocationDTO
    {
        public long? ID { get; set; }  // O ID agora é nullable
        public string SaleOrder { get; set; }
        public string Client { get; set; }
        public string BusinessUnit { get; set; }
        public double? PlannedHours { get; set; }  // Agora é nullable
        public double? ExecutedHours { get; set; }  // Agora é nullable
        public DateTime? ScheduledDate { get; set; }  // Agora é nullable
        public string Executor { get; set; }
        public string FunctionName { get; set; }

        public class TeamAllocationGraphicDTO
        {
            public string FunctionName { get; set; }
            public string Month { get; set; }
            public double TotalPlannedHours { get; set; }
            public double TotalExecutedHours { get; set; }
            public double AvailableTime { get; set; }
        }

        public class GetTeamAllocationGraphicFunctions
        {
            public string FunctionName { get; set; }
            public int Quantity { get; set; }
            public int id { get; set; }


        }

        public class GetTeamAllocationCards
        {
            public DateTime DateStart { get; set; }
            public DateTime DateEnd { get; set; }
            public double? TotalPlannedHours { get; set; }
            public double? TotalExecutedHours { get; set; }
        }


        public class GetTeamFilterBusinessUnit
        {
            public string[] BusinessUnits { get; set; }
        }
    }
}