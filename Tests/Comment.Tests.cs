namespace Belin.Akismet;

using System.Globalization;

/// <summary>
/// Tests the features of the <see cref="Comment"/> class.
/// </summary>
[TestClass]
public class CommentTests {

	[TestMethod]
	public void ToDictionary() {
		// It should return only the author info with a newly created instance.
		var dictionary = (Dictionary<string, string>) new Comment(new Author(ipAddress: "127.0.0.1"));
		dictionary.Count.ShouldBe(1);
		dictionary["user_ip"].ShouldBe("127.0.0.1");

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
		dictionary.Count.ShouldBe(7);
		dictionary["comment_author"].ShouldBe("Cédric Belin");
		dictionary["comment_content"].ShouldBe("A user comment.");
		dictionary["comment_date_gmt"].ShouldBe("2000-01-01T00:00:00.0000000Z");
		dictionary["comment_type"].ShouldBe("blog-post");
		dictionary["referrer"].ShouldBe("https://cedric-belin.fr/");
		dictionary["user_agent"].ShouldBe("Doom/6.6.6");
		dictionary["user_ip"].ShouldBe("192.168.0.1");
	}
}
