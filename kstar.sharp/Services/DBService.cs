using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kstar.sharp.domain.Extensions;

namespace kstar.sharp.Services
{
    public class DBService
    {
        public string DatabasePath
        {
            get
            {
                return sqlite.Repositories.SqLiteBaseRepository.DbFile;
            }
            set
            {
                sqlite.Repositories.SqLiteBaseRepository.DbFile = value;
            }
        }


        public DBService()
        {
            ///TODO: Another test for custom paths
            if (Tools.OSTools.IsLinux)
                DatabasePath = "/mnt/storage/SimpleDb.sqlite";
            else
                DatabasePath = @"C:\databases\SimpleDb.sqlite";


        }
         


        public List<kstar.sharp.domain.Entities.InverterDataGranular> Get(DateTimeOffset start, DateTimeOffset end)
        {
            var r = new sqlite.Repositories.InverterDataRepository();
            List<kstar.sharp.domain.Entities.InverterDataGranular> results = r.Get(start, end);

            return results;
        }

        public kstar.sharp.domain.Entities.InverterDataGranular GetLatest()
        {
            var r = new sqlite.Repositories.InverterDataRepository();
            kstar.sharp.domain.Entities.InverterDataGranular result = r.GetLatest();
            return result;
        }

        /// <summary>
        /// Parses HEXData and saves to Database. Returns Parsed Model for convieniece, eg UI display after save. NB. Not all data is saved
        /// </summary>
        /// <param name="HEXData"></param>
        public kstar.sharp.domain.Models.InverterData Save(kstar.sharp.domain.Models.InverterData inverDataModel)
        {
            var r = new sqlite.Repositories.InverterDataRepository();
            r.Add(inverDataModel.ToEntity());

            return inverDataModel;
        }



        /// <summary>
        /// Checks if database exists. If not then creates it. If database schema changes Saves will fail.
        /// </summary>
        public static void EnsureDatabase()
        {
            sqlite.Repositories.SqLiteBaseRepository.EnsureDatabase();
        }




    }
}
