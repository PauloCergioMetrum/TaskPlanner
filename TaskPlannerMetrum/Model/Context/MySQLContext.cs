using Microsoft.EntityFrameworkCore;
using System;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Model.Context
{
    public class MySQLContext : DbContext
    {

        public MySQLContext()
        {

        }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<vUserList> vUserList { get; set; }

        public DbSet<vContractList> vContractsList { get; set; }

        public static implicit operator MySQLContext(MSSQLContext v)
        {
            throw new NotImplementedException();
        }
    }
}
