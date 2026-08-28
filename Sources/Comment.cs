namespace Belin.Akismet;

using System.Collections;

/// <summary>
/// Represents a comment submitted by an author.
/// </summary>
/// <param name="author">The comment's author.</param>
public sealed class Comment(Author author) {

	/// <summary>
	/// The comment's author.
	/// </summary>
	public Author Author { get; set; } = author;

	/// <summary>
	/// The comment's content.
	/// </summary>
	public string Content { get; set; } = "";

	/// <summary>
	/// The context in which this comment was posted.
	/// </summary>
	public IList<string> Context { get; set; } = [];

	/// <summary>
	/// The UTC timestamp of the creation of the comment.
	/// </summary>
	public DateTime? Date { get; set; }

	/// <summary>
	/// The permanent location of the entry the comment is submitted to.
	/// </summary>
	public Uri? Permalink { get; set; }

	/// <summary>
	/// The UTC timestamp of the publication time for the post, page or thread on which the comment was posted.
	/// </summary>
	public DateTime? PostModified { get; set; }

	/// <summary>
	/// A string describing why the content is being rechecked.
	/// </summary>
	public string RecheckReason { get; set; } = "";

	/// <summary>
	/// The URL of the webpage that linked to the entry being requested.
	/// </summary>
	public Uri? Referrer { get; set; }

	/// <summary>
	/// The comment's type.
	/// </summary>
	public string Type { get; set; } = "";

	/// <summary>
	/// Converts the specified comment to a dictionary.
	/// </summary>
	/// <param name="comment">The comment to convert.</param>
	/// <returns>The dictionary corresponding to the specified comment.</returns>
	public static explicit operator Dictionary<string, string>(Comment comment) {
		var map = (Dictionary<string, string>) comment.Author;
		if (!string.IsNullOrWhiteSpace(comment.Content)) map["comment_content"] = comment.Content;
		// TODO if (comment.Context.Count > 0) map["comment_context"] = string.Join(',', comment.Context);
		if (comment.Date is not null) map["comment_date_gmt"] = comment.Date.Value.ToUniversalTime().ToString("o");
		if (comment.Permalink is not null) map["permalink"] = comment.Permalink.ToString();
		if (comment.PostModified is not null) map["comment_post_modified_gmt"] = comment.PostModified.Value.ToUniversalTime().ToString("o");
		if (!string.IsNullOrWhiteSpace(comment.RecheckReason)) map["recheck_reason"] = comment.RecheckReason;
		if (comment.Referrer is not null) map["referrer"] = comment.Referrer.ToString();
		if (!string.IsNullOrWhiteSpace(comment.Type)) map["comment_type"] = comment.Type;
		return map;
	}

	/// <summary>
	/// Converts the specified comment to a hash table.
	/// </summary>
	/// <param name="comment">The comment to convert.</param>
	/// <returns>The hash table corresponding to the specified comment.</returns>
	public static explicit operator Hashtable(Comment comment) => new((Dictionary<string, string>) comment);
}

/// <summary>
/// Specifies the type of a comment.
/// </summary>
public static class CommentType {

	/// <summary>
	/// A blog post.
	/// </summary>
	public const string BlogPost = "blog-post";

	/// <summary>
	/// A blog comment.
	/// </summary>
	public const string Comment = "comment";

	/// <summary>
	/// A contact form or feedback form submission.
	/// </summary>
	public const string ContactForm = "contact-form";

	/// <summary>
	/// A top-level forum post.
	/// </summary>
	public const string ForumPost = "forum-post";

	/// <summary>
	/// A message sent between just a few users.
	/// </summary>
	public const string Message = "message";

	/// <summary>
	/// A reply to a top-level forum post.
	/// </summary>
	public const string Reply = "reply";

	/// <summary>
	/// A new user account.
	/// </summary>
	public const string Signup = "signup";
}
