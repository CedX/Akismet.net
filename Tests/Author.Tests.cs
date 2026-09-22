namespace Belin.Akismet;

/// <summary>
/// Tests the features of the <see cref="Author"/> class.
/// </summary>
[TestClass]
public sealed class AuthorTests {

	[TestMethod]
	public void ToDictionary() {
		// It should return only the IP address with a newly created instance.
		var dictionary = (Dictionary<string, string>) new Author(ipAddress: "127.0.0.1");
		Assert.HasCount(1, dictionary);
		Assert.AreEqual("127.0.0.1", dictionary["user_ip"]);

		// It should return a non-empty map with an initialized instance.
		var author = new Author(ipAddress: "192.168.0.1") {
			Name = "Cédric Belin",
			Email = "contact@cedric-belin.fr",
			Url = new Uri("https://cedric-belin.fr"),
			UserAgent = "Mozilla/5.0"
		};

		dictionary = (Dictionary<string, string>) author;
		Assert.HasCount(5, dictionary);
		Assert.AreEqual("Cédric Belin", dictionary["comment_author"]);
		Assert.AreEqual("contact@cedric-belin.fr", dictionary["comment_author_email"]);
		Assert.AreEqual("https://cedric-belin.fr/", dictionary["comment_author_url"]);
		Assert.AreEqual("Mozilla/5.0", dictionary["user_agent"]);
		Assert.AreEqual("192.168.0.1", dictionary["user_ip"]);
	}
}
