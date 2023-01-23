using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Raft
{
    public class RaftNode 
    {

        private readonly int _electionTimeout;
        private RaftNodeState _raftNodeState;
        private RaftServerConfig _raftServerConfig;
        private Server _server;



        public RaftNode(RaftServerConfig raftServerConfig)
        {
            _electionTimeout = RHelper.DefaultElectionTimeoutInMinutes;
            _raftNodeState = new RaftNodeState();

            _server = new Server
            {
                Services = { RaftNodeService.BindService(new RaftNodeServiceImpl(this)) },
                Ports = { new ServerPort(_raftServerConfig.ServerIP, _raftServerConfig.ServerPort, ServerCredentials.Insecure) }
            };
            _server.Start();

        }

        public void stop()
        {
            _server?.ShutdownAsync().Wait();
        }

        public Task<RequestVoteResponse> RequestVote(RequestVoteRequest candidateRequest)
        {
            RequestVoteResponse requestVoteResponse = new RequestVoteResponse();

            requestVoteResponse.VoteGranted = false;

      
            if (RHelper.CheckAndExchangeCurrentTerm(_raftNodeState, candidateRequest))
            {
                // Add logging here
            }
           
            if(RHelper.isDefaultVote(_raftNodeState)
           ||  RHelper.isCandidateLogUptoDate(_raftNodeState,candidateRequest))
            {
                requestVoteResponse.VoteGranted = true;
            }

            return Task.FromResult(requestVoteResponse);
        }
    }

    /// <summary>
    /// At any given time each server is in one of three states: leader, follower, or candidate
    /// </summary>
    enum NodeRole
    {
        LEADER,
        FOLLOWER,
        CANDIDATE
    }

    class RaftNodeState
    {
        // perisitent state on all servers

        /// <summary>
        /// latest term server has seen (initialized to 0
        /// on first boot, increases monotonically)
        /// </summary>
        public long CurrentTerm { get; set; }

        /// <summary>
        /// candidateId that received vote in current
        /// term(or null if none)
        /// </summary>
        public long VotedFor { get;  set; }


        /// <summary>
        /// LogEntries; each entry contains command
        /// for state machine, and term when entry
        /// was received by leader(first index is 1)
        /// </summary>
        public List<ReplicatedLogEntry> LogEntries { get;  set; }

        // volatile state on all servers 

        /// <summary>
        /// index of highest log entry known to be
        /// committed(initialized to 0, increases
        /// monotonically)
        /// </summary>
        public long CommitIndex {get;  set;}

        /// <summary>
        /// index of highest log entry applied to state
        /// machine(initialized to 0, increases
        /// monotonically)
        /// </summary>
        public long LastApplied {get; private set;}

        /// <summary>
        /// At any given time each server is in one of three states:
        /// leader, follower, or candidate.In normal operation there
        /// is exactly one leader and all of the other servers are followers 
        /// are passive
        /// </summary>
        public NodeRole CurrentRole { get;  set;}


        // Volatile state on leaders:
        public Dictionary<RaftNode, long> NextIndex { get;  set; }
        public Dictionary<RaftNode, long> MatchIndex { get;  set; }

        public RaftNodeState()
        {
            // every nodes starts its life as a follower
            CurrentRole = NodeRole.FOLLOWER;
            CurrentTerm = 0;
            CommitIndex = 0;
            LastApplied = 0;
            VotedFor = RHelper.DefaultVote;
            LogEntries = new List<ReplicatedLogEntry>();

            // not sure the purpose for now
            NextIndex = new Dictionary<RaftNode, long>();
            MatchIndex = new Dictionary<RaftNode, long>();
        }
    }


}
