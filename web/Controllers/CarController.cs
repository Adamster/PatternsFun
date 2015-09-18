using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.CarTypes;
using Domain.Dto;
using Domain.Persons;
using Factories;
using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;
using Utils;
using Web.Models;

namespace Web.Controllers
{
    public class CarController : Controller
    {
        private readonly CarFactory CarFactory = ServiceLocator.Get<CarFactory>();
        private readonly ICarRepository CarRepository = ServiceLocator.Get<CarRepository>();
        private readonly IPilotRepository PilotRepository = ServiceLocator.Get<PilotRepository>();
        // GET: Car
        [HttpGet]
        public ActionResult Index()
        {
            var list = CarRepository.GetAllCars();

            return View(list);
        }

        // GET: Car/Details/5
        [HttpGet]
        public ActionResult Details(long id)
        {
            var car = CarRepository.GetEntityById<Car>(id);
            var model = new CarModel(car);
            return View(model);
        }

        // GET: Car/Create
        [HttpGet]
        public ActionResult Create()
        {
            var items = new List<SelectListItem>();

            var pilots = PilotRepository.GetPilotForCarCreation();

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

                var createdCar = CarFactory.CreateNewCar(car.TankVolume, car.Weight, car.HorsePowers, car.EngineType,
                    car.Name, car.AdditionalInfo, owner);
                CarRepository.Save(createdCar);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                return View(ex.Message + "\n" + ex.StackTrace);
            }
        }

        // GET: Car/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var car = CarRepository.GetEntityById<Car>(id);

            var items = new List<SelectListItem>();

            var pilots = PilotRepository.GetPilotForCarCreation();

            items.Add(new SelectListItem {Text = "no owner", Value = "0"});
            foreach (var pilot in pilots)
            {
                items.Add(new SelectListItem {Text = pilot.Name, Value = pilot.Id.ToString()});
            }
            var modelCar = new CarModel(car, items);
            return View(modelCar);
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CarModel carModel, string pilots)
        {
            try
            {
                var oldCar = CarRepository.GetEntityById<Car>(id);
                // TODO: Add update logic here
                CarRepository.UpdateCarInfo(oldCar, new CarUpdateDto
                {
                    Id = id,
                    Name = carModel.Name,
                    AdditionalInfo = carModel.AdditionalInfo,
                    Engine = new EngineUpdateDto(carModel.HorsePowers, carModel.EngineType),
                    Pilot = new PilotUpdateDto(PilotRepository.GetPilot(long.Parse(pilots))),
                    TankVolume = carModel.TankVolume,
                    Weight = carModel.Weight
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                return View(ex.Message);
            }
        }

        // GET: Car/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var car = CarRepository.GetEntityById<Car>(id);
            var carModel = new CarModel(car);
            return View(carModel);
        }

        // POST: Car/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                CarRepository.DeleteCar(id);
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