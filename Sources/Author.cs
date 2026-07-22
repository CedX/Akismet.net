namespace Belin.Akismet;

using System.Collections;
using System.Net;

/// <summary>
/// Represents the author of a comment.
/// </summary>
/// <param name="ipAddress">The author's IP address.</param>
public sealed class Author(IPAddress ipAddress) {

	/// <summary>
	/// The author's mail address. If you set it to <c>"akismet-guaranteed-spam@example.com"</c>, Akismet will always return <see langword="true"/>.
	/// </summary>
	public string Email { get; set; } = "";

	/// <summary>
	/// The author's IP address.
	/// </summary>
	public IPAddress IPAddress { get; set; } = ipAddress;

	/// <summary>
	/// The author's name. If you set it to <c>"viagra-test-123"</c>, Akismet will always return <see langword="true"/>.
	/// </summary>
	public string Name { get; set; } = "";

	/// <summary>
	/// The author's role. If you set it to <c>"administrator"</c>, Akismet will always return <see langword="false"/>.
	/// </summary>
	public string Role { get; set; } = "";

	/// <summary>
	/// The URL of the author's website.
	/// </summary>
	public Uri? Url { get; set; }

	/// <summary>
	/// The author's user agent, that is the string identifying the Web browser used to submit comments.
	/// </summary>
	public string UserAgent { get; set; } = "";

	/// <summary>
	/// Creates a new author.
	/// </summary>
	/// <param name="ipAddress">The author's IP address.</param>
	public Author(string ipAddress): this(IPAddress.Parse(ipAddress)) {}

	/// <summary>
	/// Converts the specified author to a dictionary.
	/// </summary>
	/// <param name="author">The author to convert.</param>
	/// <returns>The dictionary corresponding to the specified author.</returns>
	public static explicit operator Dictionary<string, string>(Author author) {
		var dictionary = new Dictionary<string, string> { ["user_ip"] = author.IPAddress.ToString() };
		if (!string.IsNullOrWhiteSpace(author.Email)) dictionary["comment_author_email"] = author.Email;
		if (!string.IsNullOrWhiteSpace(author.Name)) dictionary["comment_author"] = author.Name;
		if (!string.IsNullOrWhiteSpace(author.Role)) dictionary["user_role"] = author.Role;
		if (author.Url is not null) dictionary["comment_author_url"] = author.Url.ToString();
		if (!string.IsNullOrWhiteSpace(author.UserAgent)) dictionary["user_agent"] = author.UserAgent;
		return dictionary;
	}

	/// <summary>
	/// Converts the specified author to a hash table.
	/// </summary>
	/// <param name="author">The author to convert.</param>
	/// <returns>The hash table corresponding to the specified author.</returns>
	public static explicit operator Hashtable(Author author) => new((Dictionary<string, string>) author);
}
