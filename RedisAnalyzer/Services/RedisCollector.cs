using RedisAnalyzer.Models;
using StackExchange.Redis;

namespace RedisAnalyzer.Services
{
    public class RedisCollector
    {
        public async Task<RedisInfo> CollectAsync(string connection)
        {
            var option = ConfigurationOptions.Parse(connection);
            option.AllowAdmin = true;
            var mux = await ConnectionMultiplexer.ConnectAsync(option);
            var server = mux.GetServer(mux.GetEndPoints().First());

            var info = await server.InfoAsync();

            string Get(string section, string key)
            {
                return info.FirstOrDefault(s => s.Key == section)?
                    .FirstOrDefault(x => x.Key == key).Value ?? "0";
            }

            var slowlog = await server.SlowlogGetAsync(100);
            var slowLogCount = slowlog.Length;
            var hasSlow = slowlog.Any(x => x.Duration.TotalMilliseconds > 100);

            return new RedisInfo
            {
                UsedMemory = long.Parse(Get("memory", "used_memory")),
                MaxMemory = long.Parse(Get("memory", "maxmemory")),
                EvictionPolicy = Get("memory", "maxmemory_policy"),
                ConnectedClients = int.Parse(Get("clients", "connected_clients")),
                EvictedKeys = long.Parse(Get("stats", "evicted_keys")),
                SlowLogCount = slowLogCount,
                HasSlowCommands = hasSlow,
                MemFragmentationRatio = double.Parse(Get("memory", "mem_fragmentation_ratio")),
                RdbEnabled = Get("persistence", "rdb_last_save_time") != "0",
                AofEnabled = Get("persistence", "aof_enabled") == "1",
                UptimeInSeconds = long.Parse(Get("server", "uptime_in_seconds"))
            };
        }
    }
}