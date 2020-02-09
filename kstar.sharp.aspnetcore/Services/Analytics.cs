using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kstar.sharp.aspnetcore.Models;
using kstar.sharp.domain.Entities;
using kstar.sharp.Models;
using kstar.sharp.Type;

namespace kstar.sharp.Services
{
    public class AnalyticsService
    {
        private readonly DbService _dbService;
        private readonly HourlyStatisticsService _hourlyStatisticsService;

        public AnalyticsService(DbService dbService, HourlyStatisticsService hourlyStatisticsService)
        {
            _dbService = dbService;
            _hourlyStatisticsService = hourlyStatisticsService;
        }

        public async Task<HoursStatViewModel> GetInverterViewModel(DateTime start, DateTime end)
        {
            var inverterData = await _dbService.Get(start, end);

            var result = _hourlyStatisticsService.GetHourly(inverterData, HourlyStatisticsService.ThirtySecondFraction);

            for (int i = 0; i <= 23; i++)
            {
                var timeSlot = result.FirstOrDefault(t => t.Hour.Hour == i);
                if (timeSlot is (HourlyStatistic)default)
                {
                    result.Add(new HourlyStatistic() { Hour = start.Date + new TimeSpan(i, 0, 0), Consumption = 0, Production = 0 });
                }
            }

            return new HoursStatViewModel()
            {
                HourlyStats = result.OrderBy(e => e.Hour.Hour).ToList(),
                Latest = inverterData.Last()
            };
        }
     
    }
}
