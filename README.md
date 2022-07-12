![GitHub License](https://img.shields.io/github/license/proxfield/Proxfield.Extensions.Caching.SQLite)
[![Version](https://img.shields.io/badge/version-0.1.0-brightgreen.svg)](https://semver.org)
![Actions](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/build.yml/badge.svg)
![Actions](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/release.yml/badge.svg)
![GitHub branch checks state](https://img.shields.io/github/checks-status/proxfield/Proxfield.Extensions.Caching.SQLite/main)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/proxfield/Proxfield.Extensions.Caching.SQLite)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Brokenegg.DotIni.svg)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite)

# SQLite Caching Library

The SQLite Caching Library is layer for caching data on SQLite to be used as a secondary database in case of failures and network inconsistences.

## Nuget
```bash
PM> Install-Package Proxfield.Extensions.Caching.SQLite
```

## How
This caching library is based on the "Microsoft.Extensions.Caching.Redis" for memory caching, but instead of using Redis it uses the SQLite as a data layer. The ideia is to be a library for persistence cache in case of failures. This library uses the "Microsoft.Data" and acts as a layer above the SQLite.

## Usage
The initialization can be either by using Microsoft's dependency injection or a common initialization.

### Common Initialization
```csharp
var cache = new SQLiteCache(options =>
{
    options.Location = @"c:\cache\database.sqlite";
});
```
### Dependency Injection Initialization
```csharp
services.AddSQLiteCache(options => {
    options.Location = @"c:\cache\database.sqlite";
});
```

## Methods available

The caching can be recorded/retrieved as a simple string
```csharp
this.cache.SetAsString("users/1", "jose");
var user = this.cache.GetAsString("users/1");
```
Or either as a complex object:
```csharp
this.cache.SetAsObject<User>("users/1", new User() { Name = "Jose" });
var user = this.cache.GetAsObject<User>("users/1");
```

## Platform Support
SQLite Caching is compiled for DotNet 6, soon there will versions available for other plataforms.
- [x] DotNet 6
- [ ] Winodws 8
- [ ] Windows Phone Silverlight 8
- [ ] Windows Phone 8.1
- [ ] Xamarin iOS
- [ ] Xamarin Android

## License
The MIT License (MIT)

Copyright (c) 2022 Proxfield

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
