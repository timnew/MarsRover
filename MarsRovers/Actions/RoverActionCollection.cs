using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class RoverActionCollection : KeyedCollection<char, IRoverAction>
    {
        public RoverActionCollection()
            : base(EqualityComparer<char>.Default, 0)
        { }

        protected override char GetKeyForItem(IRoverAction item)
        {
            return item.ActionId;
        }

        public bool TryGetValue(char key, out IRoverAction result)
        {
            if (base.Dictionary != null)
            {
                return base.Dictionary.TryGetValue(key, out result);
            }
            else
            {
                if (base.Contains(key))
                {
                    result = base[key];
                    return true;
                }
                else
                {
                    result = null;
                    return false;
                }
            }
        }
    }
}
