using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMinus.Convertors
{
    public static class WeatherConvertor
    {
        public static double KelvinToCelsius(double kelvin)
        {
            return kelvin - 273.15;
        }

        public static double CelsiusToFarengate(double celsius)
        {
            return (celsius*9/5) + 32;
        }

        public static double FarengateToCelsius(double farengate)
        {
            return (farengate - 32) * 5/9;
        }

        public static double GetFeelsLike(double gradus)
        {
            Random random = new Random();
            return gradus - random.Next(-2, 2);
        }

        public static int GetDewPoint(double temp,double humidity)
        {
            return Convert.ToInt32(temp - (1 - (humidity / 100)) / 0.05 + 8);
        }

        public static double MsToKh(double speed)
        {
            return speed * 3.36;
        }

    }
}
