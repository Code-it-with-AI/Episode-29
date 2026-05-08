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
    /// Gets the weather forecast for a given NWS forecast zone.
    /// </summary>
    public async Task<ForecastResponse> GetForecastAsync(string zone, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/zones/forecast/{zone}/forecast", cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromStreamAsync(cancellationToken);
        var root = JsonSerializer.Deserialize<JsonElement>(json);

        var properties = root.GetProperty("properties");
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

        return new ForecastResponse(Zone: zone, Updated: updated, Periods: periods);
    }

    private async Task<JsonElement> ReadFromStreamAsync(HttpContent content, CancellationToken cancellationToken)
    {
        using var stream = await content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync<JsonElement>(stream, cancellationToken: cancellationToken);
    }
}

file static class HttpContentExtensions
{
    public static async Task<JsonElement> ReadFromStreamAsync(this HttpContent content, CancellationToken cancellationToken = default)
    {
        using var stream = await content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync<JsonElement>(stream, cancellationToken: cancellationToken);
    }
}
