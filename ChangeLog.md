# Changelog

## Version [5.2.0](https://github.com/CedX/Akismet.net/compare/v5.1.0...v5.2.0)
- Added explicit conversions from the `Author`, `Blog` and `Comment` classes to a `Hashtable`.

## Version [5.1.0](https://github.com/CedX/Akismet.net/compare/v5.0.0...v5.1.0)
- Added the `cancellationToken` parameter to the synchronous methods of the `Client` class.
- Added explicit conversions from the `Author`, `Blog` and `Comment` classes to a `Dictionary<string, string>`.

## Version [5.0.0](https://github.com/CedX/Akismet.net/compare/v4.0.0...v5.0.0)
- Breaking change: raised the required target framework.
- Ported the [C#](https://learn.microsoft.com/en-us/dotnet/csharp) cmdlets to [PowerShell](https://learn.microsoft.com/en-us/powershell).

## Version [4.0.0](https://github.com/CedX/Akismet.net/compare/v3.0.0...v4.0.0)
- Breaking change: added the `Async` suffix to the asynchronous methods of the `Client` class.
- Added synchronous methods to the `Client` class.

## Version [3.0.0](https://github.com/CedX/Akismet.net/compare/v2.4.0...v3.0.0)
- Merged the [C#](https://learn.microsoft.com/en-us/dotnet/csharp) and [PowerShell](https://learn.microsoft.com/en-us/powershell) projects into one solution.

## Version [2.4.0](https://github.com/CedX/Akismet.net/compare/v2.3.0...v2.4.0)
- Restored support for [.NET](https://dotnet.microsoft.com/en-us) 9.

## Version [2.3.0](https://github.com/CedX/Akismet.net/compare/v2.2.0...v2.3.0)
- Added an implicit conversion operator from `Uri` to the `Blog` class.

## Version [2.2.0](https://github.com/CedX/Akismet.net/compare/v2.1.1...v2.2.0)
- Added constructor overloads to the `Author`, `Blog` and `Client` classes.
- Added an implicit conversion operator from `string` to the `Blog` class.

## Version [2.1.1](https://github.com/CedX/Akismet.net/compare/v2.1.0...v2.1.1)
- Fixed a packaging issue.

## Version [2.1.0](https://github.com/CedX/Akismet.net/compare/v2.0.0...v2.1.0)
- Changed the visibility of the `Client.Version` constant to `public`.

## Version [2.0.0](https://github.com/CedX/Akismet.net/compare/v1.0.1...v2.0.0)
- Added support for [.NET](https://dotnet.microsoft.com/en-us) 10.

## Version [1.0.1](https://github.com/CedX/Akismet.net/compare/v1.0.0...v1.0.1)
- Included the debug symbols in the package published on [NuGet](https://www.nuget.org).

## Version 1.0.0
- Initial release.
