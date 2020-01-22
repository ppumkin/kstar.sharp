using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using kstar.sharp.domain.Entities;
using kstar.sharp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        [Route("")]
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



        [Route("consumption")]
        [HttpGet]
        public async Task<HoursStatViewModel> GetDailyStatsByHour()
        {
            DateTime start = new DateTime(2020, 01, 19);
            start = start.Date + new TimeSpan(0, 0, 0);

            DateTime end = new DateTime(2020, 01, 19);
            end = end.Date + new TimeSpan(23, 59, 59);

            List<InverterDataGranular> vm = await dbService.Get(start, end);

            var result = CalcuHourly(vm);

            return new HoursStatViewModel() { HourlyStats = result };
        }




        public class HoursStatViewModel
        {
            public List<HourlyStat> HourlyStats { get; set; }
            public KiloWattHour TotalProduction => HourlyStats.Sum(e => e.Production);
            public KiloWattHour TotalConsumption => HourlyStats.Sum(e => e.Consumption);
        }


        public class HourlyStat
        {
            public int Hour { get; set; }
            public KiloWattHour Production { get; set; }
            public KiloWattHour Consumption { get; set; }
        }

        //[JsonObject(MemberSerialization.OptIn)]
        //[TypeConverter(typeof(DecimalConverter))]
        public class KiloWattHour
        {
            //[JsonProperty(IsReference = true, PropertyName = "")]
            public decimal Value => _value;
            //public decimal _value;
            //public decimal Value => _value;
            private readonly decimal _value;

            public KiloWattHour(decimal value)
            {
                _value = value;
            }

            public static implicit operator decimal(KiloWattHour d) => d._value;
            public static implicit operator KiloWattHour(decimal kwh) => new KiloWattHour(kwh);


            //public static explicit operator KiloWattHour(decimal d) => new KiloWattHour(d); 
            //public static explicit operator decimal(KiloWattHour kwh)  => kwh._value;

            //public static implicit operator JToken(KiloWattHour d) => d._value;

            public override string ToString() => $"{_value:n2}kWh";
        }

        private List<HourlyStat> CalcuHourly(List<InverterDataGranular> vm)
        {
            var groupedByHour = vm.GroupBy(
              time => time.RecordedDateTime.Hour,
              model => model,
              (key, m) => new { Hour = key, Entries = m });


            var listByHour = new List<HourlyStat>();

            foreach (var item in groupedByHour)
            {
                var hs = new HourlyStat
                {
                    Hour = item.Hour,
                    Production = item.Entries.Sum(e => CalculateKwUsage(e.PVPower, ThirdySecondFraction)),
                    Consumption = item.Entries.Sum(e => CalculateKwUsage(e.LoadPower, ThirdySecondFraction))
                };

                listByHour.Add(hs);
            }

            return listByHour;
        }

        public const decimal ThirdySecondFraction = 0.00833333m;

        private KiloWattHour CalculateKwUsage(decimal PowerWatts, decimal time)
        {
            return decimal.Divide(decimal.Multiply(PowerWatts, time), 1000);
        }
    }
}
