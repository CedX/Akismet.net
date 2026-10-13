namespace Belin.Akismet;

/// <summary>
/// Tests the features of the <see cref="Client"/> class.
/// </summary>
/// <param name="testContext">The test context.</param>
[TestClass]
public class ClientTests(TestContext testContext) {

	/// <summary>
	/// The client used to query the remote API.
	/// </summary>
	private static readonly Client client = new(Environment.GetEnvironmentVariable("AKISMET_API_KEY")!, "https://github.com/CedX/Akismet.net") { IsTest = true };

	/// <summary>
	/// A comment with content marked as ham.
	/// </summary>
	private static readonly Comment ham = new(
		new(ipAddress: "192.168.0.1") {
			Name = "Akismet",
			Role = AuthorRole.Administrator,
			Url = new Uri("https://cedric-belin.fr"),
			UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:156.0) Gecko/20100101 Firefox/156.0"
		}) {
		Content = "I'm testing out the Service API.",
		Referrer = new Uri("https://www.nuget.org/packages/Belin.Akismet"),
		Type = CommentType.Comment
	};

	/// <summary>
	/// A comment with content marked as spam.
	/// </summary>
	private static readonly Comment spam = new(
		new(ipAddress: "127.0.0.1") {
			Email = "akismet-guaranteed-spam@example.com",
			Name = "viagra-test-123",
			UserAgent = "Spam Bot/6.6.6"
		}) {
		Content = "Spam!",
		Date = DateTime.Now,
		Type = CommentType.BlogPost
	};

	/// <summary>
	/// Disposes the Akismet client.
	/// </summary>
	[ClassCleanup]
	public static void ClassCleanup() => client.Dispose();

	[TestMethod]
	public void CheckComment() {
		client.CheckComment(ham, testContext.CancellationToken).ShouldBe(CheckResult.Ham);
		client.CheckComment(spam, testContext.CancellationToken).ShouldBeOneOf([CheckResult.Spam, CheckResult.PervasiveSpam]);
	}

	[TestMethod]
	public void SubmitHam() =>
		client.SubmitHam(ham, testContext.CancellationToken);

	[TestMethod]
	public void SubmitSpam() =>
		client.SubmitSpam(spam, testContext.CancellationToken);

	[TestMethod]
	public void VerifyKey() {
		client.VerifyKey(testContext.CancellationToken).ShouldBeTrue();

		var newClient = new Client("0123456789AB", client.Blog) { IsTest = true };
		newClient.VerifyKey(testContext.CancellationToken).ShouldBeFalse();
	}
}
