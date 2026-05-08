# 🏆 Ollama Model Showdown

## Hardware Profile

| Component | Spec |
|-----------|------|
| GPU | NVIDIA GeForce RTX 3070 (8GB VRAM) |
| RAM | 64 GB |
| OS | Windows 11 Pro |
| Target | .NET Web Applications + GitHub Copilot BYOK |

## Contenders

| # | Model | Params | VRAM Fit | Tools | Vision | Coding | Notes |
|---|-------|--------|----------|-------|--------|--------|-------|
| 1 | **Qwen3:14b** | 14B | Q3_K_M 6.8 GB | ✅ | ❌ | ⭐⭐⭐ | General-purpose reasoning powerhouse |
| 2 | **Qwen2.5-Coder:7b** | 7B | Q6_K 5.9 GB | ✅ | ❌ | ⭐⭐⭐ | Purpose-built code model, excellent quality at low VRAM |
| 3 | **Gemma3:12b** | 12B | Q4_K_M 6.6 GB | ✅ | ✅ | ⭐⭐⭐ | Google multimodal, structured JSON |
| 4 | **Llama3.2-Vision:11b** | 11B | Q4_K_M 6.1 GB | ✅ | ✅ | ⭐⭐ | Strong vision + tool calling |
| 5 | **DeepSeek-R1:14b** | 14B | Q3_K_M 6.8 GB | ✅ | ❌ | ⭐⭐⭐ | Deep reasoning, chain-of-thought |

## Pull Commands

```bash
ollama pull qwen3:14b
ollama pull qwen2.5-coder:7b
ollama pull gemma3:12b
ollama pull llama3.2-vision:11b
ollama pull deepseek-r1:14b
```

## BYOK Configuration

```
Endpoint: http://localhost:11434/v1
API Key:  ollama (any non-empty string)
```

---

## 🌦️ National Weather Service API endpoint

For the showdown, we'll use the National Weather Service REST endpoint at https://api.weather.gov/  You will need to look up a location by `Zone` and then use that `Zone` to get a forecast.  The OpenAPI documentation is at https://api.weather.gov/openapi.json  The service requires you to provide a User-Agent header in order to query it.

---

## 🧪 Showdown Test Scenario: "The Full-Stack Agent Challenge"

### Concept

Build a **.NET 9 Minimal API** with a connected MCP (Model Context Protocol) tool server — testing each model's ability to reason about code, use tools, handle multi-step agent workflows, and interpret visual input.

### The Challenge (4 Rounds)

#### Round 1: Code Generation (Baseline)
> "Create a .NET 9 Minimal API that integrates with the National Weather Service API. The API should have a `/weather/{city}` endpoint that accepts a city name, looks up the city's coordinates via the NWS `/points` endpoint, retrieves the forecast, and returns a structured response. Include proper error handling and OpenAPI documentation."

**Evaluates:** C# fluency, .NET 9 idioms, Minimal API patterns, multi-step API integration (city → coordinates → forecast), structured output

#### Round 2: Multi-Step Agent Workflow
> "The `/weather` endpoint is returning a 403 Forbidden error from the NWS API. Using the tools available to you (`list_files`, `read_file`, `edit_file`, `run_tests`), diagnose the issue, fix it, and verify the fix works. Hint: review the NWS API requirements documented in the showdown spec."

**Evaluates:** Multi-step reasoning, tool chaining, debugging ability (the NWS API requires a `User-Agent` header), verification loop

#### Round 3: Vision + Code Understanding
> Provide a screenshot of:
> - The Swagger UI page showing the `/weather/{city}` endpoint
> - A hand-drawn architecture diagram showing: Client → Minimal API → NWS API
> 
> Ask: "Based on this architecture, add an in-memory caching layer so we don't hit the NWS API more than once per city per 15 minutes. Show me the code changes needed."

**Evaluates:** Image comprehension, architectural reasoning, code generation from visual context, .NET `IMemoryCache` / caching patterns

#### Round 4: Skill Composition (Boss Round)
> "Wrap the weather API from Rounds 1-3 into a complete MCP tool server in .NET that exposes these capabilities:
> 1. `get_forecast` - Get the NWS forecast for a city
> 2. `lookup_city_coordinates` - Look up coordinates for a city name
> 
> The server should use the .NET MCP SDK, support stdio transport, include proper tool descriptions for LLM consumption, and reuse the caching layer from Round 3."

**Evaluates:** Complex multi-file generation, MCP protocol knowledge, .NET ecosystem expertise, composing previous rounds into a coherent MCP server

### Scoring Rubric

| Criteria | Weight | Description |
|----------|--------|-------------|
| Correctness | 30% | Does the code compile and run? |
| .NET Idioms | 20% | Modern C#, proper async/await, DI patterns |
| Tool Compliance | 20% | Correct tool call format, parameters, response handling |
| Completeness | 15% | All parts of the prompt addressed |
| Speed | 15% | Time to first token + total generation time |

### How to Run

1. Pull all models
2. Use a consistent system prompt for each round
3. Copy `scorecard-template.md` → `scorecards/<model-name>.md` for each model
4. For each round, paste the prompt, record timing, and score 0-10 per criterion
5. Calculate weighted scores and fill in the Final Score table
6. Compare all scorecards and crown the champion 👑

---

## Status

- [ ] Pull all models
- [ ] Validate tool calling works via Ollama API
- [ ] Run Round 1-4 for each model
- [ ] Score and compare
- [ ] Pick production model(s)
