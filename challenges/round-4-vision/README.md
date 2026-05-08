# Round 4: Vision + Code Understanding

## The Prompt

> Provide a screenshot of:
> - The Swagger UI page showing the `/weather` and `/weather/alerts` endpoints from Rounds 1-2
> - A hand-drawn architecture diagram showing: Client → Minimal API → NWS API
>
> Ask: "Based on this architecture, add an in-memory caching layer so we don't hit the NWS API more than once per zone per 15 minutes. Show me the code changes needed."

## Starting Point

A working .NET 9 Minimal API with both `/weather/{zone}` and `/weather/alerts/{zone}` endpoints, properly configured with the `User-Agent` header (the expected output of Round 3). See the `WeatherApi/` project in this folder.

## Requirements

- Add `IMemoryCache` via Microsoft's caching abstractions
- Cache forecast responses per zone for 15 minutes
- Cache alert responses per zone for 15 minutes
- Use separate cache keys for forecasts vs. alerts
- Return cached data when available, call NWS API only on cache miss

## Visual Input

During the live challenge, the model will receive:
1. A screenshot of the Swagger UI showing both endpoints
2. A hand-drawn architecture diagram (Client → API → NWS)

The model must interpret these visuals and propose the caching layer insertion point.

## Evaluation Criteria

| Criteria | Weight | What We're Looking For |
|----------|--------|----------------------|
| Correctness | 30% | Caching works, stale data expires after 15 min |
| .NET Idioms | 20% | Proper use of `IMemoryCache`, DI registration, `MemoryCacheEntryOptions` |
| Tool Compliance | 20% | Correctly interprets screenshot/diagram context |
| Completeness | 15% | Both endpoints cached, cache keys are distinct |
| Speed | 15% | Time to first token + total generation time |
