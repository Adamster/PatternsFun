﻿using System.Web.Mvc;
using Domain.Dto;
using Factories;
using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;
using Web.Models;

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
            var modelPilot = new PilotModel();
            modelPilot.Name = oldPilot.Name;
            modelPilot.Age = oldPilot.Age;
            modelPilot.DebutDate = oldPilot.DebutDate;
            modelPilot.Team = oldPilot.Team;
            return View(modelPilot);
        }

        // POST: Pilot/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PilotModel editedPilot)
        {
            try
            {
                 var oldPilot = PilotRepository.GetPilot(id);
                PilotRepository.UpdatePilot(oldPilot, new PilotUpdateDto
                {
                    Id = id,
                    Name = editedPilot.Name,
                    Debutdate = editedPilot.DebutDate.ToString(),
                    Age = editedPilot.Age,
                    Team = editedPilot.Team
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