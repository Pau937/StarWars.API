using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StarWars.API.Mapper;
using StarWars.Core.Interfaces;
using StarWars.Core.Services;
using StarWars.DataAccess.Data;
using StarWars.DataAccess.Repositories;

namespace StarWars.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var mappingConfig = new MapperConfiguration(conf =>
			{
				conf.AddProfile(new MappingProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();

			services.AddControllers();
			services.AddSingleton(mapper);
			services.AddScoped(typeof(IAsyncRepository<>), typeof(EFCoreRepository<>));
			services.AddScoped<ICharacterService, CharacterService>();
			services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "StarWars.API",
					Description = "Manage your characters now!",
				});
			});

			services.AddMvc()
			 .AddJsonOptions(options => {
				 options.JsonSerializerOptions.IgnoreNullValues = true;
			 });
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "StarWars.API");
				c.RoutePrefix = string.Empty;
			});
		}
	}
}
