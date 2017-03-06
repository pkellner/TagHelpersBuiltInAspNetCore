using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //services.AddDistributedSqlServerCache(options =>
            //{
            //    options.ConnectionString = @"Data Source=(localdb)\v11.0;Initial Catalog=DistCache;Integrated Security=True;";
            //    options.SchemaName = "dbo";
            //    options.TableName = "TestCache";
            //});

        }

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

