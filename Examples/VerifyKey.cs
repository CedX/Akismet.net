using Belin.Akismet;
using System.Net.Http;

// Verifies an Akismet API key.
try {
	using var client = new Client("123YourAPIKey", "https://www.yourblog.com");
	Console.WriteLine(await client.VerifyKeyAsync() ? "The API key is valid." : "The API key is invalid.");
}
catch (HttpRequestException e) {
	Console.Error.WriteLine($"An error occurred: {e.Message}");
}
