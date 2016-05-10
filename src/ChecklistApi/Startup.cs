using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ChecklistDomainModel;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json;
using PostgreSqlProvider;

namespace ChecklistApi
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			// Set up configuration sources.
			var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables();
			this.Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddEntityFramework().AddNpgsql().AddDbContext<ChecklistContext>();
			services.AddScoped<IDataAccessProvider, PostgreSqlProvider.PostgreSqlProvider>();

			var jsonOutputFormatter = new JsonOutputFormatter
				                          {
					                          SerializerSettings =
						                          new JsonSerializerSettings
							                          {
								                          ReferenceLoopHandling =
									                          ReferenceLoopHandling.Ignore
							                          }
				                          };
			services.AddCors();
			services.AddMvc(
				options =>
					{
						options.OutputFormatters.Clear();
						options.OutputFormatters.Insert(0, jsonOutputFormatter);
					});			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseIISPlatformHandler();
			app.UseStaticFiles();
			app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
			app.UseMvc();			
		}

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}