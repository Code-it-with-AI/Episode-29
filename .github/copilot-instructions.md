# Copilot Instructions — Episode 29: Local Model Challenge (Ollama Showdown)

## About This Repository

This is the reference content folder for **Episode 29** of the podcast **Code it with AI**. It contains code samples, references, and content from the episode. The repository follows the same structure as previous episodes (e.g., [Episode 25](https://github.com/code-it-with-ai/episode-25)).

## Episode Overview

In this episode we run a **Local Model Challenge** — a head-to-head showdown of 5 open-source LLMs running locally via [Ollama](https://ollama.com) on consumer hardware (RTX 3070 / 64 GB RAM / Windows 11). Each model tackles a progressive, 5-round coding challenge that builds a .NET 9 Minimal API backed by the National Weather Service API, culminating in a full MCP tool server.

## The Contenders

| # | Model | Params |
|---|-------|--------|
| 1 | Qwen3:14b | 14B |
| 2 | Qwen2.5-Coder:7b | 7B |
| 3 | Gemma3:12b | 12B |
| 4 | Llama3.2-Vision:11b | 11B |
| 5 | DeepSeek-R1:14b | 14B |

## Key Technologies

- **Ollama** — local model runtime (BYOK endpoint at `http://localhost:11434/v1`)
- **.NET 9 Minimal API** — the target framework for all generated code
- **National Weather Service API** (`https://api.weather.gov/`) — the real-world API used in every round
- **MCP (Model Context Protocol)** — the .NET MCP SDK is used in the final boss round
- **GitHub Copilot BYOK** — models are accessed through Copilot's Bring-Your-Own-Key configuration

## Challenge Rounds (see `model-showdown.md` for full details)

1. **Code Generation** — Create a .NET 9 Minimal API with a `/weather/{city}` endpoint that looks up coordinates and retrieves the NWS forecast
2. **Multi-Step Agent Workflow** — Diagnose and fix a 403 Forbidden error (missing `User-Agent` header)
3. **Vision + Code Understanding** — Add in-memory caching from a [Swagger UI screenshot](challenges/round-3-vision/swagger-ui.png) and [hand-drawn architecture diagram](challenges/round-3-vision/architecture.png)
4. **Skill Composition (Boss Round)** — Build a complete MCP tool server with `get_forecast`, `get_alerts`, and `lookup_zone`

## Code Style & Conventions

When generating or reviewing code in this repository:

- Use **.NET 9** and **C# 13** idioms (top-level statements, minimal APIs, file-scoped namespaces)
- Prefer `async/await` with proper cancellation token propagation
- Use dependency injection for services (`IHttpClientFactory`, `IMemoryCache`, etc.)
- Follow the NWS API requirement: **always include a `User-Agent` header** in HTTP requests
- Use `System.Text.Json` for serialization (not Newtonsoft)
- Include XML doc comments on public API endpoints
- Each model's output should be kept in its own subfolder to preserve comparison integrity

## Repository Structure

```
Episode-29/
├── .github/
│   └── copilot-instructions.md       ← you are here
├── model-showdown.md                 ← full challenge spec, scoring rubric, hardware profile
├── challenges/                       ← base/starter code for each round
│   ├── round-1-code-generation/      ← empty start (just prompt README)
│   ├── round-2-agent-workflow/       ← API with deliberate bug (missing User-Agent)
│   ├── round-3-vision/               ← working API with both endpoints
│   └── round-4-mcp-server/           ← API with caching, ready for MCP wrapping
└── <model-name>/                     ← per-model output folders (created during the challenge)
    ├── round-1/
    ├── round-2/
    ├── round-3/
    └── round-4/
```

Each `challenges/round-*/` folder contains a `README.md` with the exact prompt and evaluation criteria, plus the starter code (a `.NET 9 Minimal API project`) that the model receives at the beginning of that round. Each round's starter code is the expected successful output of the previous round.

## NWS API Notes

- Base URL: `https://api.weather.gov/`
- OpenAPI spec: `https://api.weather.gov/openapi.json`
- **A `User-Agent` header is required** — requests without one return `403 Forbidden`
- Zone lookup → forecast retrieval is a two-step flow
- Alerts endpoint: `https://api.weather.gov/alerts/active?zone={zone}`

## Scoring Rubric

| Criteria | Weight |
|----------|--------|
| Correctness | 30% |
| .NET Idioms | 20% |
| Tool Compliance | 20% |
| Completeness | 15% |
| Speed | 15% |
