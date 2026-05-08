# Round 4: Skill Composition (Boss Round)

## The Prompt

> "Wrap the weather API from Rounds 1-3 into a complete MCP tool server in .NET that exposes these capabilities:
> 1. `get_forecast` - Get the NWS forecast for a city
> 2. `lookup_city_coordinates` - Look up coordinates for a city name
>
> The server should use the .NET MCP SDK, support stdio transport, include proper tool descriptions for LLM consumption, and reuse the caching layer from Round 3."

## Starting Point

A working .NET 9 Minimal API with a `/weather/{city}` endpoint, `User-Agent` header, and in-memory caching (the expected output of Round 3). See the `WeatherApi/` project in this folder.

## Requirements

- Create a new MCP tool server project (or convert the existing one)
- Use the **.NET MCP SDK** (`ModelContextProtocol` NuGet package)
- Expose two tools with proper descriptions:
  - `get_forecast(city)` — returns forecast data for a city
  - `lookup_city_coordinates(city)` — returns the lat/lon coordinates for a city
- Support **stdio transport** for MCP communication
- Reuse the caching layer from Round 3
- Include proper tool descriptions that help an LLM understand when and how to use each tool

## Evaluation Criteria

| Criteria | Weight | What We're Looking For |
|----------|--------|----------------------|
| Correctness | 30% | MCP server starts, tools are callable, returns valid data |
| .NET Idioms | 20% | Proper MCP SDK usage, DI, async patterns |
| Tool Compliance | 20% | Correct MCP tool definitions, descriptions, parameter schemas |
| Completeness | 15% | Both tools implemented, caching reused, stdio transport |
| Speed | 15% | Time to first token + total generation time |
