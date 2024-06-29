using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using HRManager.STS.Data;
using HRManager.STS.Models;
using HRManager.STS.Quickstart.Account;
using IdentityServerHost.Quickstart.UI;
using HRManager.STS;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
using System.Configuration;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

Console.WriteLine(System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                    .FilePath);

var builder = WebApplication.CreateBuilder();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => origin == "http://localhost:4200")
            //.SetIsOriginAllowed(origin => origin == "https://192.168.1.21")
            .AllowCredentials();
        });
});

builder.Services.AddMvc();

builder.Services.AddTransient<IProfileService, CustomProfileService>();

var myBuilder = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
    options.Authentication.CookieLifetime = TimeSpan.FromMinutes(15);
})
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryClients(Config.Clients)
    .AddAspNetIdentity<ApplicationUser>()
    .AddProfileService<CustomProfileService>();
    //.AddDeveloperSigningCredential();

using (var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine))
{ 
    certStore.Open(OpenFlags.ReadOnly);
    var certCollection = certStore.Certificates.Find(
        X509FindType.FindByThumbprint,
        //"07e4ffdbdedfb307003291fec2d3c4e98b74eafb",
        "8522b390dbd5ffa12a675f12e8298d0ad40e45fb",
        validOnly: false);

    if (certCollection.Count > 0)
    {
        myBuilder.AddSigningCredential(certCollection[0]);
    }
}

var app = builder.Build();

app.UseCors(myAllowSpecificOrigins);
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseHttpsRedirection();
//app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();