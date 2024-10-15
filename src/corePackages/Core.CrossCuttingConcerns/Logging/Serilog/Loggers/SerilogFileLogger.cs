using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class SerilogFileLogger : SerilogLoggerServiceBase
{
    public SerilogFileLogger(FileLogConfiguration fileLogConfiguration) : base(logger: null!)
    {
        Logger = new LoggerConfiguration().WriteTo
            .File(path: $"{Directory.GetCurrentDirectory() + fileLogConfiguration.FolderPath}.txt",
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: null,
            fileSizeLimitBytes: 5000000,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}
