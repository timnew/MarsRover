using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
using Windy;
using Windy.IO;
using System.ComponentModel;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class ManualTimer : TimerBase
    {
        protected IEnumerator<TimeSlice> timerSession;
        public virtual bool Tick()
        {
            if (timerSession == null)
                return false;

            var result = timerSession.MoveNext();

            if (result)
            {
                DispatchNext(timerSession.Current);
            }
            else
            {
                OnTimeUp();
            }
            return result;
        }

        protected override void OnTimeUp()
        {
            base.OnTimeUp();
            timerSession = null;
        }

        protected override void OnStart()
        {
            timerSession = GetTimeSlices().GetEnumerator();
        }

        protected override void OnStop()
        {
            OnTimeUp();
        }
    }
}
