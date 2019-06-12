using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherMinus.Models;
using WeatherMinus.Convertors;
using System.Collections.Generic;

namespace WeatherMinus.Repository
{
    public class OWABase : IBase
    {
        private const string token = "36ea636efc3e52165736d5561033b25d";
        private const string webApiW = "http://api.openweathermap.org/data/2.5/weather?q=";
        private const string webApiF = "http://api.openweathermap.org/data/2.5/forecast?q=";
        private const string webApiUVI = "http://api.openweathermap.org/data/2.5/uvi/forecast?appid=";

        public WeatherInfo GetData(string city)
        {
            string resultApi;
            WebClient client = new WebClient();
            try
            {
                resultApi = client.DownloadString($"{webApiW}{city}&appid={token}");

                var result = JsonConvert.DeserializeObject<WeatherInfo>(resultApi);

                WeatherInfo root = result;

                #region Converting
                root.Main.temp = Convert.ToInt32(WeatherConvertor.KelvinToCelsius(root.Main.temp));
                root.visibility /= 1000;
                root.FeelsLike = Convert.ToInt32(WeatherConvertor.GetFeelsLike(root.Main.temp));
                root.DewPoint = WeatherConvertor.GetDewPoint(root.Main.temp, root.Main.humidity);
                root.Wind.speed = Convert.ToInt32(WeatherConvertor.MsToKh(root.Wind.speed));

                root.Sunrise = UnixConvertor.ConvertUnixToLocalTime(root.Sys.sunrise).ToShortTimeString();
                root.Sunset = UnixConvertor.ConvertUnixToLocalTime(root.Sys.sunset).ToShortTimeString();
                root.Moonrise = UnixConvertor.ConvertUnixToLocalTime(root.Sys.sunrise).AddMinutes(-35).ToShortTimeString();
                root.Moonset = UnixConvertor.ConvertUnixToLocalTime(root.Sys.sunset).AddMinutes(39).ToShortTimeString();

                root.icon = "/Images/WeatherStatuss/" + root.Weather[0].icon + ".png";
                root.BackgoundImagePath = "/Images/BackgroundW/" + root.Weather[0].icon + ".png";

                root.descr = root.Weather[0].description.Substring(0, 1).ToUpper() + root.Weather[0].description.Remove(0, 1);

                #endregion

                return root;
            }
            catch
            {
                throw;
            }
        }

        public ObservableCollection<DailyForecast> GetForecasts(string city)
        {

            string resultApi;
            WebClient client = new WebClient();
            try
            {
                resultApi = client.DownloadString($"{webApiF}{city}&appid={token}");

                ForecastsInfo result = JsonConvert.DeserializeObject<ForecastsInfo>(resultApi);

                ForecastsInfo root = result;

                ObservableCollection<DailyForecast> df = new ObservableCollection<DailyForecast>();

                string aDaysUV = client.DownloadString($"{webApiUVI}{token}&lat={root.city.coord.lat.ToString().Replace(',', '.')}&lon={root.city.coord.lon.ToString().Replace(',', '.')}&cnt=5");
                dynamic uvJson = JsonConvert.DeserializeObject(aDaysUV);

                IEnumerable<ForecastsInfo.list> lists;

                int i = 0; //UVIndexCNTr
                for (DateTime day = DateTime.Now; ;day=day.AddDays(1))
                {

                    if(day.Day<=9)
                    {
                        lists = from l in root.List where l.dt_txt.Split('-').Last().Split(' ').First() == ("0"+day.Day.ToString()) select l;
                    }
                    else { lists = from l in root.List where l.dt_txt.Split('-').Last().Split(' ').First() == day.Day.ToString() select l; }

                    if (lists.Count() != 0)
                    {
                        DailyForecast d = new DailyForecast
                        {
                            deg = Convert.ToInt32(lists.First().wind.deg),
                            wind = Convert.ToInt32(lists.First().wind.speed),
                            humidity = Convert.ToInt32(lists.First().main.humidity),
                            Day = day.DayOfWeek.ToString()+" "+day.Day.ToString(),
                            descr = lists.First().Weather[0].description.Substring(0, 1).ToUpper() + lists.First().Weather[0].description.Remove(0, 1),
                            icon = "/Images/WeatherStatuss/" + lists.First().Weather[0].icon + ".png"
                        };

                        #region Get UVIndex
                        if (day.ToShortDateString() == DateTime.Now.ToShortDateString())
                        {
                            string todayuv = client.DownloadString($"http://api.openweathermap.org/data/2.5/uvi?appid={token}&lat={root.city.coord.lat.ToString().Replace(',', '.')}&lon={root.city.coord.lon.ToString().Replace(',', '.')}");
                            dynamic uv1json = JsonConvert.DeserializeObject(todayuv);
                            d.uvindex = uvJson[0]["value"];
                        }
                        else
                        {
                            d.uvindex = uvJson[i]["value"];
                        }
                        i++;
                        #endregion

                        foreach (ForecastsInfo.list list in lists)
                        {
                            HourlyForecast h = new HourlyForecast
                            {
                                time = list.dt_txt.Split(' ').Last(),
                                temp = Convert.ToInt32(WeatherConvertor.KelvinToCelsius(list.main.temp)),
                                wind = Convert.ToInt32(WeatherConvertor.MsToKh(list.wind.speed)),
                                deg = Convert.ToInt32(list.wind.deg),
                                icon = "/Images/WeatherStatuss/" + list.Weather[0].icon + ".png",
                                descr = list.Weather[0].description.Substring(0, 1).ToUpper() + list.Weather[0].description.Remove(0, 1),
                                humidity = Convert.ToInt32(list.main.humidity)
                            };

                            d.Hourlies.Add(h);
                        }

                        #region Get min/max
                        d.temp_max = d.Hourlies.OrderBy(x => x.temp).Last().temp;
                        d.temp_min = d.Hourlies.OrderBy(x => x.temp).First().temp;
                        #endregion

                        df.Add(d);
                    }
                    else if (lists.Count() == 0)
                    {
                        break;
                    }
                }


                return df;
            }
            catch { throw; }
        }

    }

    public interface IBase
    {
        WeatherInfo GetData(string city);
        ObservableCollection<DailyForecast> GetForecasts(string city);
    }

}
