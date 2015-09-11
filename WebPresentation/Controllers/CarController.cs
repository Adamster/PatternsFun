using Domain.CarTypes;
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
    public class CarController : Controller
    {
        //
        // GET: /Car/
        internal static ICarRepository CarRepository = ServiceLocator.Get<CarRepository>();

        public ActionResult Index()
        {
            var cars = CarRepository.GetAllCars();
            return View(cars);
        }

        //
        // GET: /Car/Details/5

        public ActionResult Details(int id)
        {
            var carsDetails = CarRepository.GetCarDetails(id);
            return View(carsDetails.First());
        }

        //
        // GET: /Car/Create
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        //
        // POST: /Car/Create

        [HttpPost]
        public ActionResult Create(Car car)
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
        // GET: /Car/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Car/Edit/5

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
        // GET: /Car/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Car/Delete/5

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
