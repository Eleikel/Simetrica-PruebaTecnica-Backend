using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace TaskManager.Common
{
    public class Server
    {
        public static DateTime GetDate()
        {
            TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("America/Santo_Domingo");
            return TimeZoneInfo.ConvertTime(DateTime.Now, tzi);
        }
    }
}
