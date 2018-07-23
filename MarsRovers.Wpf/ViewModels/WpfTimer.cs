using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Threading;
using System.ComponentModel;

namespace ThoughtWorks.CodingTests.MarsRovers.ViewModels
{
    public class WpfTimer : ManualTimer, INotifyPropertyChanged
    {
        public WpfTimer()
        {
            InternalTimer = new DispatcherTimer();
            StepInterval = TimeSpan.FromSeconds(1);
            InternalTimer.Tick += new EventHandler(InternalTimer_Tick);
        }

        protected virtual void InternalTimer_Tick(object sender, EventArgs e)
        {
            Tick();
        }

        #region Notify Property StepInterval
        private TimeSpan stepInterval;
        public TimeSpan StepInterval
        {
            get { return stepInterval; }
            set
            {
                if (stepInterval == value)
                    return;

                stepInterval = value;
                InternalTimer.Interval = value;
                Notify("StepInterval");
            }
        }
        #endregion

        public DispatcherTimer InternalTimer { get; private set; }

        protected override void OnStart()
        {
            base.OnStart();
            InternalTimer.Start();
        }

        protected override void OnStop()
        {
            InternalTimer.Stop();
            base.OnStop();
        }

        protected override void OnTimeUp()
        {
            InternalTimer.Stop();
            base.OnTimeUp();
        }
    }
}
