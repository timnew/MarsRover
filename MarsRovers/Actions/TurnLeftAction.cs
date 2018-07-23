using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class TurnLeftAction : RoverActionBase
    {
        public TurnLeftAction()
            : base('L')
        { }

        protected override void DoApplyTo(Rover rover)
        {
            rover.Orientation = rover.Orientation.Turn(1);
        }
    }
}
