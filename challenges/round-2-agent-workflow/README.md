# Round 2: Multi-Step Agent Workflow

## The Prompt

> "The `/weather` endpoint is returning a 403 Forbidden error from the NWS API. Using the tools available to you (`list_files`, `read_file`, `edit_file`, `run_tests`), diagnose the issue, fix it, and verify the fix works. Hint: review the NWS API requirements documented in the showdown spec."

## Starting Point

A .NET 9 Minimal API with a `/weather/{city}` endpoint that looks up coordinates and retrieves the NWS forecast (the expected output of Round 1). 

See the `WeatherApi/` project in this folder.

## The Bug

The `HttpClient` is configured **without** a `User-Agent` header. The NWS API requires this header and returns `403 Forbidden` without it.

## What the Model Must Do

1. **Read** the existing code to understand the project structure
2. **Identify** the missing `User-Agent` header as the root cause
3. **Fix** the `HttpClient` configuration in `Program.cs`
4. **Verify** the fix works (run the app, test the endpoint)

## Evaluation Criteria

| Criteria | Weight | What We're Looking For |
|----------|--------|----------------------|
| Correctness | 30% | Correctly identifies and fixes the User-Agent issue |
| .NET Idioms | 20% | Fix uses proper HttpClient configuration patterns |
| Tool Compliance | 20% | Proper use of available tools (read, edit, verify) |
| Completeness | 15% | Full diagnostic → fix → verify workflow |
| Speed | 15% | Time to first token + total generation time |
