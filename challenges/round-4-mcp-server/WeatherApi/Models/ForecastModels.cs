namespace WeatherApi.Models;

/// <summary>
/// Top-level forecast response from the NWS API.
/// </summary>
public record ForecastResponse(
    string City,
    string Updated,
    List<ForecastPeriod> Periods);

/// <summary>
/// A single forecast period (e.g., "Tonight", "Thursday").
/// </summary>
public record ForecastPeriod(
    int Number,
    string Name,
    string DetailedForecast,
    int Temperature,
    string TemperatureUnit,
    string WindSpeed,
    string WindDirection,
    string ShortForecast);
