# Round 1: Code Generation (Baseline)

## The Prompt

> "Create a .NET 9 Minimal API that integrates with the National Weather Service API. The API should include:
> 1. A `/weather/{zone}` endpoint that retrieves the forecast for a given NWS zone
> 2. A `/weather/alerts/{zone}` endpoint that retrieves active weather alerts for a given zone
>
> Read the NWS OpenAPI spec at `https://api.weather.gov/openapi.json` to understand the response schemas. Include proper error handling, structured DTOs, and OpenAPI documentation."

## Starting Point

**None** — you start from scratch. Create the full project.

## Requirements

- .NET 9 Minimal API project
- A `/weather/{zone}` GET endpoint that:
  - Calls `https://api.weather.gov/zones/forecast/{zone}/forecast`
  - Returns the forecast data as a structured response
- A `/weather/alerts/{zone}` GET endpoint that:
  - Calls `https://api.weather.gov/alerts/active?zone={zone}`
  - Returns active alerts with proper DTOs (event, headline, severity, urgency, etc.)
- Proper error handling on both endpoints (try/catch, HTTP status codes)
- OpenAPI/Swagger documentation enabled
- Use `IHttpClientFactory` for HTTP calls
- The NWS API **requires a `User-Agent` header** — see the [NWS API docs](https://www.weather.gov/documentation/services-web-api)

## Evaluation Criteria

| Criteria | Weight | What We're Looking For |
|----------|--------|----------------------|
| Correctness | 30% | Code compiles, runs, and returns valid weather/alert data |
| .NET Idioms | 20% | Modern C# 13, minimal API patterns, async/await, DI |
| Tool Compliance | 20% | Correctly reads NWS OpenAPI spec, proper HTTP client usage |
| Completeness | 15% | Both endpoints implemented with DTOs, error handling, OpenAPI docs |
| Speed | 15% | Time to first token + total generation time |
