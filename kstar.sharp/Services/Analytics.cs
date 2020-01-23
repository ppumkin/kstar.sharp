using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kstar.sharp.domain.Entities;
using kstar.sharp.Models;
using kstar.sharp.Type;

namespace kstar.sharp.Services
{
    public class AnalyticsService
    {
        public const decimal ThirtySecondFraction = 0.00833333m;

        private readonly DbService _dbService;

        public AnalyticsService(DbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<List<HourlyStatistic>> GetHourly(DateTime start, DateTime end)//List<InverterDataGranular> vm)
        {
            var results = await _dbService.Get(start, end);

            var groupedByHour = results.GroupBy(
              time => time.RecordedDateTime.Hour,
              model => model,
              (key, m) => new { Hour = key, Entries = m });


            var listByHour = new List<HourlyStatistic>();

            foreach (var item in groupedByHour)
            {
                var hs = new HourlyStatistic
                {
                    Hour = start.Date + new TimeSpan(item.Hour, 0, 0),
                    Production = item.Entries.Sum(e => CalculateKwUsage(e.PVPower, ThirtySecondFraction)),
                    Consumption = item.Entries.Sum(e => CalculateKwUsage(e.LoadPower, ThirtySecondFraction))
                };

                listByHour.Add(hs);
            }

            return listByHour;
        }


        private KiloWattHour CalculateKwUsage(decimal PowerWatts, decimal time)
        {
            return decimal.Divide(decimal.Multiply(PowerWatts, time), 1000);
        }
    }
}
