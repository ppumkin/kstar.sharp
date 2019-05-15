using kstar.sharp.aspnetcore.Models;
using kstar.sharp.domain.Entities;
using kstar.sharp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace kstar.sharp.aspnetcore.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbService _dbService;

        public HomeController(DbService dbService)
        {
            _dbService = dbService;
        }

        public ActionResult Index()
        {
            DateTimeOffset start = DateTimeOffset.Now.AddDays(-1).Date + new TimeSpan(0, 0, 0);
            DateTimeOffset end = DateTimeOffset.Now.AddDays(-1).Date + new TimeSpan(23, 59, 59);

            var results = _dbService.Get(start, end);

            ViewBag.Results = results.Count;

            return View();
        }

        public ActionResult Graph()
        {
            return View();
        }



        public JsonResult GetData(int? historyHours)
        {
            DateTimeOffset start;

            if (!historyHours.HasValue)
                historyHours = -24;

            if (historyHours == 0)
                start = DateTimeOffset.MinValue;
            else
                start = DateTimeOffset.Now.AddHours(historyHours.Value);


            DateTimeOffset end = DateTimeOffset.Now;

            List<InverterDataGranular> vm = _dbService.Get(start, end);

            return Json(vm);
            ////Have to use this in MVC now to work around large JSON data
            //return new ContentResult()
            //{
            //    Content = Newtonsoft.Json.JsonConvert.SerializeObject(vm),
            //    ContentType = "application/json",
            //};

        }

        public JsonResult GetLatest()
        {
            InverterDataGranular vm = _dbService.GetLatest();
            return Json(vm);
        }


        public ActionResult Details(int id)
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
