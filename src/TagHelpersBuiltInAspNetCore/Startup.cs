using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TagHelpersBuiltInAspNetCore.Data;
using TagHelpersBuiltInAspNetCore.Models;
using TagHelpersBuiltInAspNetCore.Services;

namespace TagHelpersBuiltInAspNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.
                        GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddDistributedSqlServerCache(opt =>
            {
                opt.ConnectionString = 
                  Configuration.GetConnectionString("DefaultConnection");
                opt.SchemaName = "dbo";
                opt.TableName = "SQLCache";
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }








        //services.AddDistributedSqlServerCache(opt =>
        //    {
        //        //opt.ConnectionString = "Data Source=.;Initial Catalog=cachetest;Persist Security Info=True;Trusted_Connection=Yes;";
        //        opt.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        //        opt.SchemaName = "dbo";
        //        opt.TableName = "SQLCache";
        //    });

//        CREATE TABLE[dbo].[SQLCache](    
//            [Id]
//        [nvarchar](449) NOT NULL,

//[Value][varbinary](max) NOT NULL,

//[ExpiresAtTime][datetimeoffset](7) NOT NULL,

//[SlidingExpirationInSeconds][bigint]
//        NULL,   
//            [AbsoluteExpiration]
//        [datetimeoffset](7) NULL,   
//            CONSTRAINT[pk_Id] PRIMARY KEY CLUSTERED([Id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
//            ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                // need route and attribute on controller:  [Area("Blogs")]
                routes.MapRoute(name: "areaRoute",
                                    template: "{area:exists}/{controller=Home}/{action=Index}");

                // default route for non-areas
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                //routes.MapRoute(
                //    name: "route1",
                //    template: "{controller=Home}/{action=Index}/{id?}/{myParam1?}");

                //routes.MapRoute(
                //    name: "route2",
                //    template: "{controller=Home}/{action=Index}/{id?}/{myParam2?}");





            });

            
        }
    }
}

// default route for non-areas
// need route and attribute on controller:  [Area("Blogs")]
//routes.MapRoute(name: "areaRoute",
//                    template: "{area:exists}/{controller=Home}/{action=Index}");


//routes.MapRoute(
//                    name: "default",
//                    template: "{controller=Home}/{action=Index}/{id?}/{myParam1?}/{myParam2?}/{myParam3?}");

