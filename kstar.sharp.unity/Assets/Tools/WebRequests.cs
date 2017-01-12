using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebRequests : MonoBehaviour
{

    /// <summary>
    /// Most recent data loaded from the API
    /// </summary>
    public static InverterDataGranular InverterData;
    public static OpenWeatherModel OpenWeatherData;


    private string URL = "http://192.168.1.2/Home/GetLatest";
    private string Weather_URL = "http://api.openweathermap.org/data/2.5/weather?id=2643266&appid=6e3891eefe1bc570dc718c6d66424392&units=metric";

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("LoadInverterData", 0.1f, 30.0f);
        InvokeRepeating("LoadWeatherrData", 0.1f, 900.0f);
    }


    void LoadInverterData()
    {
        StartCoroutine(DoWebRequest_Inverter());
    }

    void LoadWeatherrData()
    {
        StartCoroutine(DoWebRequest_Weather());
    }

    IEnumerator DoWebRequest_Inverter()
    {
        WWW www = new WWW(URL);
        yield return www;

        var result = www.text;
        //Debug.Log(result);

        InverterData = JsonUtility.FromJson<InverterDataGranular>(result);
        //Debug.Log("Date: " + Data.RecordedDateTime 
        //    + " PV Power: " + Data.PVPower 
        //    + " Load Power: "  + Data.LoadPower 
        //    + " ETotal: " + Data.ETotal);
    }

    IEnumerator DoWebRequest_Weather()
    {
        WWW www = new WWW(Weather_URL);
        yield return www;

        var result = www.text;

        OpenWeatherData = JsonUtility.FromJson<OpenWeatherModel>(result);

        //Debug.Log("TEMP: " + OpenWeatherData.main.temp);
        //Debug.Log("main: " + OpenWeatherData.weather[0].main);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
