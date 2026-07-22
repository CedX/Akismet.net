namespace Belin.Akismet;

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
