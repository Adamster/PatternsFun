using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "may the asp.net be with you ";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "may the about be with you";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "may the contact be with you";

            return View();
        }
    }
}
