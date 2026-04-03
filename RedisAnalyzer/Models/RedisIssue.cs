namespace RedisAnalyzer.Models
{
    public enum Severity
    {
        Critical,
        Warning,
        Info
    }

    public class RedisIssue
    {
        public Severity Severity { get; set; }
        public string Message { get; set; } = "";
    }
}