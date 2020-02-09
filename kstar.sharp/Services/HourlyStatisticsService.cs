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
    public class HourlyStatisticsService
    {
        /// <summary>
        /// Base kWh calcualtion based that there is one entry every 30 seconds
        /// </summary>
        public const decimal ThirtySecondFraction = 0.00833333m; 

        public List<HourlyStatistic> GetHourly(List<InverterDataGranular> data, decimal intervalFraction)// DateTime start, DateTime end)//List<InverterDataGranular> vm)
        {
            var start = data.First().RecordedDateTime;

            var groupedByHour = data.GroupBy(
              time => time.RecordedDateTime.Hour,
              model => model,
              (key, m) => new { Hour = key, Entries = m });


            var listByHour = new List<HourlyStatistic>();

            foreach (var item in groupedByHour)
            {
                var hs = new HourlyStatistic
                {
                    Hour = start.Date + new TimeSpan(item.Hour, 0, 0),
                    Production = item.Entries.Sum(e => CalculateKwUsage(e.PVPower, intervalFraction)),
                    Consumption = item.Entries.Sum(e => CalculateKwUsage(e.LoadPower, intervalFraction))
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
