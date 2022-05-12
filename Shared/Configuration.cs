using Serilog.Events;

namespace Shared
{
    public class SerilogConfig
        {
            public LogEventLevel LogEventLevel { get; set; }

            public string LogDirectory { get; set; }

            public string OutputTemplate { get; set; }

            public int FileSizeLimitBytes { get; set; }

            public int RetainedFileCountLimit { get; set; }
        
    }
}
