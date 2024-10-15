using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class SerilogMsSqlLogger : SerilogLoggerServiceBase
{
    public SerilogMsSqlLogger(MsSqlConfiguration msSqlConfiguration) : base(logger: null!)
    {
        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName = msSqlConfiguration.TableName,
            AutoCreateSqlDatabase = msSqlConfiguration.AutoCreateSqlTable
        };

        ColumnOptions columnOptions = new();

        Logger seriLogConfig = new LoggerConfiguration().WriteTo
            .MSSqlServer(msSqlConfiguration.ConnectionString, sinkOptions, columnOptions: columnOptions)
            .CreateLogger();

        Logger = seriLogConfig;
    }
}
