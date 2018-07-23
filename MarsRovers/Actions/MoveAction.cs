using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class MoveAction : RoverActionBase
    {
        public MoveAction()
            : base('M')
        { }

        private static Lazy<Point[]> Points =
            new Lazy<Point[]>(
                    () => new Point[]
                            {
                                new Point(x:0,y:1),
                                new Point(x:-1,y:0),
                                new Point(x:0,y:-1),
                                new Point(x:1,y:0),
                            });
        protected static Point GetOffset(Orientation orientation)
        {
            return Points.Value[(int)orientation];
        }


        protected override void DoApplyTo(Rover rover)
        {
            rover.Position += GetOffset(rover.Orientation);

            if (rover.Plateau != null && !rover.Plateau.Size.IsInRange(rover.Position))
            {
                throw new InvalidOperationException("Rover has been moved out of Plateau boundry");
            }
        }
    }
}
