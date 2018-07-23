using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class ActionTest
    {
        [Fact()]
        public void TurnActionTest()
        {
            Rover rover = new Rover(null, Point.Empty, Orientation.North);

            Assert.Equal(Orientation.North, rover.Orientation);

            var turnRight = RoverActionPool.GetAction('R');
            var turnLeft = RoverActionPool.GetAction('L');

            turnRight.ApplyTo(rover);
            Assert.Equal(Orientation.East, rover.Orientation);

            turnRight.ApplyTo(rover);
            Assert.Equal(Orientation.South, rover.Orientation);

            turnLeft.ApplyTo(rover);
            Assert.Equal(Orientation.East, rover.Orientation);
        }



        [Fact()]
        public void MoveActionTest()
        {
            var testCases = new Tuple<Point, Orientation, Point>[] 
            {
                Tuple.Create(new Point(2,2), Orientation.North, new Point(2,3)),
                Tuple.Create(new Point(2,2), Orientation.East, new Point(3,2)),
                Tuple.Create(new Point(2,2), Orientation.South, new Point(2,1)),
                Tuple.Create(new Point(2,2), Orientation.West, new Point(1,2)),
            };

            var move = RoverActionPool.GetAction('M');

            Func<Point, Orientation, Point> doMove = (p, o) =>
                {
                    var rover = new Rover(null, p, o);
                    move.ApplyTo(rover);
                    return rover.Position;
                };


            var result = testCases.Select
                (
                    (c) => Tuple.Create
                             (
                                c.Item3,
                                doMove(c.Item1, c.Item2)
                             )
                );

            result.ValidateEquality();
        }


        [Fact()]
        public void MoveOutOfBoundryTest()
        {
            var moveAction = RoverActionPool.GetAction('M');

            Plateau plateau =
                new Plateau()
                .SetSize(6, 8);

            var rovers = new Rover[]
                {
                    plateau.DeployRover(1, 5, 'w'),
                    plateau.DeployRover(2, 6, 'n'),
                    plateau.DeployRover(3, 5, 'e'),
                    plateau.DeployRover(2, 4, 's'),
                };

            foreach (var fo in rovers)
            {
                moveAction.ApplyTo(fo);
            }

            for (int fo = 0; fo < 4; fo++)
            {
                Assert.Throws<InvalidOperationException>(() => moveAction.ApplyTo(rovers[fo]));

                for (int foRover = fo + 1; foRover < 4; foRover++)
                {
                    moveAction.ApplyTo(rovers[foRover]);
                }
            }
        }



    }
}
