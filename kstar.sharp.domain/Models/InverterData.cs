using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kstar.sharp.domain.Models
{
    public class InverterData : InverterDataBase
    {
        public DateTime RecordDateTime { get; set; }

        public BatteryData BatteryData { get; set; }
        public GridData GridData { get; set; }
        public LoadData LoadData { get; set; }
        public PhotoVoltaicData PVData { get; set; }
        public StatisticsData StatData { get; set; }


        public InverterData(string[] HEXData)
            : base(HEXData)
        {
            BatteryData = new BatteryData(HEXData);
            GridData = new GridData(HEXData);
            LoadData = new LoadData(HEXData);
            PVData = new PhotoVoltaicData(HEXData);
            StatData = new StatisticsData(HEXData);

            //RecordDateTime = DateTime.Now; //Not used to save. DB logic will use database timestamp and returen actuall saved time in database
        }


        //public InverterData(sqlite.Entities.InverterDataGranular dbData)
        //{
        //    RecordDateTime = dbData.RecordedDateTime;

        //    BatteryData bd = new BatteryData()
        //    {
        //        Battery1Amp = dbData.Bat1Amp,
        //        Battery1Volt = dbData.Bat1Voltage
        //    };
           


        //}


      

    }
}
