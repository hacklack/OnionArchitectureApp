using Application.ChatComponents.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Application.ChatComponents.Identity.IdentityHostingStartup))]
namespace Application.ChatComponents.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<IdentityDBContext>(options =>
                //    options.UseSqlite(
                //        context.Configuration.GetConnectionString("IdentityDBContextConnection")));

                services.AddDbContext<IdentityDBContext>();
                services.AddDefaultIdentity<ChatApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IdentityDBContext>();
            });
        }
    }
}