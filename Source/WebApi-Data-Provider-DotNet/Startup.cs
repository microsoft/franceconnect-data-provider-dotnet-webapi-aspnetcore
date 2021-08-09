// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FranceConnect.DataProvider.Middleware;
using WebApi_Data_Provider_DotNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace WebApi_Data_Provider_DotNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Env = env;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment Env { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (Env.IsProduction())
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                }
                else
                {
                    // Suggested for development environments, as the database is thus hosted with the web app instead of a separate server.
                    options.UseInMemoryDatabase("InMemory");
                }
            });
            services.AddDatabaseDeveloperPageExceptionFilter();

            // Add framework services.
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ApplicationDbContext dbContext)
        {
            if (Env.IsProduction())
            {
                app.UseExceptionHandler("/Account/Error");
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseDataProvider(options =>
            {
                options.ChecktokenEndpoint = Configuration["FranceConnect:ChecktokenEndpoint"];
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
