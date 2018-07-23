using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
using Windy;
using Windy.IO;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public abstract class TimerBase : IObservable<TimeSlice>, INotifyPropertyChanged
    {
        protected virtual IEnumerable<TimeSlice> GetTimeSlices()
        {
            TimeSlice slice;
            int index = 0;
            var resetter = TimeSlice.CreateTimeSlice(out slice, index);

            while (slice.HasMoreTime)
            {
                resetter(index++);

                yield return slice;
            }
        }

        public virtual void Start()
        {
            IsTimerTicking = true;
            OnStart();
        }
        protected abstract void OnStart();
        public virtual void Stop()
        {
            OnStop();
            IsTimerTicking = false;
        }
        protected abstract void OnStop();

        protected virtual void OnTimeUp()
        {
            IsTimerTicking = false;
        }

        #region Notify Property IsTimerTicking
        private bool isTimerTicking;
        public virtual bool IsTimerTicking
        {
            get { return isTimerTicking; }
            set
            {
                if (isTimerTicking == value)
                    return;

                isTimerTicking = value;
                Notify("IsTimerTicking");
            }
        }
        #endregion

        #region IObservable<TimeSlice> Members

        protected Action<TimeSlice> DispatchNext = delegate { };
        protected Action<Exception> DispatchError = delegate { };
        protected Action DispatchCompleted = delegate { };

        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "Contract.Result<T> will be rewriten after compiling")]
        public IDisposable Subscribe(IObserver<TimeSlice> observer)
        {
            return
               new SubscribeSession<TimeSlice>(
                   observer,
                   (c) =>
                   {
                       this.DispatchNext += c.OnNext;
                       this.DispatchError += c.OnError;
                       this.DispatchCompleted += c.OnCompleted;
                   },
                   (c) =>
                   {
                       this.DispatchNext -= c.OnNext;
                       this.DispatchError -= c.OnError;
                       this.DispatchCompleted -= c.OnCompleted;
                   });
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
