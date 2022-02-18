
using Serilog;
using System.Runtime.CompilerServices;
using Serilog.Sinks.Datadog.Logs;

namespace LoggerAPI.Common
{
    public static class LoggerExtensions
    {
        public static ILogger Enrich(this ILogger logger,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return logger
                .ForContext("MemberName", memberName)
                .ForContext("LineNumber", sourceLineNumber);
        }

        public static ILogger Logger()
        {
            return new LoggerConfiguration().WriteTo.DatadogLogs(
                "81b5ac49dd9d1ee9fc549734b6cf59a8",
                service: "OrderCreation",
                source: "Devlopment",
                host: "localhost"
            ).CreateLogger();
        }
    }
}
