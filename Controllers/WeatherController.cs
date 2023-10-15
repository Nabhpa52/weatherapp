using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json.Linq;

public class WeatherController : Controller
{
    private readonly string apiKey = "fdc26c3169ca462e1d5004d6dd5ac994";

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet] // Add this attribute to specify that this action responds to HTTP GET requests
    public IActionResult GetWeather(string city)
    {
        var client = new RestClient($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}");
        var request = new RestRequest("",Method.Get);
        var response = client.Execute(request);

        if (response.IsSuccessful)
        {
            var data = JObject.Parse(response.Content);
            ViewBag.City = data["name"];
            ViewBag.Temperature = data["main"]["temp"];
            ViewBag.Description = data["weather"][0]["description"];
            return PartialView("_WeatherPartial");
        }

        ViewBag.Error = "Error fetching weather data.";
        return PartialView("_ErrorPartial");
    }
}
