using kstar.sharp.aspnetcore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace kstar.sharp.aspnetcore.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Graph()
        {
            return View();
        }

        public ActionResult Dash()
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
