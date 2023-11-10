using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlannerMetrum.Business.Implementations;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Repository.Generic;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;
using Serilog;
using TaskPlannerMetrum.Repository.Projects;
using TaskPlannerMetrum.Repository.DepartmentProjects;
using TaskPlannerMetrum.Repository.ActiviesScope;
using TaskPlannerMetrum.Repository.ActivityPlan;
using TaskPlannerMetrum.Services.Implementations;
using TaskPlannerMetrum.Services;
using TaskPlannerMetrum.Repository.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskPlannerMetrum.Configurations;
using TaskPlannerMetrum.Repository.ProjectProgress;
using Microsoft.EntityFrameworkCore.Design;
using TaskPlannerMetrum.Repository.Contracts;
using TaskPlannerMetrum.Repository.Financs;
using TaskPlannerMetrum.Repository.Calendar;
using TaskPlannerMetrum.Repository.ContractViewer;
using TaskPlannerMetrum.Repository.Clients;
using TaskPlannerMetrum.Repository.Rating;
using TaskPlannerMetrum.Repository.UserHourCostRepository;
using TaskPlannerMetrum.Repository.ReportsViewer;

namespace TaskPlannerMetrum
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    Configuration.GetSection("TokenConfigurations")
                )
                .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            services.AddControllers();
            //var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            // services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            var connection = Configuration["MSSQLServerSQLConnection:MSSQLServerSQLConnectionString"];
            services.AddDbContext<MSSQLContext>(options => options.UseSqlServer(connection));

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();
            services.AddApiVersioning();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Task Planner Metrum API",
                        Version = "v1",
                        Description = "API RESTful developed for Task Planner Metrum'",
                        Contact = new OpenApiContact
                        {
                            Name = "Augusto Morais",
                            Url = new Uri("https://github.com/augustomemt")
                        }
                    });
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserBusiness, UserBusinessImplementation>();
            services.AddScoped<IDepartmentBusiness, DepartmentBusinessImplementation>();
            services.AddScoped<IProjectsBusiness, ProjectsBusinessImplementation>();
            services.AddScoped<IClientsBusiness, ClientsBusinessImplementation>();
            services.AddScoped<IDashboardBusiness, DashboardBusinessImplementation>();
            services.AddScoped<IContractBusiness, ContractBusinessImplementation>();
            services.AddScoped<IFinancesBusiness, FinancesBusiness>();
            services.AddScoped<ICalendarBusiness, CalendarBusiness>();
            services.AddScoped<IRatingBusiness, RatingBusiness>();
            services.AddScoped<IUserHourCostBusiness, UserHourCostBusinessImplementation>();





            services.AddScoped<IActiviesScopeBusiness, ActivesScopeBusinessImplementation>();
            services.AddScoped<IActivityPlanBusiness, ActivityPlanBusiness>();
            services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IActivityPlanRepository, ActivityPlanRepository>();
            services.AddScoped<IActiviesRepository, ActiviesScopeRepository>();
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IDepartmentProjectsRepository, DepartmentProjectsRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IContratosRepository, ContractsRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<IFinances, Finances>();


          
            services.AddScoped<IReportsPlannedExecutedViewerRepository, ReportsPlannedExecutedViewerRepository>();
            services.AddScoped<IReportsPlannedExecutedViewewBussines, ReportsPlannedExecutedViewerBusiness>();




            services.AddScoped<IContractViewerRepository, ContractViewerRepository>();
            services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IUserHourCostRepository, UserHourCostRepository>();







        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "API RESTful developed for Task Planner Metrum - v1");
            });



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }
    }
}
