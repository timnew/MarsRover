using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;

using Windy;
using Windy.IO;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class TextReaderConfigurationProvider : IConfigurationProvider
    {
        public TextReaderConfigurationProvider(TextReader inputSource = null)
        {
            this.InputSource = inputSource ?? Console.In;
        }

        public TextReader InputSource { get; private set; }

        #region IConfigurationProvider Members

        public Plateau InitializeScenario(IObservable<TimeSlice> timer, Plateau target = null)
        {
            if (target == null)
            {
                target = new Plateau();
            }
            else
            {
                target.Reset();
            }

            var sizeTuple =
                UserInputHelper.Query
                (
                    UserInputHelper.CreateToTuple<int, int>(int.TryParse, int.TryParse),
                    "Input Plateau Size(width height): ",
                    input: InputSource
                );

            target.SetSize(sizeTuple.Item1, sizeTuple.Item2);

            var roverTupleFactory = UserInputHelper.CreateToTuple<int, int, char>(int.TryParse, int.TryParse, char.TryParse);

            int roverIndex = 0;

            while (true)
            {
                roverIndex++;

                var roverTulpe =
                    UserInputHelper.Query
                    (
                        roverTupleFactory,
                        "Initial State of Rover {0,2} (x y orientation): ".ApplyFormat(roverIndex),
                        true,
                        InputSource
                    );

                if (roverTulpe == null)
                    break;

                var roverInstructions =
                    UserInputHelper.QueryString
                    (
                       "Instructions for Rover {0,2} : ".ApplyFormat(roverIndex),
                       true,
                       InputSource
                    );

                if (roverInstructions == null)
                    break;

                target
                    .DeployRover(roverTulpe.Item1, roverTulpe.Item2, roverTulpe.Item3)
                    .SetInstruction(roverInstructions, timer);
            }

            return target;
        }

        #endregion
    }
}