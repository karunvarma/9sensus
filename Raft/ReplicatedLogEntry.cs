using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raft
{
    class ReplicatedLogEntry
    {
        public long Index { get; set; }
        public long Term { get; set; }
        public string Command { get; set; }

        public ReplicatedLogEntry(long index, long term, string command)
        {
            Index = index;
            Term = term;
            Command = command;
        }
    }
}
