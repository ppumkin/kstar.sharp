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

        public async Task<List<domain.Entities.InverterDataGranular>> Get(DateTimeOffset start, DateTimeOffset end)
        {
            var results = await db.InverterDataGranular.Take(10).ToListAsync();

            return results;
        }

        public async Task<domain.Entities.InverterDataGranular> GetLatest()
        {
            var result = await db.InverterDataGranular.LastAsync();

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
