using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// BUG: No User-Agent header configured — the NWS API requires one and will return 403 without it.
builder.Services.AddHttpClient<WeatherService>(client =>
{
    client.BaseAddress = new Uri("https://api.weather.gov");
    client.DefaultRequestHeaders.Accept.ParseAdd("application/geo+json");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/weather/{zone}", async (string zone, WeatherService weatherService) =>
{
    try
    {
        var forecast = await weatherService.GetForecastAsync(zone);
        return Results.Ok(forecast);
    }
    catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return Results.NotFound(new { error = $"Zone '{zone}' not found." });
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
.WithSummary("Get the NWS forecast for a given zone")
.WithDescription("Retrieves the weather forecast from the National Weather Service API for the specified forecast zone.")
.Produces<WeatherApi.Models.ForecastResponse>(200)
.Produces(404)
.Produces(502);

app.MapGet("/weather/alerts/{zone}", async (string zone, WeatherService weatherService) =>
{
    try
    {
        var alerts = await weatherService.GetAlertsAsync(zone);
        return Results.Ok(alerts);
    }
    catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return Results.NotFound(new { error = $"Zone '{zone}' not found." });
    }
    catch (HttpRequestException ex)
    {
        return Results.Problem(
            detail: ex.Message,
            statusCode: (int?)ex.StatusCode ?? 502,
            title: "Weather API Error");
    }
})
.WithName("GetWeatherAlerts")
.WithSummary("Get active weather alerts for a given zone")
.WithDescription("Retrieves active weather alerts from the National Weather Service API for the specified zone.")
.Produces<WeatherApi.Models.AlertsResponse>(200)
.Produces(404)
.Produces(502);

app.Run();
