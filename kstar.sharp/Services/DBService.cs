using kstar.sharp.domain.Extensions;
using kstar.sharp.ef;
using System;
using System.Collections.Generic;
using System.Linq;

namespace kstar.sharp.Services
{
    public class DbService
    {
        private InverterDataContext db;

        public DbService(InverterDataContext inverterDataContext)
        {
            db = inverterDataContext;
        }

        public List<kstar.sharp.domain.Entities.InverterDataGranular> Get(DateTimeOffset start, DateTimeOffset end)
        {
            var results = db.InverterDataGranular.Take(10).ToList();

            return results;
        }

        public kstar.sharp.domain.Entities.InverterDataGranular GetLatest()
        {
            var result = db.InverterDataGranular.Last();

            return result;
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
