using System;
using System.Text.Json.Serialization;
using TaskPlannerMetrum.Model.Base;

namespace TaskPlannerMetrum.Model
{
    public class Clients : BaseEntity
    {
       
        public string Name { get; set; }
        public int WorkspaceID { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PMContactName { get; set; }
        public string PMPhoneNumber { get; set; }
        public string Cnpj { get; set; }
        public bool? SoftDelete { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
