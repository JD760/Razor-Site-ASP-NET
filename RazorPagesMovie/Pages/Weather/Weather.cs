using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace RazorPagesMovie.Pages.Weather
{
    public class Weather
    {

        private const string APIKey = "66193b691cab0a63f69bb9930e142587"; // KEEP THIS SECRET
        const string BASEURL = "http://api.openweathermap.org/data/2.5/weather?q=";

        public static WeatherObject.RootObject WeatherData(string location)
        {

            location = whitespaceRemover(location);
            string url = BASEURL + location + "&mode=json&units=metric&appid=" + APIKey; // builds the URL from user input data

            // creates a web client instance and downloads the json data from provided URL to the
            // variable 'json'
            WebClient wc = new WebClient(); // creates a new instance of the WebClient class

            var json = wc.DownloadString(url);
            //TODO HANDLE System.Net.WebException

            var root = JsonConvert.DeserializeObject<WeatherObject.RootObject>(json); // converts the json data into an object made of classes
            return root;


        }


        /// <summary>
        /// method to remove all whitespace in a string for compatibility with URLs
        /// </summary>
        /// <param name="s">the input string to be cleared of whitespace </param>
        /// <returns> the input string is returned after having whitespace removed</returns>
        static string whitespaceRemover(string s)
        {
            s.Replace(" ", "");
            return s;
        }


    }
    /// <summary>
    /// Nested classes to deserialise weather data JSON into
    /// </summary>
    public class WeatherObject
    {
        public class Coord
        {
            public double Lon { get; set; }
            public double Lat { get; set; }
        }

        public class Weather
        {
            public int ID { get; set; }
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }

        public class Main
        {
            public double Temp { get; set; }
            public double Feels_like { get; set; }
            public double Temp_min { get; set; }
            public double Temp_max { get; set; }
            public int Pressure { get; set; }
            public int Humidity { get; set; }
        }

        public class Wind
        {
            public double Speed { get; set; }
            public int Deg { get; set; }
        }

        public class Clouds
        {
            public int All { get; set; }
        }

        public class Sys
        {
            public int Type { get; set; }
            public int ID { get; set; }
            public string Country { get; set; }
            public int Sunrise { get; set; }
            public int Sunset { get; set; }
        }

        public class Misc
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Cod { get; set; }
        }

        public class RootObject
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public int visibility { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public Misc misc { get; set; }
            public int Timezone { get; set; }
        }
    }
}
