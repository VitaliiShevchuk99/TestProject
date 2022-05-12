using Serilog;
using Serilog.Core;
using Serilog.Sinks.RollingFileAlternate;

namespace Shared
{
    public static class LoggerHelper
    {
        public static Logger GetLogger(SerilogConfig configuration)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Is(configuration.LogEventLevel)
                .WriteTo.RollingFileAlternate(logDirectory: configuration.LogDirectory, outputTemplate: configuration.OutputTemplate, retainedFileCountLimit: configuration.RetainedFileCountLimit, fileSizeLimitBytes: configuration.FileSizeLimitBytes)
                .CreateLogger();
        }
    }
}
