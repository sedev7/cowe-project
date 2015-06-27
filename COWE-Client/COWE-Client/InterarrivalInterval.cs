using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.Client
{
    public class InterarrivalInterval
    {
        private static int timeMilliseconds = 30;    // Default value

        public InterarrivalInterval (int interval)
        {
            timeMilliseconds = interval;
        }
        public static int GetIntervalMilliSeconds()
        { 
            return timeMilliseconds;
        }

        public static void SetInterval(int newTime)
        {
            timeMilliseconds = newTime;
        }
           
    }
}
