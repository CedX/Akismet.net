# Submit Spam
This call is for submitting comments that weren't marked as spam but should have been.

It is very important that the values you submit with this call match those of your [comment check](CommentCheck.md) calls as closely as possible.
In order to learn from its mistakes, Akismet needs to match your missed spam and false positive reports
to the original [comment check](CommentCheck.md) API calls made when the content was first posted. While it is normal for less information
to be available for [submit spam](SubmitSpam.md) and [submit ham](SubmitHam.md) calls (most comment systems and forums will not store all metadata),
you should ensure that the values that you do send match those of the original content.

See the [Akismet API documentation](https://akismet.com/developers/detailed-docs/submit-spam-missed-spam) for more information.

```csharp
Task Client.SubmitSpam(Comment comment, CancellationToken cancellationToken = default)
```

## Parameters

### Comment **comment**
The user's `Comment` to be submitted, incorrectly classified as ham.

> [!NOTE]
> Ideally, it should be the same object as the one passed to the original [comment check](CommentCheck.md) API call.

### CancellationToken **cancellationToken**
The token to cancel the operation.

## Return value
A `Task` that completes when the given `Comment` has been submitted.

The task faults with a `HttpRequestException` when an issue occurs.
The exception `Message` usually includes some debug information, provided by the `X-akismet-debug-help` HTTP header,
about what exactly was invalid about the call.

It can also fault with a custom error message (provided by the `X-akismet-alert-msg` header).
See [Response Error Codes](https://akismet.com/developers/detailed-docs/errors) for more information.

## Example
```csharp
using Belin.Akismet;
using System.Net.Http;

try {
  var author = new Author(ipAddress: "192.168.123.456") { UserAgent = "Spam Bot/6.6.6" };
  var comment = new Comment(author) { Content = "Spam!" };

  var client = new Client("123YourAPIKey", "https://www.yourblog.com");
  await client.SubmitSpam(comment);

  Console.WriteLine("The comment was successfully submitted as spam.");
}
catch (HttpRequestException e) {
  Console.Error.WriteLine($"An error occurred: {e.Message}");
}
```
