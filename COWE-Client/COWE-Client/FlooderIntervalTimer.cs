using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace COWE.Client
{
    public static class FlooderIntervalTimer
    {
        static Timer _timer;

        static void Start(int interval)
        {
            _timer = new Timer(interval);

            _timer.Elapsed += new ElapsedEventHandler(OnFlooderIntervalTimerElapsed);
            _timer.Enabled = true;
        }

        static void OnFlooderIntervalTimerElapsed(object sender, ElapsedEventArgs e)
        {

        }
    }
}
