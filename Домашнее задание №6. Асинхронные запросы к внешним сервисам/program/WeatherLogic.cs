using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace program
{
    internal class WeatherLogic : Base
    {
        private string AppId = "appid=9531a69ec844ab86ad34a2548d068ae4";
        // 6754efb83845166229c35f2934122013
        private double Mm = 0.750063755419211;
        private string LocationUrl = "http://api.openweathermap.org/geo/1.0/direct?q=";
        private string ForecastUrl = "http://api.openweathermap.org/data/2.5/forecast?";

        private ObservableCollection<DataResponceInfo> dataResponceInfos = new ObservableCollection<DataResponceInfo>();
        public ObservableCollection<DataResponceInfo> DataResponceInfos
        {
            get => dataResponceInfos;
            set => _ = SetField(ref dataResponceInfos, value);
        }

        private List<GeoLocationInfo> geoLocationInfos;
        public List<GeoLocationInfo> GeoLocationInfos
        {
            get => geoLocationInfos;
            set => _ = SetField(ref geoLocationInfos, value);
        }

        private DataResponceInfo dataResponce;
        public DataResponceInfo DataResponce
        {
            get => dataResponce;
            set => _ = SetField(ref dataResponce, value);
        }

        private string date;
        public string Date
        {
            get => date;
            set => _ = SetField(ref date, value);
        }

        private double pressure;
        public double Pressure
        {
            get => pressure;
            set => _ = SetField(ref pressure, value);
        }

        private Avalonia.Media.Imaging.Bitmap weatherIcon;
        public Avalonia.Media.Imaging.Bitmap WeatherIcon
        {
            get => weatherIcon;
            set => _ = SetField(ref weatherIcon, value);
        }

        private string theme = "";
        public string Theme
        {
            get => theme;
            set => _ = SetField(ref theme, value);
        }

        private string city = "";
        public string City
        {
            get => city;
            set => _ = SetField(ref city, value);
        }

        private List<GeoLocationInfo> GetGeoLocation(string city)
        {
            string location = "";

            try
            {
                string Url = $"{LocationUrl}{city}&{AppId}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        location = reader.ReadToEnd();
                    }
                }

            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }


            List<GeoLocationInfo> geos = new List<GeoLocationInfo>();
            try
            {
                try
                {
                    geos = JsonConvert.DeserializeObject<List<GeoLocationInfo>>(location);
                }
                catch (Exception)
                {
                    throw new Newtonsoft.Json.JsonException();
                }
            }
            catch (Newtonsoft.Json.JsonException)
            {
                throw new Newtonsoft.Json.JsonException();
            }

            return geos;
        }

        private DaysDataResponce GetWeather(string lat, string lon)
        {
            string weather = "";

            try
            {
                string Url = $"{ForecastUrl}lat={lat}&lon={lon}&units=metric&{AppId}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        weather = reader.ReadToEnd();
                    }
                }

            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }

            DaysDataResponce days = new DaysDataResponce();
            try
            {
                try
                {
                    days = JsonConvert.DeserializeObject<DaysDataResponce>(weather);
                }
                catch (Exception)
                {
                    throw new Newtonsoft.Json.JsonException();
                }
            }
            catch (Newtonsoft.Json.JsonException)
            {
                throw new Newtonsoft.Json.JsonException();
            }

            return days;
        }

        private void SetTheme(int hour)
        {
            int current = DateTime.UtcNow.Hour + hour;
            if (current >= 6 && current <= 19)
            {
                Theme = "DodgerBlue";
            }
            else
            {
                Theme = "#0E0c32";
            }
        }

        private void Update(DaysDataResponce forecast)
        {
            DataResponce = forecast.List[0];
            SetTheme(forecast.City.Timezone / 3600);
            Pressure = Math.Round(DataResponce.Main.Pressure * Mm, 0);

            City = forecast.City.Name;

            string iconType = dataResponce.Weather[0].Icon;
            string iconPath = "Assets/" + iconType + "@2x.png";

            WeatherIcon = new Avalonia.Media.Imaging.Bitmap(iconPath);
        }

        public void Forecast(string city)
        {
            try
            {
                GeoLocationInfos = GetGeoLocation(city);


                string lat = Convert.ToString(GeoLocationInfos[0].lat);
                string lon = Convert.ToString(GeoLocationInfos[0].lon);


                DaysDataResponce weatherDays = GetWeather(lat, lon);
                ItemFactory factory = new ItemFactory();

                if (weatherDays.List.Count < 1)
                {
                    return;
                }
                this.dataResponceInfos.Clear();

                Update(weatherDays);

                string date = weatherDays.List[0].DT_txt.Split(" ")[0];
                foreach (var weather in weatherDays.List)
                {
                    string repeate = weather.DT_txt.Split(" ")[0];
                    if (date != repeate)
                    {
                        DataResponceInfos.Add(factory.Create(weather));
                        date = repeate;
                    }
                }

            }
            catch (Newtonsoft.Json.JsonException ex)
            {

            }
        }

        public WeatherLogic()
        {
            this.Date = GetDate.ConvertDayToString(DateTime.Today.DayOfWeek) + ", " + DateTime.Today.Day;
            this.Forecast("Novosibirsk");
            UpdateForecast autoUpdate = new UpdateForecast(this, 600000);
        }

    }
}