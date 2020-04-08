using FluentAssertions;
using GymBooster.Api.IntegrationTests.Infrastructure;
using GymBooster.CommonUtils;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using GymBooster.Api.DTO;
using GymBooster.Api.Infrastructure;
using Xunit;

namespace GymBooster.Api.IntegrationTests
{
    public class WeatherForecastControllerTests : IClassFixture<TestConfigurator<GymBoosterStartup>>
    {
        private HttpClient _httpClient;
                
        public WeatherForecastControllerTests(TestConfigurator<GymBoosterStartup> fixture)
        {
            _httpClient = fixture.HttpClient;
        }

        [Fact]
        public async Task GetWholeWeatherPrognosis_Returns_All_Data()
        {
            string requestPath = "/WeatherForecast";

            HttpResponseMessage response = await _httpClient.GetAsync(requestPath);

            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetPrognosisForOneDay_Returns_One_Day()
        {
            var date = DateTime.Now.Date.AddDays(1).ToString(GlobalConstants.UnifiedDateFormat);
            var requestPath = $"/WeatherForecast/{date}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestPath);

            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Prognosis_Can_Be_Added()
        {
            // Arrange
            var request = new
            {
                Url = $"/WeatherForecast",
                Body = new
                {
                    Date = DateTime.UtcNow.AddDays(8),
                    Summary = "Cold",
                    TemperatureC = 3
                }
            };

            // Act
            var response = await _httpClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Prognosis_Can_Be_Updated()
        {
            // Arrange
            var request = new
            {
                Url = $"/WeatherForecast/",
                Body = new
                {
                    Date = DateTime.Now.AddDays(1).Date,
                    Summary = "Cold",
                    TemperatureC = 3
                }
            };

            // Act
            var response = await _httpClient.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Prognosis_Can_Be_Deleted()
        {
            // Arrange

            var postRequest = new
            {
                Url = $"/WeatherForecast/",
                Body = new
                {
                    Date = DateTime.Now.AddDays(8).Date,
                    Summary = "Cold",
                    TemperatureC = 3
                }
            };

            // Act
            var postResponse = await _httpClient.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var weatherForecast = JsonConvert.DeserializeObject<WeatherForecastDTO>(jsonFromPostResponse);

            var deleteResponse = await _httpClient.DeleteAsync($"/WeatherForecast/{weatherForecast.Date.ToString(GlobalConstants.UnifiedDateFormat)}");

            // Assert
            postResponse.EnsureSuccessStatusCode();            
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}
