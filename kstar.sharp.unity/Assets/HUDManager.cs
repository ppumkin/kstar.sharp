using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    //PV 
    public Text PVPower;
    public Text BatteryPercent;
    public Text BatteryPower;
    public Text GridPower;
    public Text LoadPower;

    //Weather 
    public Text OutsideWeather;
    public Text Sunrise;
    public Text Sunset;


    public Text SystemTime;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateHUD", 0f, 1f);
    }

    void UpdateHUD()
    {
        var inv = WebRequests.InverterData;
        PVPower.text = inv.PVPower + "W";
        BatteryPercent.text = inv.Bat1Charge + "%";
        BatteryPower.text = inv.Bat1Power + "W";
        GridPower.text = inv.GridPower + "W";
        LoadPower.text = inv.LoadPower + "W";

        var wea = WebRequests.OpenWeatherData;
        string outsideWeatherText = wea.main.temp + "°C";
        foreach (var item in wea.weather)
        {
            outsideWeatherText += " " + item.main;
        }
        OutsideWeather.text = outsideWeatherText;
        SystemTime.text = System.DateTime.Now.ToString("HH:mm.ss");
        Sunrise.text = wea.sys.Sunrise.ToString("HH:mm");
        Sunset.text = wea.sys.Sunset.ToString("HH:mm");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
