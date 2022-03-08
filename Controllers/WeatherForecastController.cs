using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InqService.Repository;

namespace InqService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Get([FromBody] JsonElement requestBody)
        {
            return Ok(new Dictionary<string, string>()
                {
                    { "date", "2022-02-15T21:08:49.7681375+07:00" },
                    { "temperatureC", "-5C" },
                    { "temperatureF", "24" },
                    { "summary", "Bracing" }
                });
        }
    }
}
