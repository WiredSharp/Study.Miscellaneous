using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Hangfire;
using Microsoft.Owin;
using Owin;
using Serilog;
using SerilogWeb.Owin;

[assembly: OwinStartup(typeof(Study.Hangfire.WebApplication.Startup))]

namespace Study.Hangfire.WebApplication
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			Log.Logger = new LoggerConfiguration()
									.MinimumLevel.Verbose()
									.WriteTo.RollingFile("logs/hangfire-study.log", retainedFileCountLimit: 4)
									.CreateLogger();
			GlobalConfiguration.Configuration
				.UseSqlServerStorage("hangfire");

			app.UseHangfireDashboard();
			app.UseHangfireServer();

			var configuration = new HttpConfiguration();
			SetupHttpConfiguration(configuration);
			app.UseWebApi(configuration);
			app.UseSerilogRequestContext();
		}

		private void SetupHttpConfiguration(HttpConfiguration config)
		{
			int version = GetType().Assembly.GetApiVersion();
			Log.Debug($"initializing web api configuration v{version}");
			config.Routes.MapHttpRoute("default", $"api/v{version}/{{controller}}", new { controller = "Task" });
			config.MapHttpAttributeRoutes();
		}
	}
}