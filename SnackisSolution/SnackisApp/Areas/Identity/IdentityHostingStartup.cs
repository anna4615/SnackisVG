using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Data;

[assembly: HostingStartup(typeof(SnackisApp.Areas.Identity.IdentityHostingStartup))]
namespace SnackisApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SnackisContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SnackisContext")));

                services.AddDefaultIdentity<SnackisUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<SnackisContext>();
            });
        }
    }
}