namespace Belin.Akismet;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Submits comments to the Akismet service.
/// </summary>
/// <param name="apiKey">The Akismet API key.</param>
/// <param name="blog">The front page or home URL of the instance making requests.</param>
/// <param name="baseUrl">The base URL of the remote API endpoint.</param>
public class Client(string apiKey, Blog blog, Uri? baseUrl = null) {

	/// <summary>
	/// The response returned by the <c>submit-ham</c> and <c>submit-spam</c> endpoints when the outcome is a success.
	/// </summary>
	private const string Success = "Thanks for making the web a better place.";

	/// <summary>
	/// The assembly version.
	/// </summary>
	private static Version Version => typeof(Client).Assembly.GetName().Version!;

	/// <summary>
	/// The Akismet API key.
	/// </summary>
	public string ApiKey { get; set; } = apiKey;

	/// <summary>
	/// The base URL of the remote API endpoint.
	/// </summary>
	public Uri BaseUrl { get; set; } = baseUrl ?? new Uri("https://rest.akismet.com/");

	/// <summary>
	/// The front page or home URL of the instance making requests.
	/// </summary>
	public Blog Blog { get; set; } = blog;

	/// <summary>
	/// Value indicating whether the client operates in test mode.
	/// </summary>
	public bool IsTest { get; set; }

	/// <summary>
	/// The user agent string to use when making requests.
	/// </summary>
	public string UserAgent { get; set; } = $".NET/{Environment.Version} | Belin.Akismet/{Version.ToString(3)}";

	/// <summary>
	/// Creates a new client.
	/// </summary>
	/// <param name="apiKey">The Akismet API key.</param>
	/// <param name="blog">The front page or home URL of the instance making requests.</param>
	/// <param name="baseUrl">The base URL of the remote API endpoint.</param>
	public Client(string apiKey, Blog blog, [StringSyntax(StringSyntaxAttribute.Uri)] string baseUrl):
		this(apiKey, blog, new Uri(baseUrl, UriKind.Absolute)) {}

	/// <summary>
	/// Checks the specified comment against the service database, and returns a value indicating whether it is spam.
	/// </summary>
	/// <param name="comment">The comment to be submitted.</param>
	/// <param name="cancellationToken">The token to cancel the operation.</param>
	/// <returns>A value indicating whether the specified comment is spam.</returns>
	public CheckResult CheckComment(Comment comment, CancellationToken cancellationToken = default) =>
		CheckCommentAsync(comment, cancellationToken).GetAwaiter().GetResult();

	/// <summary>
	/// Checks the specified comment against the service database, and returns a value indicating whether it is spam.
	/// </summary>
	/// <param name="comment">The comment to be submitted.</param>
	/// <param name="cancellationToken">The token to cancel the operation.</param>
	/// <returns>A value indicating whether the specified comment is spam.</returns>
	public async Task<CheckResult> CheckCommentAsync(Comment comment, CancellationToken cancellationToken = default) {
		using var response = await Fetch("1.1/comment-check", (Dictionary<string, string>) comment, cancellationToken);
		if (await response.Content.ReadAsStringAsync(cancellationToken) == "false") return CheckResult.Ham;
		if (!response.Headers.TryGetValues("X-akismet-pro-tip", out var proTips)) return CheckResult.Spam;
		return proTips.First() == "discard" ? CheckResult.PervasiveSpam : CheckResult.Spam;
	}

	/// <summary>
	/// Submits the specified comment that was incorrectly marked as spam but should not have been.
	/// </summary>
	/// <param name="comment">The comment to be submitted.</param>
	/// <param name="cancellationToken">The token to cancel the operation.</param>
	public void SubmitHam(Comment comment, CancellationToken cancellationToken = default) =>
		SubmitHamAsync(comment, cancellationToken).GetAwaiter().GetResult();

