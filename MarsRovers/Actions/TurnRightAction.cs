using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class TurnRightAction : RoverActionBase
    {
        public TurnRightAction()
            : base('R')
        { }

        protected override void DoApplyTo(Rover rover)
        {
            rover.Orientation = rover.Orientation.Turn(-1);
        }
    }
}
