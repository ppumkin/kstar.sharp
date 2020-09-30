using kstar.sharp.domain.Entities;
using kstar.sharp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kstar.sharp.aspnetcore.Controllers
{

    [Route("api/data")]
    public class DataApiController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly DbService dbService;
        const string CacheKey_LatestData = "GetDataLatest";

        public DataApiController(IMemoryCache cache, DbService dbService)
        {
            this.cache = cache;
            this.dbService = dbService;
        }


        [HttpGet, Route("{historyHours:int?}")]
        public async Task<IActionResult> GetData(int? historyHours)
        {
            if (historyHours.HasValue)
            {
                DateTime start = DateTime.Now.AddHours(historyHours.Value);

                DateTime end = DateTime.Now;

                List<InverterDataGranular> vm = await dbService.Get(start, end);

                return Json(vm);
            }
            else
            {
                if (cache.TryGetValue(CacheKey_LatestData, out InverterDataGranular cachedResult))
                {
                    return Json(cachedResult);
                }

                InverterDataGranular latestResult = await dbService.GetLatest();

                cache.Set(CacheKey_LatestData, latestResult, TimeSpan.FromSeconds(25));

                return Json(latestResult);
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
