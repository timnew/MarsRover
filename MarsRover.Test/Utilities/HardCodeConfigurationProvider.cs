using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    class HardCodeConfigurationProvider : IConfigurationProvider
    {
        public Plateau InitializeScenario(IObservable<TimeSlice> timer, Plateau target = null)
        {
            if (target == null)
                target = new Plateau();
            else
                target.Reset();

            /*
            5 5
            1 2 N
            LMLMLMLMM
            3 3 E
            MMRMMRMRRM
            Expected Output:
            1 3 N
            5 1 E
            */
            target
                .SetSize(5, 5)
                .DeployRover(1, 2, 'N')
                .SetInstruction("LMLMLMLMM", timer)
                .EndConfigRover()
                .DeployRover(3, 3, 'E')
                .SetInstruction("MMRMMRMRRM", timer);

            return target;
        }
    }
}
