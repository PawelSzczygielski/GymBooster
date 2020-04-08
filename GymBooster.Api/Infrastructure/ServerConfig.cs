using GymBooster.DatabaseAccess;

namespace GymBooster.Api.Infrastructure
{
    public class ServerConfig
    {
        public MongoDbConfig MongoDb { get; set; } = new MongoDbConfig();
    }
}
