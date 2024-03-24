using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace program
{
    internal class TemperatureInfo
    {
        public float Temp { get; set; }
        public double Pressure { get; set; }
        public float Humidity { get; set; }
    }

    internal class WindInfo
    {
        public float Speed { get; set; }
    }

    internal class WeatherInfo
    {
        public string Icon { get; set; }
    }

    internal class GeoLocationInfo
    {
        public string Name { get; set; }
        public float lon { get; set; }
        public float lat { get; set; }
    }

    internal class CityInfo
    {
        public string Name { get; set; }
        public int Timezone { get; set; }
    }

    internal class DataResponceInfo
    {
        private Avalonia.Media.Imaging.Bitmap icon;
        public Avalonia.Media.Imaging.Bitmap Icon
        {
            get => icon;
            set => icon = value;
        }

        public TemperatureInfo Main { get; set; }
        public WindInfo Wind { get; set; }
        public List<WeatherInfo> Weather { get; set; }
        public string DT_txt { get; set; }
        public string DT { get; set; }
    }

    internal class DaysDataResponce
    {
        public List<DataResponceInfo> List { get; set; }
        public CityInfo City { get; set; }
    }
}