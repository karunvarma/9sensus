namespace Raft
{
    internal class TimerModule
    {
        private int electionTimeout;

        public TimerModule(int electionTimeout)
        {
            this.electionTimeout = electionTimeout;
        }
    }
}