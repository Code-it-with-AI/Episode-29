# 🏆 Scorecard: Qwen2.5-Coder:7b

> **Judge:** _______________
> **Date:** _______________
> **Quantization:** _______________

---

## Round 1: Code Generation (Baseline)

**Prompt:** Create a .NET 9 Minimal API with `/weather` and `/weather/alerts` endpoints.

| Criteria | Weight | Score (0-10) | Notes |
|----------|--------|:------------:|-------|
| Correctness | 30% | | Does it compile? Does it run? Does it return real data? |
| .NET Idioms | 20% | | Modern C# 13, minimal API patterns, async/await, DI |
| Tool Compliance | 20% | | Reads NWS OpenAPI spec, proper HttpClient usage |
| Completeness | 15% | | Both endpoints, DTOs, error handling, OpenAPI docs |
| Speed | 15% | | Time to first token: ___s / Total generation: ___s |

**Weighted Score:** ___ / 10
**`dotnet build` result:** ✅ Pass / ❌ Fail
**`dotnet run` + live test:** ✅ Pass / ❌ Fail

**Observations:**
> _(What stood out? Any hallucinations? Missing pieces?)_

---

## Round 2: Multi-Step Agent Workflow

**Prompt:** Diagnose and fix the 403 Forbidden error (missing User-Agent header).

| Criteria | Weight | Score (0-10) | Notes |
|----------|--------|:------------:|-------|
| Correctness | 30% | | Correctly identifies User-Agent as the root cause |
| .NET Idioms | 20% | | Fix uses proper HttpClient configuration patterns |
| Tool Compliance | 20% | | Proper use of read/edit/verify tools |
| Completeness | 15% | | Full diagnose → fix → verify workflow |
| Speed | 15% | | Time to first token: ___s / Total generation: ___s |

**Weighted Score:** ___ / 10
**Identified the bug:** ✅ Yes / ❌ No
**Fix compiles and works:** ✅ Pass / ❌ Fail

**Observations:**
> _(How many steps did it take? Did it go down wrong paths first?)_

---

## Round 3: Vision + Code Understanding

**Prompt:** Add in-memory caching from Swagger screenshot + architecture diagram.

| Criteria | Weight | Score (0-10) | Notes |
|----------|--------|:------------:|-------|
| Correctness | 30% | | Caching works, stale data expires after 15 min |
| .NET Idioms | 20% | | Proper IMemoryCache, DI registration, cache options |
| Tool Compliance | 20% | | Correctly interprets screenshot + diagram |
| Completeness | 15% | | Both endpoints cached, distinct cache keys |
| Speed | 15% | | Time to first token: ___s / Total generation: ___s |

**Weighted Score:** ___ / 10
**Understood the images:** ✅ Yes / ⚠️ Partial / ❌ No
**Caching implemented correctly:** ✅ Pass / ❌ Fail

**Observations:**
> _(Did it describe the images accurately? Did it place the cache in the right layer?)_

---

## Round 4: Skill Composition (Boss Round)

**Prompt:** Wrap the API into a complete MCP tool server with 3 tools.

| Criteria | Weight | Score (0-10) | Notes |
|----------|--------|:------------:|-------|
| Correctness | 30% | | MCP server starts, tools are callable, returns data |
| .NET Idioms | 20% | | Proper MCP SDK usage, DI, async patterns |
| Tool Compliance | 20% | | Correct tool definitions, descriptions, schemas |
| Completeness | 15% | | All 3 tools, caching reused, stdio transport |
| Speed | 15% | | Time to first token: ___s / Total generation: ___s |

**Weighted Score:** ___ / 10
**MCP server starts:** ✅ Pass / ❌ Fail
**All 3 tools exposed:** ✅ Yes / ⚠️ Partial (___ / 3) / ❌ No

**Observations:**
> _(Did it know the MCP SDK? Did it reuse caching? Any hallucinated APIs?)_

---

## Final Score

| Round | Weighted Score | Weight | Contribution |
|-------|:--------------:|:------:|:------------:|
| Round 1: Code Generation | ___ / 10 | 25% | |
| Round 2: Agent Workflow | ___ / 10 | 25% | |
| Round 3: Vision | ___ / 10 | 25% | |
| Round 4: MCP Server | ___ / 10 | 25% | |
| **TOTAL** | | | **___ / 10** |

### Strengths
> -

### Weaknesses
> -

### Would you use this model for .NET development?
> ✅ Yes / ⚠️ With caveats / ❌ No
