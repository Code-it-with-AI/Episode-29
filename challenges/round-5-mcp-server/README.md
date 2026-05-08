# Round 5: Skill Composition (Boss Round)

## The Prompt

> "Wrap the weather API from Rounds 1-4 into a complete MCP tool server in .NET that exposes these capabilities:
> 1. `get_forecast` - Get the NWS forecast for a zone
> 2. `get_alerts` - Get active weather alerts for a zone
> 3. `lookup_zone` - Look up a zone ID by state and city using the NWS zones endpoint
>
> The server should use the .NET MCP SDK, support stdio transport, include proper tool descriptions for LLM consumption, and reuse the caching layer from Round 4."

## Starting Point

A working .NET 9 Minimal API with both endpoints, `User-Agent` header, and in-memory caching (the expected output of Round 4). See the `WeatherApi/` project in this folder.

## Requirements

- Create a new MCP tool server project (or convert the existing one)
- Use the **.NET MCP SDK** (`ModelContextProtocol` NuGet package)
- Expose three tools with proper descriptions:
  - `get_forecast(zone)` — returns forecast data
  - `get_alerts(zone)` — returns active alerts
  - `lookup_zone(state, city)` — queries `https://api.weather.gov/zones/forecast?area={state}` and filters by city
- Support **stdio transport** for MCP communication
- Reuse the caching layer from Round 4
- Include proper tool descriptions that help an LLM understand when and how to use each tool

## Evaluation Criteria

| Criteria | Weight | What We're Looking For |
|----------|--------|----------------------|
| Correctness | 30% | MCP server starts, tools are callable, returns valid data |
| .NET Idioms | 20% | Proper MCP SDK usage, DI, async patterns |
| Tool Compliance | 20% | Correct MCP tool definitions, descriptions, parameter schemas |
| Completeness | 15% | All 3 tools implemented, caching reused, stdio transport |
| Speed | 15% | Time to first token + total generation time |
