using RedisAnalyzer.Services;

var argsDict = args
    .Select(a => a.Split('='))
    .ToDictionary(a => a[0].TrimStart('-'), a => a.Length > 1 ? a[1] : "");

if (!argsDict.ContainsKey("host"))
{
    Console.WriteLine("Usage:");
    Console.WriteLine("redis-analyzer --host=HOST --port=6379 --user=USERNAME --pass=PASSWORD");
    return;
}

var host = argsDict["host"];
var port = argsDict.GetValueOrDefault("port", "6379");
var user = argsDict.GetValueOrDefault("user", "");
var pass = argsDict.GetValueOrDefault("pass", "");

string connection;

// 🔥 Build connection string chuẩn
if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
{
    connection = $"{host}:{port},user={user},password={pass}";
}
else if (!string.IsNullOrEmpty(pass))
{
    // Redis cũ (không có ACL)
    connection = $"{host}:{port},password={pass}";
}
else
{
    connection = $"{host}:{port}";
}

Console.WriteLine($"Connecting to {host}:{port}...");

try
{
    var collector = new RedisCollector();
    var info = await collector.CollectAsync(connection);

    var analyzer = new RedisAnalyzerService();
    var issues = analyzer.Analyze(info);

    var formatter = new RedisReportFormatter();
    Console.WriteLine(formatter.Format(issues));
}
catch (Exception ex)
{
    Console.WriteLine("❌ Failed to connect or analyze Redis");
    Console.WriteLine(ex.Message);
}