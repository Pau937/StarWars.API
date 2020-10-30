using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StarWars.API.Mapper;
using StarWars.Core.Interfaces;
using StarWars.Core.Interfaces.Authentication;
using StarWars.Core.Models;
using StarWars.Core.Services;
using StarWars.DataAccess.Data;
using StarWars.DataAccess.Repositories;
using System.Net;
using System.Text;

namespace StarWars.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("DatabaseInMemoryForFunctionalTests"));

            ConfigureServices(services);
        }

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
            services.AddScoped<IAsyncRepository<Character>, CharacterRepository>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IPlanetService, PlanetService>();
            services.AddScoped<IEpisodeService, EpisodeService>();
            services.AddScoped<IFriendshipService, FriendshipService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:SecretToken").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                });

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
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.IgnoreNullValues = true;
             });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder => builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        await context.Response.WriteAsync(error.Error.Message);
                    }
                }));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

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
