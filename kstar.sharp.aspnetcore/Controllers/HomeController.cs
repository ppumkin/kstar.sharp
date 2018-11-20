using kstar.sharp.aspnetcore.Models;
using kstar.sharp.domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace kstar.sharp.aspnetcore.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {


            ViewBag.Results = "fix me";

            return View();
        }

        public ActionResult Graph()
        {
            return View();
        }



        public ContentResult GetData(int? historyHours)
        {
            DateTimeOffset start;

            if (!historyHours.HasValue)
                historyHours = -24;

            if (historyHours == 0)
                start = DateTimeOffset.MinValue;
            else
                start = DateTimeOffset.Now.AddHours(historyHours.Value);


            DateTimeOffset end = DateTimeOffset.Now;

            List<InverterDataGranular> vm = new List<InverterDataGranular>(); // _dbService.Get(start, end);

            //Have to use this in MVC now to work around large JSON data
            return new ContentResult()
            {
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(vm),
                ContentType = "application/json",
            };

        }

        public JsonResult GetLatest()
        {
            InverterDataGranular vm = new InverterDataGranular(); // _dbService.GetLatest();
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
