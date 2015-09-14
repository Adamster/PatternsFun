using System.Web.Mvc;
using Domain.Dto;
using Factories;
using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;

namespace Web.Controllers
{
    public class PilotController : Controller
    {
        private static readonly IPilotRepository PilotRepository = ServiceLocator.Get<PilotRepository>();
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
            return View(oldPilot);
        }

        // POST: Pilot/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string name, string debutdate, string age, string team)
        {
            try
            {
                // TODO: Add update logic here
                var oldPilot = PilotRepository.GetPilot(id);
                //var newPilot = PilotFactory.CreateNewPilot(name, debutdate, int.Parse(age), team);
                //var editedPilot = oldPilot.PilotEdit(oldPilot, newPilot);

                PilotRepository.UpdatePilot(oldPilot, new PilotUpdateDto
                {
                    Id= id,
                    Name = name,

                });
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
            var pilot = PilotRepository.GetPilot(id);
            return View(pilot);
        }

        // POST: Pilot/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
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