using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMinus.Models
{
    public class DialyForecast
    {
        public string ImagePath { get; set; }
        public string Date { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public string Descr { get; set; }
        public ObservableCollection<HourlyForecast> HourlyForecasts { get; set; }

    }
}
