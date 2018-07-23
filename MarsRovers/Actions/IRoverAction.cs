using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public interface IRoverAction
    {
        char ActionId { get; }
        void ApplyTo(Rover rover);
    }
}