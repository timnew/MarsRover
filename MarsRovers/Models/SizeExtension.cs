using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public static class SizeExtension
    {
        public static bool IsInRange(this Size size, Point point)
        {
            bool violation =
                point.X < 0 ||
                point.Y < 0 ||
                point.X > size.Width ||
                point.Y > size.Height;

            return !violation;
        }
    }
}
