using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace COWEClient
{
    public class Clock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TimeElapsed { get; set; }

        private DispatcherTimer timer;
        private Stopwatch stopWatch;

        public void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += dispatcherTimerTick_;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            stopWatch = new Stopwatch();
            stopWatch.Start();
            timer.Start();
        }
        //this.stopwatch = new Stopwatch();
        //this._worker = new BackgroundWorker();
        //this._worker.WorkerReportsProgress = true, WorkerSuppportsCancellation = true };

        private void dispatcherTimerTick_(object sender, EventArgs e)
        {
            TimeElapsed = stopWatch.Elapsed.TotalMilliseconds.ToString();
            PropertyChanged(this, new PropertyChangedEventArgs("TimeElapsed"));
        }

       
    }
}
