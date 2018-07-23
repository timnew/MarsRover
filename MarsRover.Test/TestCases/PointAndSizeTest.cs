using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class PointAndSizeTest
    {
        [Fact()]
        public void EqualityTest()
        {
            Point point0 = new Point(0, 0);
            Point point1 = new Point(2, 5);
            Point point2 = new Point(2, 6);
            Point point3 = new Point(4, 5);
            Point point4 = new Point(2, 5);

            Assert.True(point1.Equals(point4));
            Assert.True(point1 == point4);

            Assert.True(!point1.Equals(point2));
            Assert.True(point1 != point3);

            Assert.True(point0 == Point.Empty);

            Assert.Equal(Point.Empty, Point.Empty);

            Assert.Equal(point1, point4);
        }


        [Fact()]
        public void MathOperatorsTest()
        {
            var testCases = new Tuple<Point, Point, Point>[]
            {
                Tuple.Create(new Point(2, 4),Point.Empty,new Point(2, 4)),
                Tuple.Create(new Point(2, 4),new Point(x:2),new Point(4,4)),
                Tuple.Create(new Point(2, 4),new Point(y:2),new Point(2,6)),
                Tuple.Create(new Point(2, 4),new Point(3,5),new Point(5,9)),
                Tuple.Create(new Point(2, 4),new Point(x:-2),new Point(0,4)),
                Tuple.Create(new Point(2, 4),new Point(y:-2),new Point(2,2)),
            };

            testCases
                .Select((fo) => Tuple.Create(fo.Item3, fo.Item1 + fo.Item2))
                .ValidateEquality();

            testCases
                .Select((fo) => Tuple.Create(fo.Item1, fo.Item3 - fo.Item2))
                .ValidateEquality();

            var testCases2 = new Tuple<Point, Point>[]
            {
                Tuple.Create(new Point(0,0),new Point(0,0)),
                Tuple.Create(new Point(2,0),new Point(-2,0)),
                Tuple.Create(new Point(0,2),new Point(0,-2)),
                Tuple.Create(new Point(6,9),new Point(-6,-9)),
            };

            testCases2
                .Select((fo) => Tuple.Create(fo.Item1, -fo.Item2))
                .ValidateEquality();

            testCases2
                .Select((fo) => Tuple.Create(fo.Item2, -fo.Item1))
                .ValidateEquality();
        }


        [Fact()]
        public void InRangeTest()
        {
            Size size = new Size(2, 3);

            var testCases = new Tuple<bool, Point>[]
                {
                    Tuple.Create(true,Point.Empty),
                    Tuple.Create(true, new Point(2,3)),
                    Tuple.Create(true, new Point(2,0)),
                    Tuple.Create(true, new Point(0,3)),
                    Tuple.Create(true, new Point(0,2)),
                    Tuple.Create(true, new Point(2,0)),
                    Tuple.Create(false, new Point(-1,-2)),
                    Tuple.Create(false, new Point(-1,2)),
                    Tuple.Create(false, new Point(2,-2)),
                };

            var result =
                testCases.Select((c) => Tuple.Create(c.Item1, size.IsInRange(c.Item2)));

            result.ValidateEquality();
        }




    }
}
