using GymBooster.DatabaseAccess.DbModel;
using MongoDB.Driver;

namespace GymBooster.DatabaseAccess
{
    public interface ITrainingContext
    {
        IMongoCollection<TrainingDbModel> Trainings { get; }
    }

    public class TrainingContext : ITrainingContext
    {
        private readonly IMongoDatabase _db;
        public TrainingContext(MongoDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<TrainingDbModel> Trainings => _db.GetCollection<TrainingDbModel>("Trainings");
    }
}
