namespace Belin.Akismet;

/// <summary>
/// Tests the features of the <see cref="Author"/> class.
/// </summary>
[TestClass]
public class AuthorTests {

	[TestMethod]
	public void ToDictionary() {
		// It should return only the IP address with a newly created instance.
		var dictionary = (Dictionary<string, string>) new Author(ipAddress: "127.0.0.1");
		dictionary.Count.ShouldBe(1);
		dictionary["user_ip"].ShouldBe("127.0.0.1");

		// It should return a non-empty map with an initialized instance.
		var author = new Author(ipAddress: "192.168.0.1") {
			Name = "Cédric Belin",
			Email = "contact@cedric-belin.fr",
			Url = new Uri("https://cedric-belin.fr"),
			UserAgent = "Mozilla/5.0"
		};

		dictionary = (Dictionary<string, string>) author;
		dictionary.Count.ShouldBe(5);
		dictionary["comment_author"].ShouldBe("Cédric Belin");
		dictionary["comment_author_email"].ShouldBe("contact@cedric-belin.fr");
		dictionary["comment_author_url"].ShouldBe("https://cedric-belin.fr/");
		dictionary["user_agent"].ShouldBe("Mozilla/5.0");
		dictionary["user_ip"].ShouldBe("192.168.0.1");
	}
}
