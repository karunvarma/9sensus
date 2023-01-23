using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raft
{
    internal interface IRaftClusterService
    {
        Task<bool> RequestVote(string candidateId, long term, long lastLogIndex, long lastLogTerm);

        Task AppendEntries(string leaderId, long term, long prevLogIndex, long prevLogTerm, ReplicatedLogEntry[] entries, long leaderCommit);
    }
}
