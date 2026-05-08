using System.Net.Http;

var client = new HttpClient { BaseAddress = new Uri("https://api.weather.gov") };
client.DefaultRequestHeaders.Accept.ParseAdd("application/geo+json");

// Without User-Agent (Round 3 bug)
var response = await client.GetAsync("/zones/forecast/WAZ558/forecast");
Console.WriteLine($"Without User-Agent: {response.StatusCode}");

// With User-Agent (Round 4 fix)
client.DefaultRequestHeaders.UserAgent.ParseAdd("Test/1.0");
var response2 = await client.GetAsync("/zones/forecast/WAZ558/forecast");
Console.WriteLine($"With User-Agent: {response2.StatusCode}");
