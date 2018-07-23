using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windy;
using Windy.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public static class Helper
    {
        public static string GetName(this Rover rover)
        {
            Contract.Requires<ArgumentNullException>(rover != null, "rover");

            return rover.GetHashCode().ToString();
        }

        public static void Display(this Rover rover)
        {
            Contract.Requires<ArgumentNullException>(rover != null, "rover");

            "Rover: {0} Poisition: {1} Heading: {2}"
                .ApplyFormat(
                    rover.GetName(),
                    rover.Position,
                    rover.Orientation)
                .Display(ConsoleColor.Yellow);
        }

        public static void Display(this Plateau plateau)
        {
            Contract.Requires<ArgumentNullException>(plateau != null, "plateau");

            "Plateau: {0,2} x {1,2}"
                .ApplyFormat(plateau.Size.Width, plateau.Size.Height)
                .Display(ConsoleColor.Yellow);

            plateau.Rovers.ForEach(Display);

            "Total {0} Rover(s)\r\n"
                .ApplyFormat(plateau.Rovers.Count)
                .Display(ConsoleColor.Yellow);
        }
    }
}
