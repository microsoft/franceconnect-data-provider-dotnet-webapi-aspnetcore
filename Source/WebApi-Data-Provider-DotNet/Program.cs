// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using FranceConnect.DataProvider.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi_Data_Provider_DotNet.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Add database services.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsProduction())
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    else
    {
        // Suggested for development environments, as the database is thus hosted with the web app instead of a separate server.
        options.UseInMemoryDatabase("InMemory");
    }
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//TODO Regions

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}
else
{
    app.UseBrowserLink();
    app.UseMigrationsEndPoint();
}

app.UseRequestLocalization(new RequestLocalizationOptions { DefaultRequestCulture = new RequestCulture("fr-FR") });

app.UseCookiePolicy();

app.UseStaticFiles();

app.UseRouting();

app.UseDataProvider(options =>
{
    options.ChecktokenEndpoint = builder.Configuration["FranceConnect:ChecktokenEndpoint"];
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run();