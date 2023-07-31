namespace TaskPlannerMetrum.Model
{
    public class ProjectsNew
    {

        public int ID { get; set; }

        public int PlannedManHour { get; set; }
        
        public int ExecutedManHour { get; set; }    

        public int ExpectedManHor { get; set; } 

        public int DepartamentID { get; set; }      

        public int ContractID { get; set; } 

        public string Status { get; set; }  
    }
}
