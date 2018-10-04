using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Study.Hangfire.NetCore.WebApplication
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
									.MinimumLevel.Verbose()
									.WriteTo.RollingFile("logs/hangfire-study-core.log", retainedFileCountLimit: 4)
									.CreateLogger();
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.ConfigureLogging(logging =>
				{
					logging.SetMinimumLevel(LogLevel.Trace);
				}).UseSerilog()
				.UseStartup<Startup>();
		}
	}
}
