using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMinus.Models
{
    public class HourlyForecast : INotifyPropertyChanged
    {
        public string time { get; set; }

        private int _temp;

        public int temp
        {
            get
            {
                return _temp;
            }
            set
            {
                _temp = value;
                OnPropertyChanged();
            }
        }
        
        public string icon { get; set; }
        public string descr { get; set; }
        public int humidity { get; set; }
        public int wind { get; set; }
        public int deg { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
