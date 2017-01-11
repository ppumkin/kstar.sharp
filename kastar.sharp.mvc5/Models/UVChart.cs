//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace kastar.sharp.mvc4.Models
//{
//    public class UVChart
//    {

//        public List<UVCHartDataSetInt> Bat1Charge { get; set; }
//        public List<UVCHartDataSetInt> PV2Volt { get; set; }
//        public List<UVCHartDataSetInt> PV1Volt { get; set; }

//        public List<UVCHartDataSetDecimal> ETotal { get; set; }
//        public List<UVCHartDataSetDecimal> EToday { get; set; }
//        public List<UVCHartDataSetDecimal> PVPower { get; set; }
//        public List<UVCHartDataSetDecimal> LoadPower { get; set; }
//        public List<UVCHartDataSetDecimal> Bat1Power { get; set; }
//        public List<UVCHartDataSetDecimal> Bat1Voltage { get; set; }
//        public List<UVCHartDataSetDecimal> GridPower { get; set; }


        
//        public UVChart(List<kstar.sharp.sqlite.Entities.InverterDataGranular> dbData) {
//            //2016-07-21 15:00:00
//            Bat1Charge = dbData.Select(d => new UVCHartDataSetInt() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.Bat1Charge }).ToList();
//            PV1Volt =    dbData.Select(d => new UVCHartDataSetInt() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.PV1Volt }).ToList();
//            PV2Volt =    dbData.Select(d => new UVCHartDataSetInt() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.PV2Volt }).ToList();

//            ETotal =      dbData.Select(d => new UVCHartDataSetDecimal() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.EToday }).ToList();
//            EToday =      dbData.Select(d => new UVCHartDataSetDecimal() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.EToday }).ToList();
//            PVPower =     dbData.Select(d => new UVCHartDataSetDecimal() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.PVPower }).ToList();
//            LoadPower =   dbData.Select(d => new UVCHartDataSetDecimal() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.LoadPower }).ToList();
//            Bat1Power =   dbData.Select(d => new UVCHartDataSetDecimal() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.Bat1Power }).ToList();
//            Bat1Voltage = dbData.Select(d => new UVCHartDataSetDecimal() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.Bat1Voltage }).ToList();
//            GridPower =   dbData.Select(d => new UVCHartDataSetDecimal() { name = d.RecordedDateTime.ToString("yyyy-MM-dd HH:mm:ss"), value = d.GridPower }).ToList();
//        }
//    }


//    public class UVCHartDataSetInt
//    {
//        public string name { get; set; }
//        public int value { get; set; }
//    }

//    public class UVCHartDataSetDecimal
//    {
//        public string name { get; set; }
//        public decimal value { get; set; }
//    }

//}