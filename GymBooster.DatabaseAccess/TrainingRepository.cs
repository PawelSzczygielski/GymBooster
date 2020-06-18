using System.Collections.Generic;
using System.Threading.Tasks;
using GymBooster.DatabaseAccess.DbModel;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GymBooster.DatabaseAccess
{
    public interface ITrainingRepository
    {
        Task<IEnumerable<TrainingDbModel>> GetAllTrainings();
        Task<TrainingDbModel> GetTraining(string id);
        Task<TrainingDbModel> Create(TrainingDbModel training);
        Task<bool> Update(TrainingDbModel training);
        Task<bool> Delete(string id);
        Task<long> GetNextId();
        Task<bool> Exists(string trainingId);
    }

    public class TrainingRepository : ITrainingRepository
    {
        private readonly ITrainingContext _context;
        public TrainingRepository(ITrainingContext context)

        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingDbModel>> GetAllTrainings()
        {
            return await _context
                .Trainings
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<TrainingDbModel> GetTraining(string id)
        {
            var idParsedOk = ObjectId.TryParse(id, out ObjectId trainingId);
            if (!idParsedOk)
                return Task.FromResult<TrainingDbModel>(null);

            FilterDefinition<TrainingDbModel> filter = Builders<TrainingDbModel>
                .Filter.Eq(m => m.Id, trainingId);
            return _context
                    .Trainings
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task<TrainingDbModel> Create(TrainingDbModel trainingDb)
        {
            if (trainingDb.Id == ObjectId.Empty)
                trainingDb.Id = ObjectId.GenerateNewId();

            await _context.Trainings.InsertOneAsync(trainingDb);
            return trainingDb;
        }

        public async Task<bool> Update(TrainingDbModel training)
        {
            ReplaceOneResult updateResult =
                await _context
                    .Trainings
                    .ReplaceOneAsync(
                        g => g.Id == training.Id,
                        training);

            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount == 1;
        }
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<TrainingDbModel> filter = Builders<TrainingDbModel>.Filter.Eq(m => m.Id, new ObjectId(id));
            DeleteResult deleteResult = await _context
                                                .Trainings
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount == 1;
        }
        public async Task<long> GetNextId()
        {
            return await _context.Trainings.CountDocumentsAsync(new BsonDocument()) + 1;
        }

        public async Task<bool> Exists(string trainingId)
        {
            var trainingCount = await _context.Trainings.CountDocumentsAsync(t => t.Id == new ObjectId(trainingId));
            return trainingCount > 0;
        }
    }
}