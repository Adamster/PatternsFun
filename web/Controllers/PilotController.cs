using System;
using System.Web.Mvc;
using Domain.Dto;
using Factories;
using Repository;
using Repository.Interfaces;
using Utils;
using Web.Models;

namespace Web.Controllers
{
    public class PilotController : Controller
    {
       // private static readonly IPilotRepository PilotRepository = ServiceLocator.Get<PilotRepository>();
        public readonly IPilotRepository _pilotRepository;

        [Obsolete]
        public PilotController()
        {
        }

        public PilotController(IPilotRepository pilotRepository)
        {
            _pilotRepository = pilotRepository;
        }

        // GET: Pilot
        [HttpGet]
        public ActionResult Index()
        {
            var pilots = _pilotRepository.GetAllPilots();
            return View(pilots);
        }

        // GET: Pilot/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            var pilot = _pilotRepository.GetPilot(id);
            var modelPilot = new PilotModel(pilot);
            return View(modelPilot);
        }

        // GET: Pilot/Create
        [HttpGet]
        public ActionResult Create()
        {
            var model = new PilotModel();
            return View(model);
        }

        // POST: Pilot/Create
        [HttpPost]
        public ActionResult Create(PilotModel pilotModel)
        {
            try
            {
                var pilotNew = PilotFactory.CreateNewPilot(pilotModel.Name, pilotModel.DebutDate.ToString(),
                    pilotModel.Age, pilotModel.Team);

                _pilotRepository.AddPilot(pilotNew);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                return View();
            }
        }

        // GET: Pilot/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var oldPilot = _pilotRepository.GetPilot(id);
            var modelPilot = new PilotModel(oldPilot);

            return View(modelPilot);
        }

        // POST: Pilot/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PilotModel editedPilot)
        {
            try
            {
                var oldPilot = _pilotRepository.GetPilot(id);
                _pilotRepository.UpdatePilot(oldPilot, new PilotUpdateDto
                {
                    Id = id,
                    Name = editedPilot.Name,
                    Debutdate = editedPilot.DebutDate.ToString(),
                    Age = editedPilot.Age,
                    Team = editedPilot.Team
                });

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                return View();
            }
        }

        // GET: Pilot/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var pilot = _pilotRepository.GetPilot(id);
            return View(pilot);
        }

        // POST: Pilot/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _pilotRepository.DeletePilot(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                return View();
            }
        }
    }
}