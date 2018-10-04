using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Study.Hangfire.WebApplication.Tasks
{
	[RoutePrefix("tasks")]
	public class TaskController: ApiController
	{
		[HttpGet]
		[Route("")]
		public IHttpActionResult GetAll()
		{
			Log.Debug("calling tasks.getall");
			return Ok(Enumerable.Range(1,10).Select(i => new { id = i }));
		}

		[HttpGet]
		[Route("{id}")]
		public IHttpActionResult GetTask(int id)
		{
			Log.Debug($"calling tasks.getbyid({id})");
			return Ok(new { id });
		}
	}
}