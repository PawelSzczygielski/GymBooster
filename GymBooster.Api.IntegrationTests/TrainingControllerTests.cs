using System.Net.Http;
using System.Threading.Tasks;
using GymBooster.Api.DTO;
using GymBooster.Api.Infrastructure;
using GymBooster.Api.IntegrationTests.Infrastructure;
using GymBooster.CommonUtils;
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
            var trainingToAdd = new CreateTrainingDTO("Training1", "ExemplaryContent");
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
            Assert.Equal(trainingToAdd.Title, addedTraining.Title);
            Assert.Equal(trainingToAdd.Content, addedTraining.Content);
        }

        [Fact]
        public async Task Training_Is_Stored_And_Can_Be_Obtained()
        {
            // Arrange
            var postRequest = new
            {
                Url = "api/Trainings",
                Body = new
                {
                    Title = "Training1",
                    Content = "Exemplary content"
                }
            };

            var postResponse = await _httpClient.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var stringPostResponse = await postResponse.Content.ReadAsStringAsync();
            var addedTraining = stringPostResponse.DeserializeJson<TrainingDTO>();
            postResponse.EnsureSuccessStatusCode();

            var getRequestPath = $"api/Trainings/{addedTraining.Id}";

            HttpResponseMessage getResponse = await _httpClient.GetAsync(getRequestPath);

            getResponse.EnsureSuccessStatusCode();
            string stringGetResponse = await getResponse.Content.ReadAsStringAsync();
            var obtainedTraining = stringGetResponse.DeserializeJson<TrainingDTO>();

            Assert.Equal(addedTraining, obtainedTraining);
        }
    }
}