using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raft
{
    public class RaftServerConfig
    {
        public string ServerIP { get; set; }
        public int ServerPort { get; set; }

        public RaftServerConfig(string serverIP, int serverPort)
        {
            ServerIP = serverIP;
            ServerPort = serverPort;
        }
    }
}
