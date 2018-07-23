using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class RoverControllerTest
    {
        [Fact()]
        public void InstructionParseTest()
        {
            var expected = new IRoverAction[]
            {
                RoverActionPool.GetAction('L'),
                RoverActionPool.GetAction('M'),
                RoverActionPool.GetAction('L'),
                RoverActionPool.GetAction('M'),
                RoverActionPool.GetAction('R'),
            };

            var actual = "LMLMR".ParseAsInstructions().ToArray();

            Assert.Equal(expected, actual);
        }


        [Fact()]
        public void ControllerTriggerTest()
        {
            ManualTimer timer = new ManualTimer();

            Rover rover = new Rover(null, Point.Empty, Orientation.North);

            var testCases = new Tuple<Point, Orientation, char>[]
                {
                    Tuple.Create( new Point(0,1), Orientation.North,'M'),
                    Tuple.Create( new Point(0,2), Orientation.North,'M'),
                    Tuple.Create( new Point(0,2), Orientation.East,'R'),
                    Tuple.Create( new Point(0,2), Orientation.South,'R'),
                    Tuple.Create( new Point(0,2), Orientation.East,'L'),
                };

            var instructions = from fo in testCases
                               select fo.Item3;

            instructions
                .CreateControlerFromInstructions(timer)
                .AttachTo(rover);

            timer.Start();

            foreach (var fo in testCases)
            {
                Assert.True(timer.Tick());

                Assert.Equal(fo.Item1, rover.Position);
                Assert.Equal(fo.Item2, rover.Orientation);
            }
            
            Assert.False(timer.Tick());
        }



    }
}
