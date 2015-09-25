using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using Repository.Interfaces;

namespace Web.Controllers
{
    public class GridController : Controller
    {
        private readonly IPilotRepository _pilotRepository;
        public GridController(IPilotRepository pilotRepository)
        {
            _pilotRepository = pilotRepository;
        }

        public class JqSettings
        {
            public int q { get; set; }
            public bool _search { get; set; }
            public int rows { get; set; }
            public int page { get; set; }
        }

        // GET: Grid
        [HttpGet]
        public JsonResult Index(JqSettings settings)
        {
            int skip = settings.rows * (settings.page - 1);
            var dbData = _pilotRepository.GetAllPilots();
            var filteredDbData = dbData.Skip(skip).Take(settings.rows);
            int count = dbData.Count();
           
           
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
                total = (int)Math.Ceiling((double)count / settings.rows), //totalPages
                page = settings.page,
                records = count,
                rows = gridData
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index1()
        {
            return View();
        }
    }
}