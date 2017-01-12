using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OpenWeatherModel
{
    public OpenWeatherModel_Weather[] weather;
    public OopenWeatherModel_Main main;
    public OopenWeatherModel_Wind wind;
    public OopenWeatherModel_Sys sys;

    [System.Serializable]
    public class OpenWeatherModel_Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }

    [System.Serializable]
    public class OopenWeatherModel_Main
    {
        public float temp;
        public float pressure;
        public float humidity;
    }

    [System.Serializable]
    public class OopenWeatherModel_Wind
    {
        public float speed;
        public int deg;
    }

    [System.Serializable]
    public class OopenWeatherModel_Sys
    {
        public float sunrise;
        public float sunset;

        public System.DateTime Sunrise
        {
            get
            {
                System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(this.sunrise).ToLocalTime();
                return dtDateTime;
            }
        }
        public System.DateTime Sunset
        {
            get
            {
                System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(this.sunset).ToLocalTime();
                return dtDateTime;
            }
        }
    }
}

//[System.Serializable]
//public class OpenWeatherModel_Weather
//{
//    public List<OpenWeatherModel_WeatherType> weatherTypes;
//}




