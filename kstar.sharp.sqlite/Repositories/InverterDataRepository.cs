using kstar.sharp.sqlite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

using kstar.sharp.domain.Entities;


namespace kstar.sharp.sqlite.Repositories
{
    public class InverterDataRepository : IInverterDataRepository
    {

        public List<InverterDataGranular> Get(DateTimeOffset startDateTime, DateTimeOffset endDateTime)
        {
            IEnumerable<InverterDataGranular> results = new List<InverterDataGranular>();

            using (var db = SqLiteBaseRepository.SimpleDbConnection())
            {
                db.Open();
                results = db.Query<InverterDataGranular>(@"
SELECT * FROM 
    InverterDataGranular
WHERE
    RecordedDateTime >= @start AND RecordedDateTime <= @end"
                    , new { start = startDateTime.UtcDateTime, end = endDateTime.UtcDateTime });

                db.Close();
            }

            return results.ToList();
        }

        /// <summary>
        /// Get the latest inverter data reading. Usefull for "live" information
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public InverterDataGranular GetLatest()
        {
            InverterDataGranular result = new InverterDataGranular();

            using (var db = SqLiteBaseRepository.SimpleDbConnection())
            {
                db.Open();
                result = db.Query<InverterDataGranular>(@"SELECT * FROM InverterDataLatest").FirstOrDefault();

                db.Close();
            }

            return result;
        }




        /// <summary>
        /// Add a record into the Granular table
        /// </summary>
        /// <param name="inverterData"></param>
        public void Add(InverterDataGranular inverterData)
        {

            using (var db = SqLiteBaseRepository.SimpleDbConnection())
            {
                db.Open();
                inverterData.RecordedDateTime = DateTime.Now;
                string sqlQuery = @"
INSERT INTO 
    [InverterDataGranular]([TempCelcius],[ETotal],[EToday],[PVPower],[PV1Volt],[PV2Volt],[LoadPower],[Bat1Charge],[Bat1Power],[Bat1Voltage],[Bat1Amp],[GridPower]) 
VALUES 
                          (@TempCelcius, @ETotal, @EToday, @PVPower, @PV1Volt, @PV2Volt, @LoadPower, @Bat1Charge, @Bat1Power, @Bat1Voltage, @Bat1Amp, @GridPower)";
                db.Execute(sqlQuery, inverterData);

                //clear the latest table
                sqlQuery = "DELETE FROM InverterDataLatest";
                db.Execute(sqlQuery);

                //insert new entry into latest
                sqlQuery = @"
INSERT INTO 
    [InverterDataLatest]([TempCelcius],[ETotal],[EToday],[PVPower],[PV1Volt],[PV2Volt],[LoadPower],[Bat1Charge],[Bat1Power],[Bat1Voltage],[Bat1Amp],[GridPower]) 
VALUES 
                          (@TempCelcius, @ETotal, @EToday, @PVPower, @PV1Volt, @PV2Volt, @LoadPower, @Bat1Charge, @Bat1Power, @Bat1Voltage, @Bat1Amp, @GridPower)";
                db.Execute(sqlQuery, inverterData);

                db.Close();
            }


        }
    }
}

