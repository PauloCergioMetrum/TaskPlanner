namespace TaskPlannerMetrum.Model.DTO
{
    public class AvailableHoursByFunctionTableDTO
    {
        public string Period { get; set; }
        public string FunctionName { get; set; }
        public int TotalAvailableHours { get; set; }
    }
}
