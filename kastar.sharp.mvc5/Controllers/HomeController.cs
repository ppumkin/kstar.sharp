using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using kstar.sharp.Services;
using kstar.sharp.domain.Entities;

namespace kastar.sharp.mvc4.Controllers
{
    public class HomeController : Controller
    {


        //
        // GET: /Home/
        public ActionResult Index()
        {

            kstar.sharp.Services.DBService db = new DBService();
            ViewBag.DBPath = db.DatabasePath;

            return View();
        }

        //
        // GET: /Home/Details/5

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

            DBService db = new DBService();
            List<InverterDataGranular> vm = db.Get(start, end);

            //Have to use this in MVC now to work around large JSON data
            return new ContentResult()
            {
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(vm),
                ContentType = "application/json",
            };

        }

        public JsonResult GetLatest()
        {
            DBService db = new DBService();
            InverterDataGranular vm = db.GetLatest();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(int id)
        {
            return View();
        }


    }
}
