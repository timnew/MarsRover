using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public static class RoverControllerExtension
    {
        public static IRoverController CreateControlerFromInstructions(this IEnumerable<char> instructions, IObservable<TimeSlice> timer = null)
        {
            Contract.Requires(instructions != null);
            Contract.Ensures(Contract.Result<IRoverController>() != null);

            var result = new PresetInstructionRoverController(instructions.ParseAsInstructions());

            if (timer != null)
            {
                result.AttachTimer(timer);
            }

            return result;
        }

        public static void AttachTo(this IRoverController controller, Rover rover)
        {
            Contract.Requires(rover != null);
            rover.AttachContoller(controller);
        }
    }
}
