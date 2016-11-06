using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hangfire;

namespace HangfireSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var id = BackgroundJob.Enqueue(() => Debug.WriteLine("Hello, "));
            BackgroundJob.ContinueWith(id, () => Debug.WriteLine("world!"));

            return View();
        }
    }
}
