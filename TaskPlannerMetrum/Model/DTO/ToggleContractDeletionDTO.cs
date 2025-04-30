namespace TaskPlannerMetrum.DTO
{
    public class ToggleContractDeletionDTO
    {
        public int ContractID { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
