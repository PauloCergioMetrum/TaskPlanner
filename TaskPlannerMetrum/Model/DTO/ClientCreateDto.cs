namespace TaskPlannerMetrum.Model.DTO
{
    public class ClientCreateDto
    {
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PMContactName { get; set; }
        public string PMPhoneNumber { get; set; }
        public int WorkspaceID { get; set; }
    }

}
