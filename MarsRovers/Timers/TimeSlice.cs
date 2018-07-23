using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Windy;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class TimeSlice
    {
        public static Action<int> CreateTimeSlice(out TimeSlice result, int startIndex = 0)
        {
            result = new TimeSlice(startIndex);

            return result.TimeController;
        }

        protected TimeSlice(int index)
        {
            Index = index;
            HasMoreTime = true;
        }

        public int Index { get; private set; }
        public bool HasMoreTime { get; private set; }

        private void TimeController(int index)
        {
            Index = index;
            HasMoreTime = false;
        }

        public void ReportNeedMoreTime()
        {
            HasMoreTime = true;
        }
    }
}
