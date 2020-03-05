using GymBooster.CommonUtils;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GymBooster.Api
{
    public class WeatherForecast
    {
        [JsonConverterAttribute(typeof(CustomDateTimeConverter), new object[] { GlobalConstants.UnifiedDateFormat})]
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [Required]
        [StringLength(100)]
        public string Summary { get; set; }
    }
}
