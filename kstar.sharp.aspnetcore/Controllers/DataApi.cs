using kstar.sharp.domain.Entities;
using kstar.sharp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kstar.sharp.aspnetcore.Controllers
{

    [Route("api/data")]
    public class DataApiController : Controller
    {
        private readonly DbService dbService;

        public DataApiController(DbService dbService)
        {
            this.dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int? historyHours)
        {
            if (historyHours.HasValue)
            {
                DateTimeOffset start;

                if (!historyHours.HasValue)
                    historyHours = -24;

                if (historyHours == 0)
                    start = DateTimeOffset.MinValue;
                else
                    start = DateTimeOffset.Now.AddHours(historyHours.Value);


                DateTimeOffset end = DateTimeOffset.Now;

                List<InverterDataGranular> vm = await dbService.Get(start, end);

                return Json(vm);
            }
            else
            {
                InverterDataGranular vm = await dbService.GetLatest();
                return Json(vm);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(InverterDataGranular inverterDataGranular)
        {
            await dbService.Save(inverterDataGranular);
            return Ok();
        }



    }
}
