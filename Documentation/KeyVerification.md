# Key Verification
Key verification authenticates your API key before calling the [comment check](CommentCheck.md),
[submit spam](SubmitSpam.md) or [submit ham](SubmitHam.md) methods.

This is the first call that you should make to Akismet and is especially useful
if you will have multiple users with their own Akismet subscriptions using your application.

See the [Akismet API documentation](https://akismet.com/developers/detailed-docs/key-verification) for more information.

```csharp
Task<bool> Client.VerifyKeyAsync(CancellationToken cancellationToken = default)
```

## Parameters

### CancellationToken **cancellationToken**
The token to cancel the operation.

## Return value
A `Task` that completes with a boolean value indicating whether the client's API key is valid.

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
  using var client = new Client("123YourAPIKey", "https://www.yourblog.com");
  Console.WriteLine(await client.VerifyKeyAsync() ? "The API key is valid." : "The API key is invalid.");
}
catch (HttpRequestException e) {
  Console.Error.WriteLine($"An error occurred: {e.Message}");
}
```

See the [source code](https://github.com/CedX/Akismet.net/tree/main/Sources) for detailed information
about the `Client` and `Blog` classes, and their properties and methods.