	/// <summary>
	/// Submits the specified comment that was incorrectly marked as spam but should not have been.
	/// </summary>
	/// <param name="comment">The comment to be submitted.</param>
	/// <param name="cancellationToken">The token to cancel the operation.</param>
	/// <returns>Completes once the comment has been submitted.</returns>
	/// <exception cref="HttpRequestException">The remote server returned an invalid response.</exception>
	public async Task SubmitHamAsync(Comment comment, CancellationToken cancellationToken = default) {
		using var response = await Fetch("1.1/submit-ham", (Dictionary<string, string>) comment, cancellationToken);
		var body = await response.Content.ReadAsStringAsync(cancellationToken);
		if (body != Success) throw new HttpRequestException("Invalid server response.");
	}

	/// <summary>
	/// Submits the specified comment that was not marked as spam but should have been.
	/// </summary>
	/// <param name="comment">The comment to be submitted.</param>
	/// <param name="cancellationToken">The token to cancel the operation.</param>
	public void SubmitSpam(Comment comment, CancellationToken cancellationToken = default) =>
		SubmitSpamAsync(comment, cancellationToken).GetAwaiter().GetResult();

	/// <summary>
	/// Submits the specified comment that was not marked as spam but should have been.
	/// </summary>
	/// <param name="comment">The comment to be submitted.</param>
	/// <param name="cancellationToken">The token to cancel the operation.</param>
	/// <returns>Completes once the comment has been submitted.</returns>
	/// <exception cref="HttpRequestException">The remote server returned an invalid response.</exception>
	public async Task SubmitSpamAsync(Comment comment, CancellationToken cancellationToken = default) {
		using var response = await Fetch("1.1/submit-spam", (Dictionary<string, string>) comment, cancellationToken);
		var body = await response.Content.ReadAsStringAsync(cancellationToken);
		if (body != Success) throw new HttpRequestException("Invalid server response.");
	}

	/// <summary>
	/// Checks the API key against the service database, and returns a value indicating whether it is valid.
	/// </summary>
	/// <param name="cancellationToken">The token to cancel the operation.</param>
	/// <returns><see langword="true"/> if the specified API key is valid, otherwise <see langword="false"/>.</returns>
	public bool VerifyKey(CancellationToken cancellationToken = default) =>
		VerifyKeyAsync(cancellationToken).GetAwaiter().GetResult();

	/// <summary>
	/// Checks the API key against the service database, and returns a value indicating whether it is valid.
	/// </summary>
	/// <param name="cancellationToken">The token to cancel the operation.</param>
	/// <returns><see langword="true"/> if the specified API key is valid, otherwise <see langword="false"/>.</returns>
	public async Task<bool> VerifyKeyAsync(CancellationToken cancellationToken = default) {
		using var response = await Fetch("1.1/verify-key", cancellationToken: cancellationToken);
		return await response.Content.ReadAsStringAsync(cancellationToken) == "valid";
	}

	/// <summary>
	/// Queries the service by posting the specified fields to a given end point, and returns the response.
	/// </summary>
	/// <param name="endpoint">The relative URL of the end point to query.</param>
	/// <param name="fields">The fields describing the query body.</param>
	/// <param name="cancellationToken">The token to cancel the operation.</param>
	/// <returns>The server response.</returns>
	/// <exception cref="HttpRequestException">An error occurred while querying the end point.</exception>
	private async Task<HttpResponseMessage> Fetch(string endpoint, IDictionary<string, string>? fields = null, CancellationToken cancellationToken = default) {
		var body = (Dictionary<string, string>) Blog;
		body.Add("api_key", ApiKey);
		if (IsTest) body.Add("is_test", "1");
		if (fields is not null) foreach (var field in fields) body.Add(field.Key, field.Value);

		using var httpClient = new HttpClient();
		httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);

		using var httpContent = new FormUrlEncodedContent(body);
		var response = await httpClient.PostAsync(new Uri(BaseUrl, endpoint), httpContent, cancellationToken);
		response.EnsureSuccessStatusCode();
		if (response.Headers.TryGetValues("X-akismet-alert-msg", out var alertMessages)) throw new HttpRequestException(alertMessages.First());
		if (response.Headers.TryGetValues("X-akismet-debug-help", out var debugHelps)) throw new HttpRequestException(debugHelps.First());
		return response;
	}
}
