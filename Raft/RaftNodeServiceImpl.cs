using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raft
{
    class RaftNodeServiceImpl : RaftNodeService.RaftNodeServiceBase
    {
        private RaftNode _raftNode;

        public RaftNodeServiceImpl(RaftNode raftNode) {
            _raftNode = raftNode;
        }

        // leader will send it the followers
        public override async Task<AppendEntriesResponse> AppendEntries(AppendEntriesRequest request, ServerCallContext context)
        {
            return null;
        }

        // candidate will send to all other nodes
        public override async Task<RequestVoteResponse> RequestVote(RequestVoteRequest request, ServerCallContext context)
        {

            return await _raftNode.RequestVote(request);
        }
    }

}
