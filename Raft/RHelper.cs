using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raft
{
    /// <summary>
    /// A RHelper class in C# is a helper class that contains a collection of small, reusable functions or properties for Raft consensus algorithm
    /// </summary>
    class RHelper
    {
        public static int DefaultVote = -1;
        public static int DefaultElectionTimeoutInMinutes = 1;


        /* if one server’s current
term is smaller than the other’s, then it updates its current
term to the larger value*/
        internal static bool CheckAndExchangeCurrentTerm(RaftNodeState raftNodeState, RequestVoteRequest candidateRequest)
        {
            if (candidateRequest.Term > raftNodeState.CurrentTerm)
            {
                raftNodeState.CurrentTerm = candidateRequest.Term;
                return true;
            }
            return false;
        }

        //        If votedFor is null or candidateId, and candidate’s log is at
        //least as up-to-date as receiver’s log, grant vote
        internal static bool isCandidateLogUptoDate(RaftNodeState raftNodeState, RequestVoteRequest candidateRequest)
        {
            return false;
        }

        internal static bool isDefaultVote(RaftNodeState raftNodeState)
        {
            return raftNodeState.VotedFor == DefaultVote;
        }

    }
}
