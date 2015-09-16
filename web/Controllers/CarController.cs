using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Persons;
using Factories;
using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class CarController : Controller
    {
        private static readonly ICarRepository CarRepository = ServiceLocator.Get<CarRepository>();
        private static readonly CarFactory _carFactory = ServiceLocator.Get<CarFactory>();
        private static readonly IPilotRepository PilotRepository = ServiceLocator.Get<PilotRepository>();
        // GET: Car
        public ActionResult Index()
        {
            var list = CarRepository.GetAllCars();
            return View(list);
        }

        // GET: Car/Details/5
        public ActionResult Details(int id)
        {
            var car = CarRepository.GetCarDetailsWithPilotbyCarId(id);

            return View(car);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            var items = new List<SelectListItem>();

            //var pilots = PilotRepository.GetPilotForCarCreation();
           var pilots = PilotRepository.GetAllPilots();
            items.Add(new SelectListItem {Text = "no owner", Value = "0", Selected = true});
            foreach (var pilot in pilots)
            {
                items.Add(new SelectListItem {Text = pilot.Name, Value = pilot.Id.ToString()});
            }

            var carmodel = new CarModel(items);

            return View(carmodel);
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(CarModel car)
        {
            Pilot owner = null;

            try
            {
                // TODO: Add insert logic here
                if (car.PilotId != 0)
                {
                    owner = PilotRepository.GetPilot(car.PilotId);
                }

                var createdCar = _carFactory.CreateNewCar(car.TankVolume, car.Weight, car.HorsePowers, car.EngineType,
                    car.Name, car.AdditionalInfo, owner);
                CarRepository.Save(createdCar);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message + "\n" + ex.StackTrace);
            }
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Car/Edit/5
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

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Car/Delete/5
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