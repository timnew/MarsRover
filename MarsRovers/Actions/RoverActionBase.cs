using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public abstract class RoverActionBase : IRoverAction
    {
        protected RoverActionBase(char actionId)
        {
            this.ActionId = actionId;
        }

        public char ActionId { get; private set; }
        public virtual void ApplyTo(Rover rover)
        {
            DoApplyTo(rover);
        }

        protected abstract void DoApplyTo(Rover rover);
    }
}
