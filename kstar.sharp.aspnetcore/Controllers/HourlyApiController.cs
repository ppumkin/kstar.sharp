using System;
using System.Linq;
using System.Threading.Tasks;
using kstar.sharp.aspnetcore.Models;
using kstar.sharp.Models;
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
        public async Task<HoursStatViewModel> Get()
        {
            DateTime start = DateTime.Now;
            start = start.Date + new TimeSpan(0, 0, 0);

            DateTime end = DateTime.Now;
            end = end.Date + new TimeSpan(23, 59, 59);

            var vm = await _analyticsService.GetInverterViewModel(start, end);

            return vm; 
        }
    }
}
