using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMinus.Models
{
    public class WeatherInfo
    {
        public class weather
        {
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class coord
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }

        public class main : INotifyPropertyChanged
        {
            private double _temp { get; set; }
            public double temp
            {
                get { return _temp; }
                set
                {
                    _temp = value;
                    OnPropertyChanged();
                }
            }
            public double temp_max { get; set; }
            public double temp_min { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }

        }

        public class wind
        {
            public double speed { get; set; }
            public double deg { get; set; }
        }

        public class sys
        {
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }


        public string Name { get; set; }
        public int visibility { get; set; }
        public sys Sys { get; set; }
        public List<weather> Weather { get; set; }
        public string icon { get; set; }
        public string descr { get; set; }
        public int DewPoint { get; set; }
        public int FeelsLike { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string Moonrise { get; set; }
        public string Moonset { get; set; }
        public double dt { get; set; }
        public wind Wind { get; set; }
        private main _Main { get; set; }
        public main Main { get
            {
                return _Main;
            }
            set
            {
                _Main = value;
            }
        }
        public string BackgoundImagePath { get; set; }

    }
}
