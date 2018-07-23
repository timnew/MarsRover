using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windy.IO
{
    public static class AnswersExtension
    {
        public static Answers SetBit(this Answers state, Answers bit)
        {
            return state | bit;
        }

        public static Answers ClearBit(this Answers state, Answers bit)
        {
            return state & (~bit);
        }

        public static Answers ToggleBit(this Answers state, Answers bit)
        {
            return state ^ bit;
        }

        public static Answers SetValue(this Answers state, Answers bit, bool value)
        {
            return value ? SetBit(state, bit) : ClearBit(state, bit);
        }

        public static Answers SetValue(this Answers state, Answers bit, bool? value)
        {
            return value.HasValue ? SetValue(state, bit, (bool)value) : state;
        }

        public static bool TestBit(this Answers state, Answers bit)
        {
            return (state & bit) == bit;
        }

        public static bool TestMask(this Answers state, Answers bit)
        {
            return (state & bit) != default(Answers);
        }
    }
}
