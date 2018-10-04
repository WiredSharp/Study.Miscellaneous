using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Study.Hangfire.NetCore.WebApplication
{
	public class Startup
	{
		private readonly IConfiguration _config;

		public Startup(IConfiguration config)
		{
			_config = config;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHangfire(config => config.UseSqlServerStorage(_config.GetConnectionString("hangfire")));
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHangfireDashboard("/tasks");
			app.UseHangfireServer();
			app.Map($"/configuration", routeApp => routeApp.Run(async (context) =>
			{
				await context.Response.WriteAsync($"<p><strong>Hangfire: </strong>{_config.GetConnectionString("hangfire")}</p>");
			}));
            int apiVersion = GetType().Assembly.GetApiVersion();
            //app.Map($"/api/v{apiVersion}/tasks", routeApp => routeApp.Run(async (context) => 
			//{
			//	await context.Response.WriteAsync("task added");
			//}));
			app.UseMvc(routes =>
                {
                    routes.MapRoute("default", $"", new { controller = "task", action = "index" });
                    routes.MapRoute("MvcController", $"api/v{apiVersion}/{{controller=task}}/{{action=index}}");
                });
		}
	}
}
