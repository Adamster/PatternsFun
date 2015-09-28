using System;
using System.Linq;
using System.Web.Mvc;
using Repository.Interfaces;

namespace Web.Controllers
{
    public class GridController : Controller
    {
        private readonly IPilotRepository _pilotRepository;
        private readonly ICarRepository _carRepository;

        public GridController(IPilotRepository pilotRepository, ICarRepository carRepository)
        {
            _pilotRepository = pilotRepository;
            _carRepository = carRepository;
        }

        // GET: Grid
        [HttpGet]
        public JsonResult PilotGridData(JqSettings settings)
        {
            var skip = settings.rows*(settings.page - 1);
            var dbData = _pilotRepository.GetAllPilots();
            var filteredDbData = dbData.Skip(skip).Take(settings.rows);
            var count = dbData.Count();


            var gridData = from record in filteredDbData
                select new
                {
                    cell = new object[]
                    {
                        record.Name,
                        record.Age,
                        record.Team,
                        record.DebutDate
                    }
                };
            var jsonData = new
            {
                total = (int) Math.Ceiling((double) count/settings.rows), //totalPages
                settings.page,
                records = count,
                rows = gridData
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ViewResult PilotGridView()
        {
            return View();
        }

        public class JqSettings
        {
            public int q { get; set; }
            public bool _search { get; set; }
            public int rows { get; set; }
            public int page { get; set; }
        }

        public JsonResult CarGridData(JqSettings settings)
        {
            var skip = settings.rows * (settings.page - 1);
            var dbData = _carRepository.GetAllCars();
            var filteredDbData = dbData.Skip(skip).Take(settings.rows);
            var count = dbData.Count();


            var gridData = from record in filteredDbData
                           select new
                           {
                               cell = new object[]
                    {
                        record.Version,
                        record.Name,
                        record.FuelTank,
                        record.AdditionalInfo
                    }
                           };
            var jsonData = new
            {
                total = (int)Math.Ceiling((double)count / settings.rows), //totalPages
                settings.page,
                records = count,
                rows = gridData
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ViewResult CarGridView()
        {
            return View();
        }

    }

}