using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymBooster.Api
{
    public interface ITrainingRepository
    {
        // api/[GET]
        Task<IEnumerable<Training>> GetAllTrainings();
        // api/1/[GET]
        Task<Training> GetTraining(long id);
        // api/[POST]
        Task Create(Training training);
        // api/[PUT]
        Task<bool> Update(Training training);
        // api/1/[DELETE]
        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }

    public class TrainingRepository : ITrainingRepository
    {
        private readonly ITrainingContext _context;
        public TrainingRepository(ITrainingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Training>> GetAllTrainings()
        {
            return await _context
                .Trainings
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<Training> GetTraining(long id)
        {
            FilterDefinition<Training> filter = Builders<Training>.Filter.Eq(m => m.Id, id);
            return _context
                    .Trainings
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(Training training)
        {
            await _context.Trainings.InsertOneAsync(training);
        }

        public async Task<bool> Update(Training training)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Trainings
                        .ReplaceOneAsync(
                            filter: g => g.Id == training.Id,
                            replacement: training);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount == 1;
        }
        public async Task<bool> Delete(long id)
        {
            FilterDefinition<Training> filter = Builders<Training>.Filter.Eq(m => m.Id, id);
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
    }
}