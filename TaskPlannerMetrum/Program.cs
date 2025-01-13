using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memt.Logger;
using System.Security.Cryptography;

namespace TaskPlannerMetrum
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Logger.CreateLogger("TaskPlanner");

            Logger.SetLogLevel(ELoggerType.Debug);
            Logger.AddNewAppender(EAppenderType.File);
            Logger.AddNewAppender(EAppenderType.Console);

            Logger.Activate();

            Logger.EnableDebugMode();
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
