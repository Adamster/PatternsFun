using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "May the Patterns be with you";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Follow me on ";

            return View();
        }
    }
}