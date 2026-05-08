# Round 1: Code Generation (Baseline)

## The Prompt

> "Create a .NET 9 Minimal API with a `/weather` endpoint that calls the National Weather Service API to get forecast data for a given zone. Include proper error handling and OpenAPI documentation."

## Starting Point

**None** — you start from scratch. Create the full project.

## Requirements

- .NET 9 Minimal API project
- A `/weather/{zone}` GET endpoint that:
  - Calls `https://api.weather.gov/zones/forecast/{zone}/forecast`
  - Returns the forecast data as a structured response
  - Includes proper error handling (try/catch, HTTP status codes)
- OpenAPI/Swagger documentation enabled
- Use `IHttpClientFactory` for HTTP calls
- The NWS API **requires a `User-Agent` header** — see the [NWS API docs](https://www.weather.gov/documentation/services-web-api)

## Evaluation Criteria

| Criteria | Weight | What We're Looking For |
|----------|--------|----------------------|
| Correctness | 30% | Code compiles, runs, and returns valid weather data |
| .NET Idioms | 20% | Modern C# 13, minimal API patterns, async/await, DI |
| Tool Compliance | 20% | Proper HTTP client usage, correct NWS API integration |
| Completeness | 15% | All parts of the prompt addressed (error handling, OpenAPI docs) |
| Speed | 15% | Time to first token + total generation time |
