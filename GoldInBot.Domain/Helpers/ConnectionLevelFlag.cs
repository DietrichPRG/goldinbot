using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldInBot.Domain.Helpers
{
    [Flags]
    public enum ConnectionLevelFlag : byte
    {
        None = 0,
        First = 1 << 1,
        Second = 1 << 2,
        Third = 1 << 3,
    }
}
