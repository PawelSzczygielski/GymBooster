using System;
using System.Collections.Generic;
using System.Linq;
using GymBooster.Api.DTO;
using GymBooster.CommonUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GymBooster.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly SortedDictionary<DateTime, WeatherForecastDTO> WholeForecast;

        static WeatherForecastController()
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            Random rng = new Random();
            WholeForecast = new SortedDictionary<DateTime, WeatherForecastDTO>(Enumerable.Range(1, 5).Select(index => new WeatherForecastDTO
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            }).ToDictionary(k => k.Date.Date, v => v));
        }


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecastDTO>> Get()
        {
            return Ok(WholeForecast.Values);
        }

        [HttpGet("{date}")]
        public ActionResult<WeatherForecastDTO> Get([DateTimeModelBinder(DateFormat = GlobalConstants.UnifiedDateFormat)] DateTime? date)
        {
            _logger.LogWarning("");
            var prognosisFound = WholeForecast.TryGetValue(date.Value.Date, out WeatherForecastDTO dailyPrognosis);
            if (!prognosisFound)
                NotFound(date);

            return Ok(dailyPrognosis);
        }

        [HttpPost]
        public ActionResult<WeatherForecastDTO> PostWeatherForecast(WeatherForecastDTO prognosis)
        {
            if (WholeForecast.ContainsKey(prognosis.Date))
                return UnprocessableEntity($"Forecast for {prognosis.Date} already exist");

            WholeForecast.Add(prognosis.Date.Date, prognosis);
            return CreatedAtAction(nameof(PostWeatherForecast), new { prognosis.Date.Date }, prognosis);
        }

        [HttpPut]
        public ActionResult<WeatherForecastDTO> PutWeatherForecast(WeatherForecastDTO prognosis)
        {
            if (!ModelState.IsValid) //todo investigate: what's this and how to use it? https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api
            {
                BadRequest();
            }

            WholeForecast[prognosis.Date] = prognosis;
            return Ok(prognosis);
        }

        [HttpDelete("{date}")]
        public ActionResult DeleteWeatherForecast([DateTimeModelBinder(DateFormat = GlobalConstants.UnifiedDateFormat)] DateTime? date)
        {
            if (!WholeForecast.ContainsKey(date.Value))
                return NotFound();

            WholeForecast.Remove(date.Value);
            return NoContent();
        }
    }
}
