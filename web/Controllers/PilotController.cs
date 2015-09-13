using Domain.Persons;
using Factories;
using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PilotController : Controller
    {
        private static IPilotRepository PilotRepository = ServiceLocator.Get<PilotRepository>();
        // GET: Pilot
        public ActionResult Index()
        {
            var pilots = PilotRepository.GetAllPilots();
            return View(pilots);
        }

        // GET: Pilot/Details/5
        public ActionResult Details(int id)
        {
            var pilot = PilotRepository.GetPilot(id);
            return View(pilot);
        }

        // GET: Pilot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pilot/Create
        [HttpPost]
        public ActionResult Create(string name, string debutdate, string age, string team)
        {
            try
            {
                var pilotNew = PilotFactory.CreateNewPilot(name, debutdate, int.Parse(age), team);
               
                PilotRepository.AddPilot(pilotNew); 
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pilot/Edit/5
        public ActionResult Edit(int id)
        {
            var oldPilot = PilotRepository.GetPilot(id);
            return View();
        }

        // POST: Pilot/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Pilot pilot)
        {
            try
            {
                // TODO: Add update logic here
               var oldPilot = PilotRepository.GetPilot(id);
               PilotRepository.Save<Pilot>(pilot);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pilot/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pilot/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
              //  var pilotOnDelete = PilotRepository.GetPilot(id);
                PilotRepository.DeletePilot(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
