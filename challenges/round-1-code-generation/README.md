# Round 1: Code Generation (Baseline)

## The Prompt

> "Create a .NET 9 Minimal API that integrates with the National Weather Service API. The API should have a `/weather/{city}` endpoint that:
> 1. Accepts a city name (e.g., "Dallas", "Chicago", "Seattle")
> 2. Looks up the city's latitude/longitude using the NWS `/points` endpoint
> 3. Retrieves the forecast for that location
> 4. Returns a structured weather forecast response
>
> Use the NWS API at `https://api.weather.gov`. Include proper error handling, structured DTOs, and OpenAPI documentation."

## Starting Point

**None** — you start from scratch. Create the full project.

## Requirements

- .NET 9 Minimal API project
- A `/weather/{city}` GET endpoint that:
  - Resolves city to coordinates (hint: geocoding via NWS `/points/{lat},{lon}` → forecast URL)
  - Retrieves and returns the forecast as a structured response
- Proper error handling (try/catch, HTTP status codes for city not found, API errors, etc.)
- OpenAPI/Swagger documentation enabled
- Use `IHttpClientFactory` for HTTP calls
- The NWS API **requires a `User-Agent` header** — see the [NWS API docs](https://www.weather.gov/documentation/services-web-api)

## NWS API Flow

The NWS API uses a two-step lookup:
1. `GET https://api.weather.gov/points/{lat},{lon}` → returns a `forecast` URL in the response
2. `GET {forecast_url}` → returns the actual forecast periods

The model must figure out how to go from a city name to coordinates. It may use a hardcoded city lookup, a geocoding service, or another creative approach.

## Evaluation Criteria

| Criteria | Weight | What We're Looking For |
|----------|--------|----------------------|
| Correctness | 30% | Code compiles, runs, and returns valid weather data for a city |
| .NET Idioms | 20% | Modern C# 13, minimal API patterns, async/await, DI |
| Tool Compliance | 20% | Correctly uses NWS API flow (points → forecast), proper HTTP client |
| Completeness | 15% | End-to-end city→forecast flow, error handling, OpenAPI docs |
| Speed | 15% | Time to first token + total generation time |
