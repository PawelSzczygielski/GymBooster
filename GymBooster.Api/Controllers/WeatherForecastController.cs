using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBooster.CommonUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GymBooster.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries;
        private static readonly SortedDictionary<DateTime, WeatherForecast> wholeForecast;

        static WeatherForecastController()
        {
            Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            Random rng = new Random();
            wholeForecast = new SortedDictionary<DateTime, WeatherForecast>(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToDictionary(k => k.Date.Date, v => v));
        }


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            return Ok(wholeForecast.Values);
        }

        [HttpGet("{date}")]
        public ActionResult<WeatherForecast> Get([DateTimeModelBinder(DateFormat = GlobalConstants.UnifiedDateFormat)] DateTime? date)
        {
            _logger.LogWarning("");
            var prognosisFound = wholeForecast.TryGetValue(date.Value.Date, out WeatherForecast dailyPrognosis);
            if (!prognosisFound)
                NotFound(date);

            return Ok(dailyPrognosis);
        }

        [HttpPost]
        public ActionResult<WeatherForecast> PostWeatherForecast(WeatherForecast prognosis)
        {
            if (wholeForecast.ContainsKey(prognosis.Date))
                return UnprocessableEntity($"Forecast for {prognosis.Date} already exist");

            wholeForecast.Add(prognosis.Date.Date, prognosis);
            return CreatedAtAction(nameof(PostWeatherForecast), new { prognosis.Date.Date }, prognosis);
        }

        [HttpPut]
        public ActionResult<WeatherForecast> PutWeatherForecast(WeatherForecast prognosis)
        {
            if (!ModelState.IsValid) //todo investigate: what's this and how to use it? https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api
            {
                BadRequest();
            }

            wholeForecast[prognosis.Date] = prognosis;
            return Ok(prognosis);
        }

        [HttpDelete("{date}")]
        public ActionResult DeleteWeatherForecast([DateTimeModelBinder(DateFormat = GlobalConstants.UnifiedDateFormat)] DateTime? date)
        {
            if (!wholeForecast.ContainsKey(date.Value))
                return NotFound();

            wholeForecast.Remove(date.Value);
            return NoContent();
        }
    }
}
