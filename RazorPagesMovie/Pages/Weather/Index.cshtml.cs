using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages.Weather
{
    public class IndexModel : PageModel
    {
        // Location

        public string CityName { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string CountryCode { get; set; }

        // Main
        public string Temp { get; set; }
        public string FeelsLike { get; set; }
        public string Humidity { get; set; }
        public string WindSpeed { get; set; }
        public string CloudCover { get; set; }

        // Time
        public string Timezone { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }

        public void OnGet()
        {
            ClearVariables();
        }

        public void OnPost()
        {
            ClearVariables();
            CityName = Request.Form["CityName"];
            if (CityName == "")
            {
                return;
            }
            UpdateWeatherForm(CityName);
        }

        public void ClearVariables()
        {
            CityName = Lat = Lon = CountryCode = ""; //Clear Location variables
            Temp = FeelsLike = Humidity = WindSpeed = CloudCover = ""; // Clear Main variables
            Timezone = Sunrise = Sunset = ""; // Clear Time variables
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public string TimezoneToUTC(int Timezone)
        {
            int TimezoneHrs = Timezone / 3600;

            if (TimezoneHrs == 0)
            {
                return "UTC + 0";
            }
            if (TimezoneHrs >= 1)
            {
                return "UTC +" + Convert.ToString(TimezoneHrs);
            }
            else
            {
                return "UTC " + Convert.ToString(TimezoneHrs);
            }
        }
        public void UpdateWeatherForm(string CityName)
        {
            WeatherObject.RootObject weatherRoot = Weather.WeatherData(CityName); // deserialised json with weather data

            // Location variables
            Lat = Convert.ToString(weatherRoot.coord.Lat);
            Lon = Convert.ToString(weatherRoot.coord.Lon);
            CountryCode = weatherRoot.sys.Country;

            // Main variables
            Temp = Convert.ToString(weatherRoot.main.Temp) + "C";
            FeelsLike = Convert.ToString(weatherRoot.main.Feels_like) + "C";
            Humidity = Convert.ToString(weatherRoot.main.Humidity) + "%";
            WindSpeed = Convert.ToString(weatherRoot.wind.Speed * 2.23694);
            while (WindSpeed.Length > 5)
            {
                WindSpeed = WindSpeed.Remove(WindSpeed.Length - 1, 1);
            }
            WindSpeed = WindSpeed + "mph";
            CloudCover = Convert.ToString(weatherRoot.clouds.All) + "%";

            // Time variables
            Timezone = TimezoneToUTC(weatherRoot.Timezone);
            Sunrise = Convert.ToString(UnixTimeStampToDateTime(weatherRoot.sys.Sunrise));
            Sunset = Convert.ToString(UnixTimeStampToDateTime(weatherRoot.sys.Sunset));
        }
    }
}