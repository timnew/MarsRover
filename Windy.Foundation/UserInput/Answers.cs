using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windy.IO
{
    [Flags]
    public enum Answers
    {
        None = 0x00,
        Retry = 0x01,
        Ignore = 0x02,
        Abort = 0x04,

        Delete = 0x08,
        Quit = 0x10,

        RIA = Retry | Ignore | Abort,
        IA = Ignore | Abort,
        IDA = Ignore | Delete | Abort,
        RQ = Retry | Quit,
    }
}
