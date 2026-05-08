namespace WeatherApi.Models;

/// <summary>
/// Top-level alerts response.
/// </summary>
public record AlertsResponse(
    string Zone,
    int Count,
    List<WeatherAlert> Alerts);

/// <summary>
/// A single weather alert from the NWS API.
/// </summary>
public record WeatherAlert(
    string Id,
    string Event,
    string Headline,
    string Description,
    string Severity,
    string Urgency,
    string Certainty,
    string AreaDesc,
    string Onset,
    string Expires);
