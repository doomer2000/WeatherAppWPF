using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMinus.Models
{
    public class NowForecast:INotifyPropertyChanged
    {
        private string cityName;

        public string CityName
        {
            get { return cityName; }
            set
            {
                cityName = value;
                OnPropertyChanged("CityName");
            }
        }

        private int wind;

        public int Wind
        {
            get { return wind; }
            set
            {
                wind = value;
                OnPropertyChanged();
            }
        }

        private double barometer;

        public double Barometer
        {
            get { return barometer; }
            set
            {
                barometer = value;
                OnPropertyChanged();
            }
        }

        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }

            set
            {
                imagePath = value;
                OnPropertyChanged();
            }
        }

        private int humidity;

        public int Humidity
        {
            get { return humidity; }
            set
            {
                humidity = value;
                OnPropertyChanged();
            }
        }

        private int dewPoint;

        public int DewPoint
        {
            get { return dewPoint; }
            set
            {
                dewPoint = value;
                OnPropertyChanged();
            }
        }

        private int visibility;

        public int Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                OnPropertyChanged();
            }
        }

        private int feelsLike;

        public int FeelsLike
        {
            get { return feelsLike; }
            set
            {
                feelsLike = value;
                OnPropertyChanged();
            }
        }

        private int temp;

        public int Temp
        {
            get { return temp; }
            set
            {
                temp = value;
                OnPropertyChanged("Temp");
            }
        }

        private string descr;

        public string Descr
        {
            get { return descr; }
            set
            {
                descr = value;
                OnPropertyChanged("Descr");
            }
        }

        private string sunrise { get; set; }
        private string sunset { get; set; }

        public string Sunrise
        {
            get { return sunrise; }
            set
            {
                sunrise = value;
                OnPropertyChanged();
            }
        }
        public string Sunset
        {
            get { return sunset; }
            set
            {
                sunset = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
