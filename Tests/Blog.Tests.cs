namespace Belin.Akismet;

using System.Text;

/// <summary>
/// Tests the features of the <see cref="Blog"/> class.
/// </summary>
[TestClass]
public class BlogTests {

	[TestMethod]
	public void ToDictionary() {
		// It should return only the blog URL with a newly created instance.
		var dictionary = (Dictionary<string, string>) new Blog("https://github.com/CedX/Akismet.net");
		dictionary.Count.ShouldBe(1);
		dictionary["blog"].ShouldBe("https://github.com/CedX/Akismet.net");

		// It should return a non-empty map with an initialized instance.
		dictionary = (Dictionary<string, string>) new Blog("https://github.com/CedX/Akismet.net") { Charset = Encoding.UTF8, Languages = ["en", "fr"] };
		dictionary.Count.ShouldBe(3);
		dictionary["blog"].ShouldBe("https://github.com/CedX/Akismet.net");
		dictionary["blog_charset"].ShouldBe("utf-8");
		dictionary["blog_lang"].ShouldBe("en,fr");
	}
}
