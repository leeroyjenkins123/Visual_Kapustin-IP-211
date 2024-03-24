using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program
{
    internal class ItemFactory
    {
        public DataResponceInfo Create(DataResponceInfo weather)
        {
            string iconType = weather.Weather[0].Icon;
            string iconPath = "Assets/" + iconType + "@2x.png";

            weather.Icon = new Avalonia.Media.Imaging.Bitmap(iconPath);

            DateTime result = new DateTime();
            string[] separatedData = weather.DT_txt.Split(' ');

            if (DateTime.TryParse(separatedData[0], out result))
            {
                weather.DT_txt = GetDate.ConvertDayToString(result.DayOfWeek) + ", " +
                    result.Day;
            }

            return weather;
        }
    }
}
