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

            var HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("https://10.0.2.2:5001/gymbooster/");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var getRequestPath = $"api/Trainings/";

            HttpResponseMessage getResponse = await HttpClient.GetAsync(getRequestPath);

            return null;
        }

        public async Task<IEnumerable<TrainingDTO>> GetItemsAsync(bool forceRefresh = false)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var httpClient = new HttpClient(httpClientHandler);
            
            try
            {
                HttpResponseMessage getResponse =
                    await httpClient.GetAsync(new Uri("https://10.0.2.2:32768/api/Trainings/"));
                getResponse.EnsureSuccessStatusCode();
                string stringGetResponse = await getResponse.Content.ReadAsStringAsync();
                var obtainedTraining = stringGetResponse.DeserializeJson<List<TrainingDTO>>();
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}