using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Windy;
using Windy.Reflection;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public static class RoverActionPool
    {
        private static RoverActionCollection Actions;

        static RoverActionPool()
        {
            Actions = new RoverActionCollection();

            InitiailizeActions(Actions);
        }

        private static void InitiailizeActions(RoverActionCollection actions)
        {
            Assembly
                .GetExecutingAssembly()
                .GetTypesByAncestor<IRoverAction>(true)
                .CreateInstance<IRoverAction>()
                .ForEach(Actions.Add);
        }

        public static void AddActions(Assembly hostAssembly)
        {
            Contract.Requires<ArgumentNullException>(hostAssembly != null, "hostAssembly");

            hostAssembly
                .GetTypesByAncestor<IRoverAction>(true)
                .CreateInstance<IRoverAction>()
                .ForEach(Actions.Add);
        }

        public static void AddActions(IEnumerable<IRoverAction> actions)
        {
            Contract.Requires<ArgumentNullException>(actions != null, "actions");

            foreach (var fo in actions)
            {
                if (fo == null)
                    throw new ArgumentNullException("Action in actions is null");

                Actions.Add(fo);
            }
        }

        public static IRoverAction GetAction(char key)
        {
            return Actions[key];
        }

        public static IEnumerable<IRoverAction> ParseAsInstructions(this IEnumerable<char> input, bool ignoreInvalidInstruction = true)
        {
            Contract.Requires(input != null);
            Contract.Ensures(Contract.Result<IEnumerable<IRoverAction>>() != null);

            foreach (var fo in input)
            {
                IRoverAction result;

                if (!RoverActionPool.Actions.TryGetValue(fo, out result))
                {
                    if (!ignoreInvalidInstruction)
                    {
                        throw new ArgumentOutOfRangeException("input", "Invalid instruction char '{0}'".ApplyFormat(fo));
                    }
                    else
                    {
                        continue;
                    }
                }
                yield return result;
            }
        }
    }
}
