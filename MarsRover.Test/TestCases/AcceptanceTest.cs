using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class AcceptanceTest
    {
        [Fact()]
        public void OriginalTestCase()
        {
            var timer = new InstantTimer();

            var provider = new HardCodeConfigurationProvider();

            var plateau = provider.InitializeScenario(timer);

            Assert.Equal(new Size(5, 5), plateau.Size);

            var expected = new string[]
                {
                    "1 2 N",
                    "3 3 E",
                };

            var actual = from fo in plateau.Rovers
                         select fo.ToString();

            Assert.Equal(expected, actual.ToArray());

            timer.Start();

            expected = new string[]
                {
                    "1 3 N",
                    "5 1 E",
                };

            actual = from fo in plateau.Rovers
                     select fo.ToString();

            Assert.Equal(expected, actual.ToArray());
        }


    }
}
