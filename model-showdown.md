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
| 1 | **Qwen3-Coder:14b** | 14B | ✅ Full GPU | ✅ | ⚠️ Limited | ⭐⭐⭐ | Best .NET/C# coding at this tier |
| 2 | **Gemma4:12b** | 12B | ✅ Full GPU | ✅ | ✅ | ⭐⭐⭐ | Google multimodal, structured JSON |
| 3 | **Llama3.2-Vision:11b** | 11B | ✅ Full GPU | ✅ | ✅ | ⭐⭐ | Strong vision + tool calling |
| 4 | **Qwen3:14b** | 14B | ✅ Full GPU | ✅ | ✅ | ⭐⭐⭐ | General-purpose reasoning powerhouse |
| 5 | **DeepSeek-Coder-V2:16b** | 16B | ⚠️ Partial offload | ✅ | ✅ | ⭐⭐⭐ | Deep reasoning, needs some CPU offload |

## Pull Commands

```bash
ollama pull qwen3-coder:14b
ollama pull gemma4:12b
ollama pull llama3.2-vision:11b
ollama pull qwen3:14b
ollama pull deepseek-coder-v2:16b
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

### The Challenge (5 Rounds)

#### Round 1: Code Generation (Baseline)
> "Create a .NET 9 Minimal API with a `/weather` endpoint that calls an external weather API to get forecast data. Include proper error handling and OpenAPI documentation."

**Evaluates:** C# fluency, .NET 9 idioms, Minimal API patterns, structured output

#### Round 2: Tool Use / Function Calling
> Using the `/weather` API from Round 1, add a new `/weather/alerts` endpoint that queries the NWS `https://api.weather.gov/alerts/active?zone={zone}` endpoint to retrieve active weather alerts for a given zone. The model must:
> 1. Read the NWS OpenAPI spec at `https://api.weather.gov/openapi.json` to understand the alerts response schema
> 2. Create a proper response DTO that maps the relevant alert fields
> 3. Wire up the new endpoint with error handling consistent with Round 1

**Evaluates:** API spec comprehension, tool/endpoint discovery, correct HTTP client usage, DTO mapping from real-world JSON

#### Round 3: Multi-Step Agent Workflow
> "The `/weather` endpoint is returning a 403 Forbidden error from the NWS API. Using the tools available to you (`list_files`, `read_file`, `edit_file`, `run_tests`), diagnose the issue, fix it, and verify the fix works. Hint: review the NWS API requirements documented in the showdown spec."

**Evaluates:** Multi-step reasoning, tool chaining, debugging ability (the NWS API requires a `User-Agent` header), verification loop

#### Round 4: Vision + Code Understanding
> Provide a screenshot of:
> - The Swagger UI page showing the `/weather` and `/weather/alerts` endpoints from Rounds 1-2
> - A hand-drawn architecture diagram showing: Client → Minimal API → NWS API
> 
> Ask: "Based on this architecture, add an in-memory caching layer so we don't hit the NWS API more than once per zone per 15 minutes. Show me the code changes needed."

**Evaluates:** Image comprehension, architectural reasoning, code generation from visual context, .NET `IMemoryCache` / caching patterns

#### Round 5: Skill Composition (Boss Round)
> "Wrap the weather API from Rounds 1-4 into a complete MCP tool server in .NET that exposes these capabilities:
> 1. `get_forecast` - Get the NWS forecast for a zone
> 2. `get_alerts` - Get active weather alerts for a zone
> 3. `lookup_zone` - Look up a zone ID by state and city using the NWS zones endpoint
> 
> The server should use the .NET MCP SDK, support stdio transport, include proper tool descriptions for LLM consumption, and reuse the caching layer from Round 4."

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
3. Record: response time, token count, pass/fail per criterion
4. Score each model 0-10 per round
5. Crown the champion 👑

---

## Status

- [ ] Pull all models
- [ ] Validate tool calling works via Ollama API
- [ ] Run Round 1-5 for each model
- [ ] Score and compare
- [ ] Pick production model(s)
