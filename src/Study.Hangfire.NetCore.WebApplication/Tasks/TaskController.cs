using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study.Hangfire.NetCore.WebApplication.Tasks
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return CreatedAtAction("addtask", new { id = 1 }, new { message = "Task created" });
        }
    }
}
