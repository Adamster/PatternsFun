using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.CarTypes;
using Domain.Dto;
using Domain.Persons;
using Factories;
using InterfacesActions;
using Repository.Interfaces;
using Utils;
using Web.Models;

namespace Web.Controllers
{
    public class CarController : Controller
    {
        private readonly CarFactory _carFactory;
        private readonly ICarRepository _carRepository;
        private readonly IPilotRepository _pilotRepository;

        [Obsolete]
        public CarController()
        {
        }

        public CarController(ICarRepository carRepository, IPilotRepository pilotRepository,
            ICarActionOnCreation carActionOnCreation, IElectroCarActionOnCreation electroCarAction)
        {
            _carFactory = new CarFactory(carActionOnCreation, electroCarAction);
            _carRepository = carRepository;
            _pilotRepository = pilotRepository;
        }


        // GET: Car
        [HttpGet]
        public ActionResult Index()
        {
            var list = _carRepository.GetAllCars();
            Logger.AddMsgToLog("Car Index requested");
            return View(list);
        }

        // GET: Car/Details/5
        [HttpGet]
        public ActionResult Details(long id)
        {
            var car = _carRepository.GetEntityById<Car>(id);
            var model = new CarModel(car);
            Logger.AddMsgToLog("Details on car with id:" + id + " requested");
            return PartialView(model);
        }

        // GET: Car/Create
        [HttpGet]
        public ActionResult Create()
        {
            var items = new List<SelectListItem>();

            var pilots = _pilotRepository.GetPilotForCarCreation();

            items.Add(new SelectListItem {Text = "no owner", Value = "0", Selected = true});
            foreach (var pilot in pilots)
            {
                items.Add(new SelectListItem {Text = pilot.Name, Value = pilot.Id.ToString()});
            }

            var carmodel = new CarModel(items);
            Logger.AddMsgToLog("Create form opened");
            return PartialView(carmodel);
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(CarModel car)
        {
            Pilot owner = null;

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    if (car.PilotId != 0)
                    {
                        owner = _pilotRepository.GetPilot(car.PilotId);
                    }

                    Car createdCar = null;
                    if (!car.IsSportCar)
                    {
                        createdCar = _carFactory.CreateNewCar(car.TankVolume, car.Weight, car.HorsePowers,
                            car.EngineType,
                            car.Name, car.AdditionalInfo, owner);
                    }
                    else
                    {
                        createdCar = _carFactory.CreateNewSportCar(car.TankVolume, car.Weight, car.HorsePowers,
                            car.EngineType,
                            car.Name, car.AdditionalInfo, owner);
                    }


                    _carRepository.Save(createdCar);
                    Logger.AddMsgToLog("Car created and saved Name: " + createdCar.Name);
                    return PartialView("CarTable", _carRepository.GetAllCars());
                }
                catch (Exception ex)
                {
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return View(ex.Message + "\n" + ex.StackTrace);
                }
            }
            return PartialView(car);
        }

        // GET: Car/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var car = _carRepository.GetEntityById<Car>(id);

            var items = new List<SelectListItem>();

            var pilots = _pilotRepository.GetPilotForCarCreation();

            items.Add(new SelectListItem {Text = "no owner", Value = "0"});
            foreach (var pilot in pilots)
            {

                if (car.OwnerPilot != null && pilot.Name == car.OwnerPilot.Name)
                {
                    var item = new SelectListItem {Text = pilot.Name, Value = pilot.Id.ToString(), Selected = true};
                    if (!items.Contains(item))
                    {
                        items.Add(item);
                    }
                   
                }
                else  items.Add(new SelectListItem {Text = pilot.Name, Value = pilot.Id.ToString()});
            }
            var modelCar = new CarModel(car, items);

            Logger.AddMsgToLog("Car requested to edit with Name: " + car.Name);
            return PartialView(modelCar);
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CarModel carModel, string pilots)
        {
            Pilot owner = null;
            try
            {
                if (pilots != "0")
                {
                    owner = _pilotRepository.GetPilot(long.Parse(pilots));
                }
                var oldCar = _carRepository.GetEntityById<Car>(id);
                // TODO: Add update logic here
                _carRepository.UpdateCarInfo(oldCar, new CarUpdateDto
                {
                    Id = id,
                    Name = carModel.Name,
                    AdditionalInfo = carModel.AdditionalInfo,
                    Engine = new EngineUpdateDto(carModel.HorsePowers, carModel.EngineType),
                    Pilot = owner,
                    TankVolume = carModel.TankVolume,
                    Weight = carModel.Weight
                });
                //return RedirectToAction("Index");
                Logger.AddMsgToLog("Car edited");
                return PartialView("CarTable", _carRepository.GetAllCars());
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
            var car = _carRepository.GetEntityById<Car>(id);
            var carModel = new CarModel(car);
            Logger.AddMsgToLog("Car delete request with name\nName: " + car.Name);
            return PartialView(carModel);
        }

        // POST: Car/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _carRepository.DeleteCar(id);
                Logger.AddMsgToLog("Car deleted");
                return PartialView("CarTable", _carRepository.GetAllCars());
            }
            catch (Exception ex)
            {
                Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                return View();
            }
        }
    }
}