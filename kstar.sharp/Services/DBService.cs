using kstar.sharp.domain.Extensions;
using kstar.sharp.ef;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kstar.sharp.Services
{
    public class DbService
    {
        private InverterDataContext db;

        public DbService(InverterDataContext inverterDataContext)
        {
            db = inverterDataContext;
        }

        public async Task<List<domain.Entities.InverterDataGranular>> Get(DateTime start, DateTime end)
        {
            var results = await db.InverterDataGranular
                .Where(d => d.RecordedDateTime > start && d.RecordedDateTime <= end)
                .ToListAsync();

            return results;
        }

        public async Task<domain.Entities.InverterDataGranular> GetLatest()
        {
            //This is very slow for some reason
            //var result = await db.InverterDataGranular.LastAsync();

            var start = DateTime.Now.AddMinutes(-5);
            var end = DateTime.Now;

            var results = await Get(start, end);

            var result = results.OrderByDescending(x => x.RecordedDateTime).FirstOrDefault();

            if (result is null)
                return new domain.Entities.InverterDataGranular();
            else
                return result;
        }

        /// <summary>
        /// Save a parsed and valid entity directly
        /// </summary>
        /// <param name="inverterDataGranular"></param>
        public async Task Save(domain.Entities.InverterDataGranular inverterDataGranular)
        {
            await db.InverterDataGranular.AddAsync(inverterDataGranular);
            db.SaveChanges();
        }

        /// <summary>
        /// Parses HEXData and saves to Database. Returns Parsed Model for convieniece, eg UI display after save. NB. Not all data is saved
        /// </summary>
        /// <param name="HEXData"></param>
        public kstar.sharp.domain.Models.InverterData Save(kstar.sharp.domain.Models.InverterData inverDataModel)
        {
            db.InverterDataGranular.Add(inverDataModel.ToEntity());
            db.SaveChanges();

            return inverDataModel;
        }

    }
}
