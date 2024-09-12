using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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


        //TABELAS
        public DbSet<RatingProject> RatingProject { get; set; }
        public DbSet<RatingDescription> RatingDescription { get; set; }
        public DbSet<Rating> Rating { get; set; }
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
        public DbSet<PmCostMade> PmCostMade { get; set; }
        public DbSet<ContractTechLeaders> ContractTechLeaders { get; set; }
        public DbSet<PM_Mobilization_Made> PM_Mobilization_Made { get; set; }
        public DbSet<vPM_Mobilization_Made>vPM_Mobilization_Made { get; set; } 
        public DbSet<PM_Mobilization_Planned> PM_Mobilization_Planned { get; set; }
        public DbSet<PM_OutsourcedServices_Planned> PM_OutsourcedServices_Planned { get; set; }
        public DbSet<PM_Type_OutsourcedServices> PM_Type_OutsourcedServices { get; set; }
        public DbSet<PM_OutsourcedServices_Made> PM_OutsourcedServices_Made { get; set; }
        public DbSet<PM_Type_Hours> PM_Type_Hours { get; set; }
        public DbSet<PM_Function_HH> PM_Function_HH { get; set; }
        public DbSet<PM_Man_Hours> PM_Man_Hours { get; set; }
        public DbSet<PM_Scope_Traking> PM_Scope_Traking { get; set; }
        public DbSet<PM_Scope_Change> PM_Scope_Change { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Functions> Functions { get; set; }
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
        public DbSet<PM_TAP_RiskLevel> PM_TAP_RiskLevel { get; set; }
        public DbSet<PM_TAP_General_Info> PM_TAP_General_Info { get; set; }
        public DbSet<PM_TAP_Scope> PM_TAP_Scope { get; set; }
        public DbSet<PM_Information_General> PM_Information_General { get; set; }

        public DbSet<PM_TAP_Resources> PM_TAP_Resources { get; set; }
        public DbSet<PM_MilestonesType> PM_MilestonesType { get; set; }


        public DbSet<PM_TAP_Resources> PM_TAP_Resources { get; set; }

        
        //VIEWS
        public DbSet<vPmCostMade> vPmCostMade { get; set; }     
        public DbSet<vCalendar> vCalendar { get; set; }
        public DbSet<vPlannedHours> VPlannedHours { get; set; }
        public DbSet<UserProjects> UserProjects { get; set; }
        public DbSet<vContractViewer> vContractViewer { get; set; }
        public DbSet<vReports_PlannedExecuted> vReports_PlannedExecuted { get; set; }
        public DbSet<vRating> vRating { get; set; }
        public DbSet<DisplacementServices> DisplacementServices { get; set; }
        public DbSet<vActivePlans> vActivePlans { get; set; }
        public DbSet<Observation> Observation { get; set; }
        public DbSet<BusinessUnit> BusinessUnit { get; set; }
        public DbSet<MilestonesItem> MilestonesItem { get; set; }
        public DbSet<MilestonesValue> MilestonesValue { get; set; }
        public DbSet<vPMAcquisitionCombined> vPM_Acquisition_Combined { get; set; }
        public DbSet<vPMAcquisitionCost> vPM_Acquisition_Cost { get; set; }
        public DbSet<vPM_Functions_MilestoneType> vPM_Functions_MilestoneType { get; set; }
        public DbSet<PM_Functions_MilestoneType> PM_Functions_MilestoneType { get; set; }
        public DbSet<vPM_OutsourcedServices_Combined> vPM_OutsourcedServices_Combined { get; set; }
        public DbSet<vPM_OutsourcedServices_Made_Combined> vPM_OutsourcedServices_Made_Combined { get; set; }
        public DbSet<vPM_Mam_Hours> vPM_Mam_Hours { get; set; }
        public DbSet<vPM_Mobilization_Combined> vPM_Mobilization_Combined { get; set; }
        public DbSet<vMileStonesValue> vMileStonesValue { get; set; }
        public DbSet<vProjectList> vProjectList { get; set; }
        public DbSet<vUserList> vUserList { get; set; }
        public DbSet<vContractList> vContractList { get; set; }
        public DbSet<vActivePlanBusinessUnit> vActivePlanBusinessUnit { get; set; }
        public DbSet<vPM_MilestoneType> vPM_MilestoneType { get; set; }
        public DbSet<vUsersView> vUsersView { get; set; }
        public DbSet<Management> Management { get; set; }
        public DbSet<vPmCostPlanned> vPmCostPlanned {  get; set; }
        public DbSet<vPm_Cost_Planned> vPm_Cost_Planned { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoursExecutor>().HasNoKey();
            modelBuilder.Entity<ActivePlansByExecutor>().HasNoKey();
            modelBuilder.Entity<GetEquipamentAvaibilaity>().HasNoKey();
            modelBuilder.Entity<GetFinanceMilestones>().HasNoKey();
            modelBuilder.Entity<GetMilestoneType_HH_Details>().HasNoKey();
            modelBuilder.Entity<GetMilestones>().HasNoKey();
            modelBuilder.Entity<ProjectCharterDatails>().HasNoKey();
            modelBuilder.Entity<TechLeaderDTO>().HasNoKey();
            modelBuilder.Entity<StatusForPeriod>().HasNoKey();
            modelBuilder.Entity<NumberOfContractsForBusinessUnit>().HasNoKey();
            modelBuilder.Entity<BalancePerProject>().HasNoKey();
            modelBuilder.Entity<GetNumberOfContractsForBusinessUnit>().HasNoKey();
            modelBuilder.Entity<OperationalRelationshipTable>().HasNoKey();

            modelBuilder.Entity<PredictedInvoiced>().HasNoKey();
            modelBuilder.Entity<MaterialServices>().HasNoKey();
            modelBuilder.Entity<BillingPerBusinessUnit>().HasNoKey();
            modelBuilder.Entity<ReportDetailsTable>().HasNoKey();
            modelBuilder.Entity<GoalRealizationReport>().HasNoKey();

            modelBuilder.Entity<ActivityPlanHH>().HasNoKey();
            modelBuilder.Entity<ActivityPlanHHTable>().HasNoKey();




            
        }




        public virtual List<HoursExecutor> GetActivityPlanByExecutorTeamIDAndPeriod(string executorTeamIDs, string startDate, string endDate, string horaSchedule)
        {
            var query = $"EXECUTE [dbo].[GetActivityPlanByExecutorTeamIDAndPeriod] @ExecutorTeamIDs='{executorTeamIDs}', @StartDate='{startDate}', @EndDate='{endDate}', @HoraSchedule={horaSchedule.Replace(",", ".")}";

            return this.Set<HoursExecutor>().FromSqlRaw(query).ToList();
        }




        public List<GetEquipamentAvaibilaity> GetEquipmentAvailability(int EquipamentID, DateTime StartDate, DateTime EndDate)
        {

            var sql = "EXEC [dbo].[GetEquipamentAvaibilaity] @EquipamentID, @StartDate, @EndDate";


            return this.Set<GetEquipamentAvaibilaity>()
                        .FromSqlRaw(sql,
                            new SqlParameter("@EquipamentID", EquipamentID),
                            new SqlParameter("@StartDate", StartDate),
                            new SqlParameter("@EndDate", EndDate))
                        .ToList();
        }

        //AVANÇO FISICO 

        public List<GetFinanceMilestones> GetFinanceMilestones(int ContractID)
        {
            var sql = "EXEC [dbo].[GetFinanceMilestones] @ContractID";

            return this.Set<GetFinanceMilestones>()
                      .FromSqlRaw(sql, new SqlParameter("@ContractID", ContractID))
                      .ToList();
        }
        public List<ActivePlansByExecutor> GetActivityPlanDetailsByExecutorID(int executorID)
        {
            var query = $"EXECUTE [dbo].[GetActivityPlanDetailsByExecutorID] @ExecutorID={executorID}";
            return this.Set<ActivePlansByExecutor>()
                       .FromSqlRaw(query)
                       .ToList();
        }
        public List<GetMilestoneType_HH_Details> GetMilestoneType_HH_Details(string MilestonesValueID)
        {
            var query = $"EXECUTE [dbo].[GetMilestoneType_HH_Details] @MilestonesValueID='{MilestonesValueID}'";
            return this.Set<GetMilestoneType_HH_Details>()
                       .FromSqlRaw(query)
                       .ToList();
        }
        public List<GetMilestones> GetMilestones(int contractID)
        {
            var sql = "EXEC [dbo].[GetMilestones] @ContractID";

            return this.Set<GetMilestones>()
                       .FromSqlRaw(sql, new SqlParameter("@ContractID", contractID))
                       .ToList();
        }

        public ProjectCharterDatails GetProjectCharterDatails(int contractID)
        {
            var sql = "[dbo].[GetProjectCharterDetails] @ContractID";
            return this.Set<ProjectCharterDatails>()
                .FromSqlRaw(sql, new SqlParameter("@ContractID", contractID)).ToList().FirstOrDefault();

        }
        public List<TechLeaderDTO> GetTechLeaders(int contractID)
        {
            var sql = "[dbo].[GetTechLeadersByContract] @ContractID";
            return this.Set<TechLeaderDTO>()
                .FromSqlRaw(sql, new SqlParameter("@ContractID", contractID)).ToList();

        }

        public List<ActivityPlanHH> ActivityPlanHH(int contractID)
        {
            var sql = "[dbo].[ActivityPlanHH] @ContractID";
            return this.Set<ActivityPlanHH>()
                .FromSqlRaw(sql, new SqlParameter("@ContractID", contractID))
                .ToList();
        }
        public List<ActivityPlanHHTable> ActivityPlanHHTable(int contractID)
        {
            var sql = "[dbo].[ActivityPlanHHTable] @ContractID";
            return this.Set<ActivityPlanHHTable>()
                .FromSqlRaw(sql, new SqlParameter("@ContractID", contractID))
                .ToList();
        }


    }


}