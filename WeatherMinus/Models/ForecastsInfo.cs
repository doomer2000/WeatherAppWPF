using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherMinus.Models;

namespace WeatherMinus.Models
{
    public class ForecastsInfo
    {
        public class list
        {
            public int dt { get; set; }
            public string dt_txt { get; set; }
            public WeatherInfo.main main { get; set; }
            public WeatherInfo.wind wind { get; set; }
            public List<WeatherInfo.weather> Weather { get; set; }
        }

        public city city { get; set; }
        public int cnt { get; set; }
        public ObservableCollection<list> List { get; set; }
    }

    public class city
    {
        public WeatherInfo.coord coord { get; set; }
    }
}
