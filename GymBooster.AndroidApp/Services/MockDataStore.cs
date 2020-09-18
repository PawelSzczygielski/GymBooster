using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBooster.Common.Objects.DTO;

namespace GymBooster.AndroidApp.Services
{
    public class MockDataStore : IDataStore<TrainingDTO>
    {
        readonly List<TrainingDTO> items;

        public MockDataStore()
        {
            items = new List<TrainingDTO>()
            {
                new TrainingDTO(Guid.NewGuid().ToString(),    "First item", new List<ExerciseDTO>()),
                new TrainingDTO (Guid.NewGuid().ToString(), "Second item", new List<ExerciseDTO>()),
                new TrainingDTO (Guid.NewGuid().ToString(), "Third item", new List<ExerciseDTO>()),
                new TrainingDTO (Guid.NewGuid().ToString(), "Fourth item", new List<ExerciseDTO>()),
                new TrainingDTO (Guid.NewGuid().ToString(), "Fifth item", new List<ExerciseDTO>()),
                new TrainingDTO (Guid.NewGuid().ToString(), "Sixth item", new List<ExerciseDTO>()),
            };
        }

        public async Task<bool> AddItemAsync(TrainingDTO item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(TrainingDTO item)
        {
            var oldItem = items.Where((TrainingDTO arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((TrainingDTO arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<TrainingDTO> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TrainingDTO>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}