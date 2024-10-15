using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using MongoDB.Driver;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class SerilogMongoDbLogger : SerilogLoggerServiceBase
{
    public SerilogMongoDbLogger(MongoDbConfiguration mongoDbConfiguration) : base(logger: null!)
    {
        Logger = new LoggerConfiguration().WriteTo
            .MongoDB(mongoDbConfiguration.ConnectionString, collectionName: mongoDbConfiguration.Collection)
            .CreateLogger();
    }
}
