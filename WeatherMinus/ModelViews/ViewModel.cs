using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Input;
using WeatherMinus.Commands;
using WeatherMinus.Convertors;
using WeatherMinus.Models;
using WeatherMinus.Repository;

namespace WeatherMinus.ModelViews
{
    public class ViewModel:INotifyPropertyChanged
    {
        private string lastUpdateTime = DateTime.Now.ToShortTimeString();
        public string LastUpdateTime
        {

            get
            {
                return lastUpdateTime;
            }
            set
            {
                lastUpdateTime = value;
                OnPropertyChanged();
            }

        }
        private bool isCelsius = true;

        private IBase Base = new OWABase();

        private ObservableCollection<DailyForecast> forecasts = new ObservableCollection<DailyForecast>();
        public ObservableCollection<DailyForecast> Forecasts
        {
            get { return forecasts; }
            set
            {
                forecasts = value;
                OnPropertyChanged();
            }
        }

        public WeatherInfo nowForecast = new WeatherInfo();
        public WeatherInfo NowForecast { get { return nowForecast; }
            set
            {
                nowForecast = value;
                OnPropertyChanged();
            }
        }

        public string CityName { get; set; }
        public string citytb { get; set; }

        public ViewModel()
        {
            CityName = "Baku";
            NowForecast = Base.GetData(CityName);
            Forecasts = Base.GetForecasts(CityName);
        }

        #region Commands
        public ICommand CityChanged
        {
            get
            {
                if (CityName != "")
                {
                    return new DefaultCommand((obj) =>
                    {
                        try
                        {
                            NowForecast = Base.GetData(citytb);
                            Forecasts = Base.GetForecasts(citytb);
                            if (!isCelsius)
                            {
                                ConvertAllToFarengate();
                            }
                            CityName = citytb;
                        }
                        catch { }
                    }
                    );
                }
                else return null;
            }
        }
        public ICommand Refresh
        {
            get
            {
                return new DefaultCommand((obj) =>
                {
                    NowForecast = Base.GetData(NowForecast.Name);
                    Forecasts = Base.GetForecasts(NowForecast.Name);
                    if (!isCelsius)
                    {
                        ConvertAllToFarengate();
                    }
                    LastUpdateTime = DateTime.Now.ToShortTimeString();
                }, (obj) => LastUpdateTime != DateTime.Now.ToShortTimeString()
                );
            }
        }

        public ICommand CelsiusToFarengate
        {
            get
            {
                return new DefaultCommand((obj) =>
                {
                    isCelsius = false;
                    ConvertAllToFarengate();
                }, (obj) => isCelsius != false);
            }
        }
        public ICommand FarengateToCelsius
        {
            get
            {
                return new DefaultCommand((obj) =>
                {
                    isCelsius = true;
                    ConvertAllToCelsius();
                }, (obj) => isCelsius != true);
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void ConvertAllToFarengate()
        {
            NowForecast.Main.temp = Convert.ToInt32(WeatherConvertor.CelsiusToFarengate(NowForecast.Main.temp));
            foreach (DailyForecast d in Forecasts)
            {
                d.temp_max = Convert.ToInt32(WeatherConvertor.CelsiusToFarengate(d.temp_max));
                d.temp_min = Convert.ToInt32(WeatherConvertor.CelsiusToFarengate(d.temp_min));
                foreach (HourlyForecast h in d.Hourlies)
                {                                       
                    h.temp = Convert.ToInt32(WeatherConvertor.CelsiusToFarengate(h.temp));
                }
            }
        }
        private void ConvertAllToCelsius()
        {
            NowForecast.Main.temp = Convert.ToInt32(WeatherConvertor.FarengateToCelsius(NowForecast.Main.temp));
            foreach (DailyForecast d in Forecasts)
            {
                d.temp_max = Convert.ToInt32(WeatherConvertor.FarengateToCelsius(d.temp_max));
                d.temp_min = Convert.ToInt32(WeatherConvertor.FarengateToCelsius(d.temp_min));
                foreach (HourlyForecast h in d.Hourlies)
                {
                    h.temp = Convert.ToInt32(WeatherConvertor.FarengateToCelsius(h.temp));
                }
            }
        }
    }
}
