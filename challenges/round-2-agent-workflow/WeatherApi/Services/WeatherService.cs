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

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var root = await JsonSerializer.DeserializeAsync<JsonElement>(stream, cancellationToken: cancellationToken);

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

    /// <summary>
    /// Gets active weather alerts for a given NWS zone.
    /// </summary>
    public async Task<AlertsResponse> GetAlertsAsync(string zone, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/alerts/active?zone={zone}", cancellationToken);
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var root = await JsonSerializer.DeserializeAsync<JsonElement>(stream, cancellationToken: cancellationToken);

        var features = root.GetProperty("features");
        var alerts = new List<WeatherAlert>();

        foreach (var feature in features.EnumerateArray())
        {
            var props = feature.GetProperty("properties");
            alerts.Add(new WeatherAlert(
                Id: props.GetProperty("id").GetString() ?? "",
                Event: props.GetProperty("event").GetString() ?? "",
                Headline: props.TryGetProperty("headline", out var h) ? h.GetString() ?? "" : "",
                Description: props.TryGetProperty("description", out var d) ? d.GetString() ?? "" : "",
                Severity: props.GetProperty("severity").GetString() ?? "",
                Urgency: props.GetProperty("urgency").GetString() ?? "",
                Certainty: props.GetProperty("certainty").GetString() ?? "",
                AreaDesc: props.TryGetProperty("areaDesc", out var a) ? a.GetString() ?? "" : "",
                Onset: props.TryGetProperty("onset", out var o) ? o.GetString() ?? "" : "",
                Expires: props.TryGetProperty("expires", out var e) ? e.GetString() ?? "" : ""));
        }

        return new AlertsResponse(Zone: zone, Count: alerts.Count, Alerts: alerts);
    }
}
