using Microsoft.AspNetCore.Http;
using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class HoursCostCSV
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IFormFile FileCSV { get; set; }
    }
}
