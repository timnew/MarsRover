using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class OrientationTest
    {
        [Fact()]
        public void ShortNameConversion()
        {
            var testCases = new Tuple<Orientation, char>[]
            {
                Tuple.Create(Orientation.North,'N'),
                Tuple.Create(Orientation.West,'W'),
                Tuple.Create(Orientation.South,'S'),
                Tuple.Create(Orientation.East,'E'),
           };

            var toShortName = from fo in testCases
                              select Tuple.Create(fo.Item2, fo.Item1.ToShortName());

            toShortName.ValidateEquality();

            var fromShortName = from fo in testCases
                                select Tuple.Create(fo.Item1, fo.Item2.ParseAsOrientation());

            fromShortName.ValidateEquality();

            var testCases2 = new Tuple<Orientation, char>[]
            {
                Tuple.Create(Orientation.North,'n'),
                Tuple.Create(Orientation.West,'w'),
                Tuple.Create(Orientation.South,'s'),
                Tuple.Create(Orientation.East,'e'),
           };

            var fromShortName2 = from fo in testCases2
                                 select Tuple.Create(fo.Item1, fo.Item2.ParseAsOrientation());

            fromShortName2.ValidateEquality();
        }


        [Fact()]
        public void NormalizeTest()
        {
            var testCases = new Tuple<Orientation, Orientation>[]
            {
                Tuple.Create(Orientation.North,Orientation.North),
                Tuple.Create(Orientation.West,Orientation.West),
                Tuple.Create(Orientation.South,Orientation.South),
                Tuple.Create(Orientation.East,Orientation.East),

                Tuple.Create(Orientation.East,Orientation.East+4),
                Tuple.Create(Orientation.East,Orientation.East+8),
                Tuple.Create(Orientation.East,Orientation.East-4),
                Tuple.Create(Orientation.East,Orientation.East-8),
           };

            testCases
                .Select((i) => Tuple.Create(i.Item1, i.Item2.Normalize()))
                .ValidateEquality();
        }


        [Fact()]
        public void TurnTest()
        {

            var testCases = new Tuple<Orientation, int, Orientation>[]
            {
                Tuple.Create(Orientation.North,0,Orientation.North),
                Tuple.Create(Orientation.West,4,Orientation.West),
                Tuple.Create(Orientation.South,-4,Orientation.South),
                Tuple.Create(Orientation.East,8,Orientation.East ),

                Tuple.Create(Orientation.North, 1, Orientation.West),
                Tuple.Create(Orientation.North,2,Orientation.South),
                Tuple.Create(Orientation.North,-2,Orientation.South),
                Tuple.Create(Orientation.North,3,Orientation.East),
                Tuple.Create(Orientation.North,-1,Orientation.East),
            };

            testCases
                .Select((fo) => Tuple.Create(fo.Item3, fo.Item1.Turn(fo.Item2)))
                .ValidateEquality();



        }




    }
}