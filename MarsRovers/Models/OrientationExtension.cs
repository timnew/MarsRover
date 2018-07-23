using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Windy;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public static class OrientationExtension
    {
        public static char ToShortName(this Orientation orientation)
        {
            var result = orientation.ToString();

            if (result.Length < 1)
                throw new Exception("Unexpected Exception");

            return result[0];
        }


        public static Orientation ParseAsOrientation(this char shortName)
        {
            shortName = char.ToUpperInvariant(shortName);

            switch (shortName)
            {
                case 'N':
                    return Orientation.North;
                case 'W':
                    return Orientation.West;
                case 'S':
                    return Orientation.South;
                case 'E':
                    return Orientation.East;
                default:
                    throw new ArgumentOutOfRangeException("shotName should be either 'N' 'W' 'S' or 'E'");
            }
        }

        public static Orientation Turn(this Orientation origin, int angles)
        {
            return (origin + angles).Normalize();
        }

        public static Orientation Normalize(this Orientation orientation)
        {
            var value = (int)orientation;
            value = value % 4;

            if (value < 0)
                value += 4;

            return (Orientation)value;
        }
    }
}
