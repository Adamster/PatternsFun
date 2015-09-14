using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factories;
using Infrastrucuture.IoC;
using Repository;
namespace Web.Controllers
{
    public class CarController : Controller
    {
        private static ICarRepository CarRepository = ServiceLocator.Get<CarRepository>();
        private static CarFactory _carFactory = ServiceLocator.Get<CarFactory>();
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
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

             //   _carFactory.CreateNewCar(fuelTankVolume, weight, horsePower, engineType, name, null, null)

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
