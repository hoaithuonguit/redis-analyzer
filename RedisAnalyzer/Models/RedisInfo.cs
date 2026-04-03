namespace RedisAnalyzer.Models
{
    public class RedisInfo
    {
        public long UsedMemory { get; set; }
        public long MaxMemory { get; set; }
        public string EvictionPolicy { get; set; } = "";
        public int ConnectedClients { get; set; }
        public long EvictedKeys { get; set; }
        public int SlowLogCount { get; set; }
        public bool HasSlowCommands { get; set; }
        public double MemFragmentationRatio { get; set; }
        public bool RdbEnabled { get; set; }
        public bool AofEnabled { get; set; }
        public long UptimeInSeconds { get; set; }
    }
}