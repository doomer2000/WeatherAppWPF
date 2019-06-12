using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMinus.Convertors
{
    static class UnixConvertor
    {
        public static DateTime ConvertUnixToLocalTime(int unix)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unix).ToLocalTime();
            return dtDateTime;
        }
    }
}
