using MongoDB.Driver;

namespace GymBooster.Api
{
    public interface ITrainingContext
    {
        IMongoCollection<Training> Trainings { get; }
    }

    public class TrainingContext : ITrainingContext
    {
        private readonly IMongoDatabase _db;
        public TrainingContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<Training> Trainings => _db.GetCollection<Training>("Trainings");
    }
}
