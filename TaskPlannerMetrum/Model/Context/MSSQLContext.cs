using Microsoft.EntityFrameworkCore;
using System;
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

        //Views
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
        public DbSet<vPlannedHours> vPlannedHours { get; set; }

        public DbSet<UserProjects> UserProjects { get; set; }

        public DbSet<vContractViewer> vContractViewer { get; set; }

        public DbSet<vReports_PlannedExecuted> vReports_PlannedExecuted { get; set; }   

        public DbSet<RatingProject> RatingProject { get; set; }

        public DbSet<RatingDescription> RatingDescription { get; set; }
        public DbSet<Rating> Rating { get; set; }


        public DbSet<vRating> vRating { get; set; }



        public DbSet<vActivePlans> vActivePlans { get; set; }



        

        public DbSet<MilestonesItem> MilestonesItem { get; set; }


        public DbSet<MilestonesValue> MilestonesValue { get; set; }









    }
}
