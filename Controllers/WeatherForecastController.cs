using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIVersioning.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0", Deprecated = true)]
[ApiVersion("2.0")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }


   [HttpGet]
   [MapToApiVersion("1.0")]
   [Authorize]
    public WeatherForecast GetOld()
    {
        var NewWeather = new WeatherForecast
        {


            Summary = "Old Weather",
            TemperatureC = 22,
            Date = DateOnly.FromDateTime(DateTime.Now)
        };

        return NewWeather;
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    [Authorize]
    public WeatherForecast GetNew()
    {
        var NewWeather = new WeatherForecast
        {


            Summary = "New Weather",
            TemperatureC = 22,
            Date = DateOnly.FromDateTime(DateTime.Now)
        };

        return NewWeather;
    }

}
