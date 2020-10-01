using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GymBooster.Common.Objects.DTO;
using GymBooster.Common.Utils;

namespace GymBooster.AndroidApp.Services
{
    public class GymBoosterDataStore : IDataStore<TrainingDTO>
    {
        public Task<bool> AddItemAsync(TrainingDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(TrainingDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TrainingDTO> GetItemAsync(string id)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var httpClient = new HttpClient(httpClientHandler);

            try
            {
                HttpResponseMessage getResponse =
                    await httpClient.GetAsync(new Uri($"https://10.0.2.2:32768/api/Trainings/{id}"));
                getResponse.EnsureSuccessStatusCode();
                string stringGetResponse = await getResponse.Content.ReadAsStringAsync();
                var obtainedTraining = stringGetResponse.DeserializeJson<TrainingDTO>();
                return obtainedTraining;
            }
            catch (Exception ex)
            {
                //TODO: make this exception less general + move hadling into upper code.
                return null;
            }
        }

        public async Task<IEnumerable<TrainingDTO>> GetItemsAsync(bool forceRefresh = false)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true; //TODO: this true should probably be removed
            var httpClient = new HttpClient(httpClientHandler);
            
            try
            {
                HttpResponseMessage getResponse =
                    await httpClient.GetAsync(new Uri("https://10.0.2.2:32775/api/Trainings/"));
                getResponse.EnsureSuccessStatusCode();
                string stringGetResponse = await getResponse.Content.ReadAsStringAsync();
                var obtainedTrainings = stringGetResponse.DeserializeJson<List<TrainingDTO>>();
                return obtainedTrainings;
            }
            catch (Exception ex)
            {
                //TODO: make this exception less general + move hadling into upper code.
                return new List<TrainingDTO>();
            }
        }
    }
}