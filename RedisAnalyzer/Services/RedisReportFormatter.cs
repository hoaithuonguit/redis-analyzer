using System.Text;
using RedisAnalyzer.Models;

namespace RedisAnalyzer.Services
{
    public class RedisReportFormatter
    {
        public string Format(List<RedisIssue> issues)
        {
            var sb = new StringBuilder();

            void Write(string title, List<RedisIssue> list)
            {
                if (!list.Any()) return;
                sb.AppendLine(title);
                foreach (var i in list)
                    sb.AppendLine($"- {i.Message}");
                sb.AppendLine();
            }

            Write("CRITICAL", issues.Where(i => i.Severity == Severity.Critical).ToList());
            Write("WARNING", issues.Where(i => i.Severity == Severity.Warning).ToList());
            Write("INFO", issues.Where(i => i.Severity == Severity.Info).ToList());

            if (!issues.Any())
                sb.AppendLine("🟢 HEALTHY");

            return sb.ToString();
        }
    }
}