using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class PilotController : Controller
    {
        internal static IPilotRepository PilotRepository = ServiceLocator.Get<PilotRepository>();

        //
        // GET: /Pilot/
        
        public ActionResult Index()
        {
            var pilots = PilotRepository.GetAllPilots();
            return View(pilots);
        }

        //
        // GET: /Pilot/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Pilot/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pilot/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Pilot/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Pilot/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Pilot/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Pilot/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
