using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWars.API;
using StarWars.DataAccess.Data;

namespace StarWars.FunctionalTests
{
	public class WebTest : WebApplicationFactory<Startup>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.UseEnvironment("Testing");

			builder.ConfigureServices((IServiceCollection services) =>
			{
				services.AddEntityFrameworkInMemoryDatabase();

				var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

				services.AddDbContext<DataContext>(options =>
				{
					options.UseInMemoryDatabase("DatabaseInMemoryForFunctionalTests");
					options.UseInternalServiceProvider(provider);
				});

				var sp = services.BuildServiceProvider();

				using (var scope = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<DataContext>();
					db.Database.EnsureCreated();
				}
			});
		}
	}
}
