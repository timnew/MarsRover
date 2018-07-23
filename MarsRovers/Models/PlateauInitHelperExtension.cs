using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public static class PlateauInitHelperExtension
    {
        public static Plateau SetSize(this Plateau plateau, int width, int height)
        {
            Contract.Requires(plateau != null);
            Contract.Ensures(Contract.Result<Plateau>() != null);

            plateau.Size = new Size(width, height);
            return plateau;
        }

        public static Rover DeployRover(this Plateau plateau, int x, int y, char orientation)
        {
            Contract.Requires(plateau != null);
            Contract.Ensures(Contract.Result<Rover>() != null);

            return plateau.DeployRover(new Point(x, y), orientation.ParseAsOrientation());
        }

        public static Rover SetInstruction(this Rover rover, string instructions, IObservable<TimeSlice> timer = null)
        {
            Contract.Requires(instructions != null);
            Contract.Requires(rover != null);
            Contract.Ensures(Contract.Result<Rover>() != null);

            instructions
                .CreateControlerFromInstructions(timer)
                .AttachTo(rover);

            return rover;
        }

        public static Plateau EndConfigRover(this Rover rover)
        {
            Contract.Requires(rover != null);
            Contract.Requires(rover.Plateau != null);
            Contract.Ensures(Contract.Result<Plateau>() != null);

            return rover.Plateau;
        }
    }
}
