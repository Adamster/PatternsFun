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
    public class SportCarController : Controller
    {
        //
        // GET: /SportCar/
        internal static ICarRepository CarRepository = ServiceLocator.Get<CarRepository>();
        public ActionResult Index()
        {
           var sportCars = CarRepository.GetAllSportCars();
           return View(sportCars);
        }

        //
        // GET: /SportCar/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SportCar/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SportCar/Create

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
        // GET: /SportCar/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SportCar/Edit/5

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
        // GET: /SportCar/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SportCar/Delete/5

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
