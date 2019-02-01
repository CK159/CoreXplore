using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(App.Areas.Identity.IdentityHostingStartup))]

namespace App.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) => { });
		}
	}
}
