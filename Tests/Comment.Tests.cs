namespace Belin.Akismet;

using System.Globalization;

/// <summary>
/// Tests the features of the <see cref="Comment"/> class.
/// </summary>
[TestClass]
public sealed class CommentTests {

	[TestMethod]
	public void ToDictionary() {
		// It should return only the author info with a newly created instance.
		var dictionary = (Dictionary<string, string>) new Comment(new Author(ipAddress: "127.0.0.1"));
		Assert.HasCount(1, dictionary);
		Assert.AreEqual("127.0.0.1", dictionary["user_ip"]);

		// It should return a non-empty map with an initialized instance.
		var author = new Author(ipAddress: "192.168.0.1") {
			Name = "Cédric Belin",
			UserAgent = "Doom/6.6.6"
		};

		var comment = new Comment(author) {
			Content = "A user comment.",
			Date = DateTime.Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
			Referrer = new Uri("https://cedric-belin.fr"),
			Type = CommentType.BlogPost
		};

		dictionary = (Dictionary<string, string>) comment;
		Assert.HasCount(7, dictionary);
		Assert.AreEqual("Cédric Belin", dictionary["comment_author"]);
		Assert.AreEqual("A user comment.", dictionary["comment_content"]);
		Assert.AreEqual("2000-01-01T00:00:00.0000000Z", dictionary["comment_date_gmt"]);
		Assert.AreEqual("blog-post", dictionary["comment_type"]);
		Assert.AreEqual("https://cedric-belin.fr/", dictionary["referrer"]);
		Assert.AreEqual("Doom/6.6.6", dictionary["user_agent"]);
		Assert.AreEqual("192.168.0.1", dictionary["user_ip"]);
	}
}
