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

        //public string DatabasePath
        //{
        //    get
        //    {
        //        return sqlite.Repositories.SqLiteBaseRepository.DbFile;
        //    }
        //    set
        //    {
        //        sqlite.Repositories.SqLiteBaseRepository.DbFile = value;
        //    }
        //}


        //public DBService()
        //{
        //    ///TODO: Another test for custom paths
        //    if (Tools.OSTools.IsLinux)
        //        DatabasePath = "/mnt/storage/SimpleDb.sqlite";
        //    else
        //        DatabasePath = @"C:\databases\SimpleDb.sqlite";
        //}



        public List<kstar.sharp.domain.Entities.InverterDataGranular> Get(DateTimeOffset start, DateTimeOffset end)
        {
            //var r = new sqlite.Repositories.InverterDataRepository();
            //List<kstar.sharp.domain.Entities.InverterDataGranular> results = r.Get(start, end);

            var results = db.InverterDataGranular.Take(10).ToList();

            return results;
        }

        public kstar.sharp.domain.Entities.InverterDataGranular GetLatest()
        {
            //var r = new sqlite.Repositories.InverterDataRepository();
            //kstar.sharp.domain.Entities.InverterDataGranular result = r.GetLatest();

            var result = db.InverterDataGranular.Last();

            return result;
        }

        /// <summary>
        /// Parses HEXData and saves to Database. Returns Parsed Model for convieniece, eg UI display after save. NB. Not all data is saved
        /// </summary>
        /// <param name="HEXData"></param>
        public kstar.sharp.domain.Models.InverterData Save(kstar.sharp.domain.Models.InverterData inverDataModel)
        {
            //var r = new sqlite.Repositories.InverterDataRepository();

            db.InverterDataGranular.Add(inverDataModel.ToEntity());
            db.SaveChanges();

            return inverDataModel;
        }



        ///// <summary>
        ///// Checks if database exists. If not then creates it. If database schema changes Saves will fail.
        ///// </summary>
        //public static void EnsureDatabase()
        //{
        //    sqlite.Repositories.SqLiteBaseRepository.EnsureDatabase();
        //}




    }
}
