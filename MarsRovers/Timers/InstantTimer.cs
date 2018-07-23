using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
using Windy;
using Windy.IO;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class InstantTimer : TimerBase
    {
        protected override void OnStart()
        {
            foreach (var fo in GetTimeSlices())
            {
                base.DispatchNext(fo);
            }

            OnTimeUp();
        }

        protected override void OnStop()
        {
            // DoNothing;
        }
    }
}
