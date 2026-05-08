using System.Text.Json;
using WeatherApi.Models;

namespace WeatherApi.Services;

/// <summary>
/// Service for interacting with the National Weather Service API.
/// </summary>
public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets the weather forecast for a city by looking up coordinates and retrieving the forecast.
    /// </summary>
    public async Task<ForecastResponse> GetForecastForCityAsync(string city, CancellationToken cancellationToken = default)
    {
        var (lat, lon) = GetCityCoordinates(city);

        // Step 1: Get the forecast URL from the /points endpoint
        var pointsResponse = await _httpClient.GetAsync($"/points/{lat},{lon}", cancellationToken);
        pointsResponse.EnsureSuccessStatusCode();

        using var pointsStream = await pointsResponse.Content.ReadAsStreamAsync(cancellationToken);
        var pointsRoot = await JsonSerializer.DeserializeAsync<JsonElement>(pointsStream, cancellationToken: cancellationToken);
        var forecastUrl = pointsRoot.GetProperty("properties").GetProperty("forecast").GetString()
            ?? throw new InvalidOperationException("No forecast URL returned from points endpoint.");

        // Step 2: Get the forecast
        var forecastResponse = await _httpClient.GetAsync(forecastUrl, cancellationToken);
        forecastResponse.EnsureSuccessStatusCode();

        using var forecastStream = await forecastResponse.Content.ReadAsStreamAsync(cancellationToken);
        var forecastRoot = await JsonSerializer.DeserializeAsync<JsonElement>(forecastStream, cancellationToken: cancellationToken);

        var properties = forecastRoot.GetProperty("properties");
        var updated = properties.GetProperty("updated").GetString() ?? "unknown";
        var periodsJson = properties.GetProperty("periods");

        var periods = new List<ForecastPeriod>();
        foreach (var p in periodsJson.EnumerateArray())
        {
            periods.Add(new ForecastPeriod(
                Number: p.GetProperty("number").GetInt32(),
                Name: p.GetProperty("name").GetString() ?? "",
                DetailedForecast: p.GetProperty("detailedForecast").GetString() ?? "",
                Temperature: p.GetProperty("temperature").GetInt32(),
                TemperatureUnit: p.GetProperty("temperatureUnit").GetString() ?? "F",
                WindSpeed: p.GetProperty("windSpeed").GetString() ?? "",
                WindDirection: p.GetProperty("windDirection").GetString() ?? "",
                ShortForecast: p.GetProperty("shortForecast").GetString() ?? ""));
        }

        return new ForecastResponse(City: city, Updated: updated, Periods: periods);
    }

    private static (double Lat, double Lon) GetCityCoordinates(string city)
    {
        var lookup = new Dictionary<string, (double, double)>(StringComparer.OrdinalIgnoreCase)
        {
            ["Dallas"] = (32.7767, -96.7970),
            ["Chicago"] = (41.8781, -87.6298),
            ["Seattle"] = (47.6062, -122.3321),
            ["New York"] = (40.7128, -74.0060),
            ["Los Angeles"] = (34.0522, -118.2437),
            ["Denver"] = (39.7392, -104.9903),
            ["Miami"] = (25.7617, -80.1918),
            ["Atlanta"] = (33.7490, -84.3880),
            ["Boston"] = (42.3601, -71.0589),
            ["Phoenix"] = (33.4484, -112.0740)
        };

        if (!lookup.TryGetValue(city, out var coords))
            throw new HttpRequestException($"City '{city}' not found.", null, System.Net.HttpStatusCode.NotFound);

        return coords;
    }
}
