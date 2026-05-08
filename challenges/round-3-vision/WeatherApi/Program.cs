using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHttpClient<WeatherService>(client =>
{
    client.BaseAddress = new Uri("https://api.weather.gov");
    client.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherApi/1.0 (codeitwithai@example.com)");
    client.DefaultRequestHeaders.Accept.ParseAdd("application/geo+json");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/weather/{city}", async (string city, WeatherService weatherService) =>
{
    try
    {
        var forecast = await weatherService.GetForecastForCityAsync(city);
        return Results.Ok(forecast);
    }
    catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return Results.NotFound(new { error = $"City '{city}' not found or no forecast available." });
    }
    catch (HttpRequestException ex)
    {
        return Results.Problem(
            detail: ex.Message,
            statusCode: (int?)ex.StatusCode ?? 502,
            title: "Weather API Error");
    }
})
.WithName("GetWeatherForecast")
.WithSummary("Get the NWS forecast for a city")
.WithDescription("Looks up coordinates for the given city and retrieves the weather forecast from the National Weather Service API.")
.Produces<WeatherApi.Models.ForecastResponse>(200)
.Produces(404)
.Produces(502);

app.Run();
