using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherMinus.Models;

namespace WeatherMinus.Models
{
    public class DailyForecast : INotifyPropertyChanged
    {
        public string descr { get; set; }

        private int _temp_max { get; set; }
        public int temp_max {
            get
            {
                return _temp_max;
            }
            set
            {
                _temp_max = value;
                OnPropertyChanged();
            }
        }

        private int _temp_min { get; set; }
        public int temp_min
        {
            get
            {
                return _temp_min;
            }
            set
            {
                _temp_min = value;
                OnPropertyChanged();
            }
        } 

        public int humidity { get; set; }
        public double uvindex { get; set; }
        public int wind { get; set; }
        public int deg { get; set; }
        public string icon { get; set; }
        public string Day { get; set; }
        public ObservableCollection<HourlyForecast> Hourlies { get; set; } = new ObservableCollection<HourlyForecast>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
