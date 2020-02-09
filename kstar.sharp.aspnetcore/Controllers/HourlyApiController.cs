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

        public HourlyApiController(AnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [Route("")]
        [HttpGet]
        public async Task<HoursStatViewModel> Get(int dayOffset = 0)
        {
            DateTime start = DateTime.Now;
            start = start.Date + new TimeSpan(0, 0, 0);

            if (dayOffset < 0)
                start = start.AddDays(dayOffset);

            DateTime end = start.Date + new TimeSpan(23, 59, 59);


            var vm = await _analyticsService.GetInverterViewModel(start, end);

            return vm; 
        }
    }
}
