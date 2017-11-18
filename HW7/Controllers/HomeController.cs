using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW7.Controllers
{
    public class HomeController : Controller
    {
        string APIKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GiphyAPIKey"];
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Search Giphy";
            return View();
        }
    }
}