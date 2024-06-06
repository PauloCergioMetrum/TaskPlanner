using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Model.Context
{
    public class MSSQLContext : DbContext
    {
        public MSSQLContext()
        {

        }
        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ActiviesScopeList> ActivitiesScopeList { get; set; }

        public DbSet<ActivityPlan> ActivityPlan { get; set; }

        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentProjects> DepartmentProjects { get; set; }

        public DbSet<Projects> Projects { get; set; }

        public DbSet<Clients> Clients { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<UserHourCosts> UserHourCosts { get; set; }

        public DbSet<Positions> Positions { get; set; }

        public DbSet<PMAcquisitionPlanned> PM_Acquisition_Planned { get; set; }

        public DbSet<PMAcquisitionMade> PM_Acquisition_Made { get; set; }

        public DbSet<PmTypeCost> Pm_Type_Cost { get; set; }

        public DbSet<PmCostPlanned> Pm_Cost_Planned { get; set; }
        public DbSet<PmCostMade> PM_Cost_Made { get; set; }

        public DbSet<ContractTechLeaders> ContractTechLeaders { get; set; }
        public DbSet<PM_Mobilization_Made> PM_Mobilization_Made { get; set; }
        public DbSet<PM_Mobilization_Planned> PM_Mobilization_Planned { get; set; }

        public DbSet<PM_OutsourcedServices_Planned> PM_OutsourcedServices_Planned { get; set; }
        public DbSet< PM_Type_OutsourcedServices> PM_Type_OutsourcedServices { get; set; }

        public DbSet<PM_OutsourcedServices_Made> PM_OutsourcedServices_Made { get; set; }

        public DbSet<PM_Type_Hours> PM_Type_Hours { get; set; }

        public DbSet<PM_Function_HH> PM_Function_HH { get; set; }

        public DbSet<PM_Hours> PM_Hours {  get; set; }  










        //Views


        public DbSet<vPM_OutsourcedServices_Combined> vPM_OutsourcedServices_Combined { get; set; }

        public DbSet<vPM_OutsourcedServices_Made_Combined> vPM_OutsourcedServices_Made_Combined { get; set; }




        public DbSet<vPM_Mobilization_Combined> vPM_Mobilization_Combined { get; set; }
        public DbSet<vMileStonesValue>vMileStonesValue { get; set; }
        public DbSet<vProjectList> vProjectList { get; set; }
        public DbSet<vUserList> vUserList { get; set; }

        public DbSet<vContractList> vContractList { get; set; }

        public DbSet<Contracts> Contracts { get; set; }


        public DbSet<ProjectsNew> ProjectsNew { get; set; }

        public DbSet<Workspace> Workspace { get; set; }

        public DbSet<Finances> Finances { get; set; }

        public DbSet<vFinanceContract> vFinanceContract { get; set; }

        public DbSet<vContractProject> vContractProject { get; set; }

        public DbSet<GroupTasks> GroupTasks { get; set; }

        public DbSet<UserTask> UserTask { get; set; }

        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Goals> Goals { get; set; }

        public DbSet<Service> Service { get; set; }

        public DbSet<vCalendar> vCalendar { get; set; }
        public DbSet<vPlannedHours> VPlannedHours { get; set; }

        public DbSet<UserProjects> UserProjects { get; set; }

        public DbSet<vContractViewer> vContractViewer { get; set; }

        public DbSet<vReports_PlannedExecuted> vReports_PlannedExecuted { get; set; }

        public DbSet<RatingProject> RatingProject { get; set; }

        public DbSet<RatingDescription> RatingDescription { get; set; }
        public DbSet<Rating> Rating { get; set; }


        public DbSet<vRating> vRating { get; set; }



        public DbSet<vActivePlans> vActivePlans { get; set; }
        public DbSet<Observation> Observation { get; set; }
        public DbSet<vBusinessUnit> vBusinessUnit { get; set; }

           









        public DbSet<MilestonesItem> MilestonesItem { get; set; }


        public DbSet<MilestonesValue> MilestonesValue { get; set; }

        public DbSet<vPMAcquisitionCombined> vPM_Acquisition_Combined { get; set; }

        public DbSet<vPMAcquisitionCost> vPM_Acquisition_Cost { get; set; }




        public DbSet<vActivePlanBusinessUnit> vActivePlanBusinessUnit { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoursExecutor>().HasNoKey();
            modelBuilder.Entity<ActivePlansByExecutor>().HasNoKey();
        }





        public virtual List<HoursExecutor> GetActivityPlanByExecutorTeamIDAndPeriod(string executorTeamIDs, string startDate, string endDate, string horaSchedule)
        {
            var query = $"EXECUTE [dbo].[GetActivityPlanByExecutorTeamIDAndPeriod] @ExecutorTeamIDs='{executorTeamIDs}', @StartDate='{startDate}', @EndDate='{endDate}', @HoraSchedule={horaSchedule.Replace("," , ".")}";

            return this.Set<HoursExecutor>().FromSqlRaw(query).ToList();
        }




        public List<ActivePlansByExecutor> GetActivityPlanDetailsByExecutorID(int executorID)
        {

          
     
            var query = $"EXECUTE [dbo].[GetActivityPlanDetailsByExecutorID] @ExecutorID={executorID}";

          
            return this.Set<ActivePlansByExecutor>()
                       .FromSqlRaw(query)
                       .ToList();
        }

     
    }


}

