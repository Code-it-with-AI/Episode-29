# Episode 29: Local Model Challenge — Ollama Showdown

Can a local LLM running on consumer hardware replace cloud AI for real coding tasks? In this episode, we find out. We pit **5 open-source models** against each other in a 4-round progressive coding challenge — all running locally via [Ollama](https://ollama.com) on an RTX 3070 with 64 GB RAM. No cloud. No API keys. Just raw local inference.

Each model faces the same challenge: build a **.NET 9 Minimal API** backed by the National Weather Service API, debug a real issue, add caching from a hand-drawn architecture diagram, and ultimately wrap everything into a complete **MCP tool server**. We score each model on correctness, .NET idioms, tool compliance, completeness, and speed.

📺 YouTube Video: *(coming soon)*

🏠 Code it with AI Home Page: https://codeitwithai.com

---

## What We'll Cover

1. **The Local Model Landscape** — Why running models locally matters for privacy, cost, latency, and offline development — and where the trade-offs are vs. cloud models
2. **Ollama + GitHub Copilot BYOK** — Setting up Ollama as a local inference server and connecting it to GitHub Copilot via Bring-Your-Own-Key configuration
3. **The Contenders** — Introducing 5 models that fit in 8 GB VRAM: Qwen3-Coder:14b, Gemma4:12b, Llama3.2-Vision:11b, Qwen3:14b, and DeepSeek-Coder-V2:16b
4. **Live: 4-Round Showdown** — Running each model through progressive challenges: code generation → agent debugging → vision-based architecture → MCP server composition
5. **Scoring & Results** — Head-to-head comparison across all rounds, crowning the local model champion

## Learning Objectives

By the end of this episode, you'll be able to:

- **Set up** Ollama as a local model runtime and connect it to GitHub Copilot via BYOK
- **Evaluate** local LLMs for coding tasks using a structured, repeatable benchmark
- **Compare** model strengths: code generation quality, debugging ability, vision comprehension, and multi-file composition
- **Identify** which local models are viable replacements for cloud models in .NET development workflows
- **Build** your own model evaluation framework using progressive challenge rounds with consistent scoring

## Prerequisites

Before starting this episode, you should:

- Have a **GPU with 8+ GB VRAM** (we use an NVIDIA RTX 3070) or sufficient RAM for CPU inference
- Have **Ollama** installed ([ollama.com](https://ollama.com))
- Have **.NET 9 SDK** installed (`dotnet --version` → 9.x)
- Have an active **GitHub Copilot license** with BYOK support (Business or Enterprise)
- Be comfortable with **C# and .NET Minimal APIs** (we're evaluating model output, not teaching .NET basics)

---

## Resource Links

- **Ollama:** https://ollama.com — Local model runtime
- **Ollama Model Library:** https://ollama.com/library — Browse available models
- **GitHub Copilot BYOK Docs:** https://docs.github.com/copilot/managing-copilot/managing-github-copilot-in-your-organization/managing-the-copilot-subscription-for-your-organization/managing-copilot-knowledge-bases
- **NWS API:** https://api.weather.gov/ — The real-world API used in all challenges
- **NWS OpenAPI Spec:** https://api.weather.gov/openapi.json
- **.NET MCP SDK:** https://github.com/modelcontextprotocol/csharp-sdk
- **GitHub Copilot Official Docs:** https://docs.github.com/copilot

---

## The Contenders

| # | Model | Params | VRAM Fit | Tools | Vision | Coding |
|---|-------|--------|----------|-------|--------|--------|
| 1 | **Qwen3:14b** | 14B | Q3_K_M 6.8 GB | ✅ | ❌ | ⭐⭐⭐ |
| 2 | **Qwen2.5-Coder:7b** | 7B | Q6_K 5.9 GB | ✅ | ❌ | ⭐⭐⭐ |
| 3 | **Gemma3:12b** | 12B | Q4_K_M 6.6 GB | ✅ | ✅ | ⭐⭐⭐ |
| 4 | **Llama3.2-Vision:11b** | 11B | Q4_K_M 6.1 GB | ✅ | ✅ | ⭐⭐ |
| 5 | **DeepSeek-R1:14b** | 14B | Q3_K_M 6.8 GB | ✅ | ❌ | ⭐⭐⭐ |

## Demo Repository

This episode's content lives entirely in this folder. The `challenges/` directory contains the starter code and prompts for each round of the showdown:

```
Episode-29/
├── .github/
│   └── copilot-instructions.md      Copilot context for this repository
├── challenges/
│   ├── round-1-code-generation/     Prompt only — model creates from scratch
│   │   └── README.md                The Round 1 challenge prompt
│   ├── round-2-agent-workflow/      API with deliberate bug (missing User-Agent)
│   │   ├── README.md                The Round 2 challenge prompt
│   │   └── WeatherApi/              .NET 9 starter project with the bug
│   ├── round-3-vision/              Working API — model adds caching from visuals
│   │   ├── README.md                The Round 3 challenge prompt
│   │   ├── swagger-ui.png           Swagger UI screenshot (visual input)
│   │   ├── architecture.png         Hand-drawn architecture diagram (visual input)
│   │   └── WeatherApi/              .NET 9 starter project
│   └── round-4-mcp-server/          API with caching — model wraps into MCP server
│       ├── README.md                The Round 4 challenge prompt
│       └── WeatherApi/              .NET 9 starter project with caching
├── model-showdown.md                Full challenge spec, hardware profile, scoring rubric
└── README.md                        ← You are here
```

**Quick start:**
```bash
# Pull all 5 models
ollama pull qwen3:14b
ollama pull qwen2.5-coder:7b
ollama pull gemma3:12b
ollama pull llama3.2-vision:11b
ollama pull deepseek-r1:14b

# Verify Ollama is running
curl http://localhost:11434/v1/models

# Build a challenge starter project (e.g., Round 2)
cd challenges/round-2-agent-workflow/WeatherApi
dotnet build
```

---

## Scoring Rubric

| Criteria | Weight | Description |
|----------|--------|-------------|
| Correctness | 30% | Does the code compile and run? |
| .NET Idioms | 20% | Modern C#, proper async/await, DI patterns |
| Tool Compliance | 20% | Correct tool call format, parameters, response handling |
| Completeness | 15% | All parts of the prompt addressed |
| Speed | 15% | Time to first token + total generation time |

---

## License

This episode's content and code are part of the **Code it with AI** series.
Licensed under [MIT License](LICENSE).
