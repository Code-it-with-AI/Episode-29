using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using WeatherApi.Models;

namespace WeatherApi.Services;

/// <summary>
/// Service for interacting with the National Weather Service API.
/// Includes in-memory caching (15-minute TTL per zone).
/// </summary>
public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

    public WeatherService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    /// <summary>
    /// Gets the weather forecast for a given NWS forecast zone.
    /// Results are cached for 15 minutes per zone.
    /// </summary>
    public async Task<ForecastResponse> GetForecastAsync(string zone, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"forecast:{zone}";

        if (_cache.TryGetValue(cacheKey, out ForecastResponse? cached) && cached is not null)
            return cached;

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

        var result = new ForecastResponse(Zone: zone, Updated: updated, Periods: periods);

        _cache.Set(cacheKey, result, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = CacheDuration
        });

        return result;
    }

    /// <summary>
    /// Gets active weather alerts for a given NWS zone.
    /// Results are cached for 15 minutes per zone.
    /// </summary>
    public async Task<AlertsResponse> GetAlertsAsync(string zone, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"alerts:{zone}";

        if (_cache.TryGetValue(cacheKey, out AlertsResponse? cached) && cached is not null)
            return cached;

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

        var result = new AlertsResponse(Zone: zone, Count: alerts.Count, Alerts: alerts);

        _cache.Set(cacheKey, result, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = CacheDuration
        });

        return result;
    }
}
