using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kstar.sharp.domain.Entities;
using kstar.sharp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kstar.sharp.aspnetcore.Controllers
{
    [Route("api/hourly")]
    [ApiController]
    public class HourlyApiController : ControllerBase
    {
        private readonly DbService dbService;

        public HourlyApiController(DbService dbService)
        {
            this.dbService = dbService;
        }

        // GET: api/hourly
        [HttpGet]
        public async Task<InverterDataGranular[]> Get()
        {
            DateTime start = DateTime.Now;
            start = start.Date + new TimeSpan(0, 0, 0);

            DateTime end = DateTime.Now;

            List<InverterDataGranular> vm = await dbService.Get(start, end);

            var groupedByHour = vm.GroupBy(
                time => time.RecordedDateTime.Hour,
                model => model,
                (key, m) => new { Hour = key, Entries = m });

            var groupedData = new InverterDataGranular[24];

            var placeHolderDate = start;

            for (int i = 0; i <= groupedData.Length - 1; i++)
            {
                placeHolderDate = start.Date + new TimeSpan(i, 0, 0);

                var pvMean = 0m;

                if (groupedByHour.Where(x => x.Hour == i).Any())
                {
                    var hourDateEntyFromList = groupedByHour.First(x => x.Hour == i);
                    pvMean = hourDateEntyFromList.Entries.Sum(x => x.PVPower) / hourDateEntyFromList.Entries.Count();
                }

                groupedData[i] = new InverterDataGranular()
                {
                    RecordedDateTime = placeHolderDate,
                    PVPower = pvMean
                };

            }

            return groupedData;
        }


    }
}
