using System;
using System.Threading.Tasks;
using kstar.sharp.aspnetcore.Models;
using kstar.sharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace kstar.sharp.aspnetcore.Controllers
{
    [Route("api/hourly")]
    [ApiController]
    public partial class HourlyApiController : ControllerBase
    {
        private readonly AnalyticsService _analyticsService;

        public HourlyApiController(AnalyticsService  analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [Route("")]
        [HttpGet]
        public async Task<HoursStatViewModel> Get()
        {
            //DateTime start = DateTime.Now;
            //start = start.Date + new TimeSpan(0, 0, 0);
            //DateTime end = DateTime.Now;

            DateTime start = new DateTime(2020, 01, 19);
            start = start.Date + new TimeSpan(0, 0, 0);

            DateTime end = new DateTime(2020, 01, 19);
            end = end.Date + new TimeSpan(23, 59, 59);

            var result = await _analyticsService.GetHourly(start, end);

            return new HoursStatViewModel() { HourlyStats = result };


            //List<InverterDataGranular> vm = await dbService.Get(start, end);

            //var groupedByHour = vm.GroupBy(
            //    time => time.RecordedDateTime.Hour,
            //    model => model,
            //    (key, m) => new { Hour = key, Entries = m });

            //var groupedData = new InverterDataGranular[24];

            //var placeHolderDate = start;

            //for (int i = 0; i <= groupedData.Length - 1; i++)
            //{
            //    placeHolderDate = start.Date + new TimeSpan(i, 0, 0);

            //    var pvMean = 0m;

            //    if (groupedByHour.Where(x => x.Hour == i).Any())
            //    {
            //        var hourDateEntyFromList = groupedByHour.First(x => x.Hour == i);
            //        pvMean = hourDateEntyFromList.Entries.Sum(x => x.PVPower) / hourDateEntyFromList.Entries.Count();
            //    }

            //    groupedData[i] = new InverterDataGranular()
            //    {
            //        RecordedDateTime = placeHolderDate,
            //        PVPower = pvMean
            //    };

            //}

            //return groupedData;
        }
    }
}
