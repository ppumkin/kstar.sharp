using Dapper;
using Mono.Data.Sqlite;
using System;
using System.IO;

namespace kstar.sharp.sqlite.Repositories
{
    public class SqLiteBaseRepository
    {

        private static string _dbFile;
        public static string DbFile
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_dbFile))
                    return Environment.CurrentDirectory + "/SimpleDb.sqlite";
                else
                    return _dbFile;
            }
            set
            {
                _dbFile = value;
            }
        }


        public static SqliteConnection SimpleDbConnection()
        {
            var dbConn = new SqliteConnection("Data Source=" + DbFile);
            return dbConn;
        }




        public static void EnsureDatabase()
        {

            if (!File.Exists(DbFile))
            {
                using (var db = SimpleDbConnection())
                    CreateDatabase(db);
            }
        }


        private static void CreateDatabase(SqliteConnection cnn)
        {
            cnn.Open();
            cnn.Execute(
                @"
CREATE TABLE `InverterDataGranular` (
    `Id`                INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`RecordedDateTime`	TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`TempCelcius`	    INTEGER,
	`ETotal`	        INTEGER,
	`EToday`	        INTEGER,
	`PVPower`	        REAL,
	`PV1Volt`	        INTEGER,
	`PV2Volt`	        INTEGER,
	`LoadPower`	        REAL,
	`Bat1Charge`	    INTEGER,
	`Bat1Power`	        REAL,
	`Bat1Voltage`	    REAL,
	`Bat1Amp`	        REAL,
	`GridPower`	        REAL,
	PRIMARY KEY(Id)
);
");

            cnn.Execute(
                @"
CREATE TABLE `InverterDataLatest` (
	`RecordedDateTime`	TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`TempCelcius`	    INTEGER,
	`ETotal`	        INTEGER,
	`EToday`	        INTEGER,
	`PVPower`	        REAL,
	`PV1Volt`	        INTEGER,
	`PV2Volt`	        INTEGER,
	`LoadPower`	        REAL,
	`Bat1Charge`	    INTEGER,
	`Bat1Power`	        REAL,
	`Bat1Voltage`	    REAL,
	`Bat1Amp`	        REAL,
	`GridPower`	        REAL,
	PRIMARY KEY(RecordedDateTime)
);
");

            //CREATE TABLE [InverterData] (
            //  [TimeStamp] datetime DEFAULT CURRENT_TIMESTAMP NOT NULL
            //, [Battery1Power] float NOT NULL
            //, [Battery1Mode] int NOT NULL
            //, CONSTRAINT [PK_InverterData] PRIMARY KEY ([TimeStamp])

            cnn.Close();
        }


    }
}
