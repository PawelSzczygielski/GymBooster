using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using GymBooster.Api.Infrastructure;
using GymBooster.Api.IntegrationTests.Infrastructure;
using GymBooster.Common.Objects.DTO;
using GymBooster.Common.Utils;
using MongoDB.Bson;
using Xunit;

namespace GymBooster.Api.IntegrationTests
{
    public class TrainingControllerTests : IClassFixture<TestConfigurator<GymBoosterStartup>>
    {
        private HttpClient _httpClient;

        public TrainingControllerTests(TestConfigurator<GymBoosterStartup> fixture)
        {
            _httpClient = fixture.HttpClient;
        }

        [Fact]
        public async Task Training_Can_Be_Added()
        {
            var createdOnUtc = new DateTime(2020, 10, 14, 21, 04, 32, DateTimeKind.Utc);

            var trainingToAdd = new TrainingDTO(string.Empty, "Training1", createdOnUtc, new List<ExerciseDTO>());
            // Arrange
            var request = new
            {
                Url = "api/Trainings",
                Body = trainingToAdd
            };

            // Act
            var response = await _httpClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var responseString = await response.Content.ReadAsStringAsync();
            TrainingDTO addedTraining = responseString.DeserializeJson<TrainingDTO>();

            // Assert
            response.EnsureSuccessStatusCode();
            trainingToAdd.Title.Should().BeEquivalentTo(addedTraining.Title);
        }

        [Fact]
        public async Task Training_Is_Stored_And_Can_Be_Obtained()
        {
            var postRequest = new
            {
                Url = "api/Trainings",
                Body = new
                {
                    Title = "Training1"
                }
            };

            var postResponse =
                await _httpClient.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var stringPostResponse = await postResponse.Content.ReadAsStringAsync();
            var addedTraining = stringPostResponse.DeserializeJson<TrainingDTO>();
            postResponse.EnsureSuccessStatusCode();

            var getRequestPath = $"api/Trainings/{addedTraining.Id}";

            HttpResponseMessage getResponse = await _httpClient.GetAsync(getRequestPath);

            getResponse.EnsureSuccessStatusCode();
            string stringGetResponse = await getResponse.Content.ReadAsStringAsync();
            var obtainedTraining = stringGetResponse.DeserializeJson<TrainingDTO>();

            addedTraining.Should().BeEquivalentTo(obtainedTraining);
        }

        [Fact]
        public async void Training_Can_Be_Updated()
        {

            var postRequest = new
            {
                Url = "api/Trainings",
                Body = new
                {
                    Title = "Training1"
                }
            };

            var postResponse =
                await _httpClient.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var postStringContent = await postResponse.Content.ReadAsStringAsync();
            var addedTraining = postStringContent.DeserializeJson<TrainingDTO>();

            var createdOnUtc = new DateTime(2020, 10, 14, 21, 04, 32, DateTimeKind.Utc);
            TrainingDTO trainingToUpdate = new TrainingDTO(addedTraining.Id, "updatedTitle", createdOnUtc, new List<ExerciseDTO>());
            var putRequest = new
            {
                Url = $"api/Trainings/{trainingToUpdate.Id}",
                Body = trainingToUpdate
            };

            var putResponse =
                await _httpClient.PutAsync(putRequest.Url, ContentHelper.GetStringContent(putRequest.Body));

            putResponse.EnsureSuccessStatusCode();
            var putStringContent = await putResponse.Content.ReadAsStringAsync();
            var updatedTraining = putStringContent.DeserializeJson<TrainingDTO>();

            trainingToUpdate.Should().BeEquivalentTo(updatedTraining);
        }

        [Fact]
        public async void Querying_For_NonExisting_Training_Returns_Proper_Core()
        {
            var getRequestPath = $"api/Trainings/NonExistingId";

            HttpResponseMessage getResponse = await _httpClient.GetAsync(getRequestPath);

            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            string stringGetResponse = await getResponse.Content.ReadAsStringAsync();
            var obtainedTraining = stringGetResponse.DeserializeJson<TrainingDTO>();

            obtainedTraining.Should().BeNull();
        }

        [Fact]
        public async void Put_Can_Add_Training_If_Send_With_Valid_Id()
        {
            var createdOnUtc = new DateTime(2020, 10, 14, 21, 04, 32, DateTimeKind.Utc);

            var id = ObjectId.GenerateNewId();
            TrainingDTO trainingToUpdate = new TrainingDTO(id.ToString(), "updatedTitle", createdOnUtc, new List<ExerciseDTO>());
            var putRequest = new
            {
                Url = $"api/Trainings/{trainingToUpdate.Id}",
                Body = trainingToUpdate
            };

            var putResponse =
                await _httpClient.PutAsync(putRequest.Url, ContentHelper.GetStringContent(putRequest.Body));

            putResponse.EnsureSuccessStatusCode();
            var putStringContent = await putResponse.Content.ReadAsStringAsync();
            var updatedTraining = putStringContent.DeserializeJson<TrainingDTO>();

            trainingToUpdate.Should().BeEquivalentTo(updatedTraining);
        }

        [Fact]
        public async void Series_Can_Be_Added_To_Training()
        {
            var createdOnUtc = new DateTime(2020, 10, 14, 21, 04, 32, DateTimeKind.Utc);

            var id = ObjectId.GenerateNewId();
            TrainingDTO trainingToUpdate = new TrainingDTO(id.ToString(), "updatedTitle", createdOnUtc, new List<ExerciseDTO>
            {
                new ExerciseDTO("DeadLift",
                    new List<SeriesDTO> {new SeriesDTO(6, 200, "Too fast")})
            });
            
            var putRequest = new
            {
                Url = $"api/Trainings/{trainingToUpdate.Id}",
                Body = trainingToUpdate
            };

            var putResponse =
                await _httpClient.PutAsync(putRequest.Url, ContentHelper.GetStringContent(putRequest.Body));

            putResponse.EnsureSuccessStatusCode();
            var putStringContent = await putResponse.Content.ReadAsStringAsync();
            var updatedTraining = putStringContent.DeserializeJson<TrainingDTO>();

            updatedTraining.Should().BeEquivalentTo(trainingToUpdate);
        }
    }
}