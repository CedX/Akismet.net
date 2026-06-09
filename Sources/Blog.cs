namespace Belin.Akismet;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

/// <summary>
/// Represents the front page or home URL transmitted when making requests.
/// </summary>
/// <param name="url">The blog or site URL.</param>
public sealed class Blog(Uri url) {

	/// <summary>
	/// The character encoding for the values included in comments.
	/// </summary>
	public Encoding? Charset { get; set; }

	/// <summary>
	/// The languages in use on the blog or site, in ISO 639-1 format.
	/// </summary>
	public IList<string> Languages { get; set; } = [];

	/// <summary>
	/// The blog or site URL.
	/// </summary>
	public Uri Url { get; set; } = url;

	/// <summary>
	/// Creates a new blog.
	/// </summary>
	/// <param name="url">The blog or site URL.</param>
	public Blog([StringSyntax(StringSyntaxAttribute.Uri)] string url): this(new Uri(url, UriKind.Absolute)) {}

	/// <summary>
	/// Converts the specified blog to a dictionary.
	/// </summary>
	/// <param name="blog">The blog to convert.</param>
	/// <returns>The dictionary corresponding to the specified blog.</returns>
	public static explicit operator Dictionary<string, string>(Blog blog) {
		var dictionary = new Dictionary<string, string> { ["blog"] = blog.Url.ToString() };
		if (blog.Charset is not null) dictionary["blog_charset"] = blog.Charset.WebName;
		if (blog.Languages.Count > 0) dictionary["blog_lang"] = string.Join(',', blog.Languages);
		return dictionary;
	}

	/// <summary>
	/// Converts the specified blog to a hash table.
	/// </summary>
	/// <param name="blog">The blog to convert.</param>
	/// <returns>The hash table corresponding to the specified blog.</returns>
	public static explicit operator Hashtable(Blog blog) => new((Dictionary<string, string>) blog);

	/// <summary>
	/// Creates a new blog from the specified string.
	/// </summary>
	/// <param name="url">The blog or site URL.</param>
	/// <returns>The blog corresponding to the specified string.</returns>
	public static implicit operator Blog([StringSyntax(StringSyntaxAttribute.Uri)] string url) => new(url);

	/// <summary>
	/// Creates a new blog from the specified URI.
	/// </summary>
	/// <param name="url">The blog or site URL.</param>
	/// <returns>The blog corresponding to the specified URI.</returns>
	public static implicit operator Blog(Uri url) => new(url);
}
