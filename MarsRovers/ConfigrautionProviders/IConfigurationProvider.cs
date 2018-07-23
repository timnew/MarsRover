using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public interface IConfigurationProvider
    {
        Plateau InitializeScenario(IObservable<TimeSlice> timer, Plateau target = null);
    }
}

