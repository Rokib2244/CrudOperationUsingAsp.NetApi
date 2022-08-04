using Autofac;
using Autofac.Extensions.DependencyInjection;
using InventorySystem.Data;
using InventorySystem.Training;
using InventorySystem.Training.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Membership.BusinessObjects;
using InventorySystem.Membership;

namespace InventorySystem.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {

            var builder = new ConfigurationBuilder()
 .SetBasePath(env.ContentRootPath)
 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
 .AddEnvironmentVariables();
            WebHostEnvironment = env;
            Configuration = builder.Build();

            //IConfiguration configuration
            //Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public static ILifetimeScope AutofacContainer { get; set; }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionInfo = GetConnectionStringAndAssemblyName();
            builder
                .RegisterModule(new TrainingModule(connectionInfo.connectionString, connectionInfo.migrationAssemblyName));
            builder
                .RegisterModule(new MembershipModule(connectionInfo.connectionString, connectionInfo.migrationAssemblyName));

            builder
               .RegisterModule(new WebModule(connectionInfo.connectionString, connectionInfo.migrationAssemblyName));
            builder
                .RegisterModule(new ApiModule());

        }
        private (string connectionString, string migrationAssemblyName) GetConnectionStringAndAssemblyName()
        {
            var connectionStringName = "DefaultConnection";
            var connectionString = Configuration.GetConnectionString(connectionStringName);
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;
            return (connectionString, migrationAssemblyName);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionInfo = GetConnectionStringAndAssemblyName();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionInfo.connectionString,
                    b => b.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            services.AddDbContext<TrainingContext>(options =>
                options.UseSqlServer(connectionInfo.connectionString,
                    b => b.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers();
            services.AddAuthentication()
              .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
              {
                  x.RequireHttpsMetadata = false;
                  x.SaveToken = true;
                  x.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Audience"]
                  };
              });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AccessPermission", policy =>
                {
                    policy.AuthenticationSchemes.Clear();
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new ApiRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, ApiRequirementHandler>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InventorySystem.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventorySystem.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            //app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
