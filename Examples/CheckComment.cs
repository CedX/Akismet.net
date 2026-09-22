using Belin.Akismet;
using System.Net.Http;
using System.Text;

// Checks a comment against the Akismet service.
try {
	var author = new Author(ipAddress: "192.168.123.456") {
		Email = "john.doe@domain.com",
		Name = "John Doe",
		Role = "guest",
		UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:156.0) Gecko/20100101 Firefox/156.0"
	};

	var comment = new Comment(author) {
		Content = "A user comment.",
		Date = DateTime.Now,
		Referrer = new Uri("https://github.com/CedX/Akismet.net"),
		Type = CommentType.ContactForm
	};

	var blog = new Blog("https://www.yourblog.com") { Charset = Encoding.UTF8, Languages = ["fr"] };
	var result = await new Client("123YourAPIKey", blog).CheckCommentAsync(comment);
	Console.WriteLine(result == CheckResult.Ham ? "The comment is ham." : "The comment is spam.");
}
catch (HttpRequestException e) {
	Console.Error.WriteLine($"An error occurred: {e.Message}");
}
