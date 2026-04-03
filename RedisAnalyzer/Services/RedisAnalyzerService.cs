using RedisAnalyzer.Models;

namespace RedisAnalyzer.Services
{
    public class RedisAnalyzerService
{
    public List<RedisIssue> Analyze(RedisInfo info)
    {
        var issues = new List<RedisIssue>();

        double ratio = info.MaxMemory > 0 ? (double)info.UsedMemory / info.MaxMemory : 0;

        if (ratio > 0.9 && info.EvictionPolicy == "noeviction")
            issues.Add(new() { Severity = Severity.Critical, Message = "Memory >90% & noeviction → writes may fail" });

        if (info.MaxMemory > 0 && info.UsedMemory >= info.MaxMemory)
            issues.Add(new() { Severity = Severity.Critical, Message = "Memory limit reached" });

        if (info.ConnectedClients > 1000)
            issues.Add(new() { Severity = Severity.Critical, Message = "Too many clients (>1000)" });

        if (ratio > 0.8)
            issues.Add(new() { Severity = Severity.Warning, Message = "Memory usage >80%" });

        if (info.EvictedKeys > 0)
            issues.Add(new() { Severity = Severity.Warning, Message = "Keys are being evicted" });

        if (info.SlowLogCount > 0)
            issues.Add(new() { Severity = Severity.Warning, Message = "Slow commands detected" });

        if (info.MemFragmentationRatio > 1.5)
            issues.Add(new() { Severity = Severity.Warning, Message = "High memory fragmentation" });

        if (info.HasSlowCommands) 
            issues.Add(new() { Severity = Severity.Warning, Message = "Slow commands > 100ms detected"});


        if (info.MaxMemory == 0)
            issues.Add(new() { Severity = Severity.Info, Message = "No maxmemory set" });

        if (!info.RdbEnabled && !info.AofEnabled)
            issues.Add(new() { Severity = Severity.Info, Message = "No persistence configured" });

        if (info.UptimeInSeconds < 3600)
            issues.Add(new() { Severity = Severity.Info, Message = "Recently restarted" });

        return issues;
    }
}
}