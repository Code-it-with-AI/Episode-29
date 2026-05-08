# Round 2: Tool Use / Function Calling

## The Prompt

> Using the `/weather` API from Round 1, add a new `/weather/alerts` endpoint that queries the NWS `https://api.weather.gov/alerts/active?zone={zone}` endpoint to retrieve active weather alerts for a given zone. The model must:
> 1. Read the NWS OpenAPI spec at `https://api.weather.gov/openapi.json` to understand the alerts response schema
> 2. Create a proper response DTO that maps the relevant alert fields
> 3. Wire up the new endpoint with error handling consistent with Round 1

## Starting Point

A working .NET 9 Minimal API with a `/weather/{zone}` endpoint (the expected output of Round 1). See the `WeatherApi/` project in this folder.

## Requirements

- Add a GET `/weather/alerts/{zone}` endpoint
- Read and interpret the NWS OpenAPI spec to understand the alerts response shape
- Create DTO classes that map the alert fields (event, headline, description, severity, urgency, etc.)
- Error handling consistent with the existing `/weather` endpoint
- Reuse the existing `IHttpClientFactory` / `HttpClient` configuration

## Evaluation Criteria

| Criteria | Weight | What We're Looking For |
|----------|--------|----------------------|
| Correctness | 30% | Alerts endpoint works, returns real NWS alert data |
| .NET Idioms | 20% | Proper DTO design, consistent patterns with Round 1 code |
| Tool Compliance | 20% | Correctly reads/interprets the OpenAPI spec, proper HTTP usage |
| Completeness | 15% | All 3 sub-tasks addressed |
| Speed | 15% | Time to first token + total generation time |
