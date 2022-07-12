# Proxfield.Extensions.Caching.SQLite

![GitHub License](https://img.shields.io/github/license/proxfield/Proxfield.Extensions.Caching.SQLite)
[![Version](https://img.shields.io/badge/version-0.1.0-brightgreen.svg)](https://semver.org)
![Actions](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/build.yml/badge.svg)
![Actions](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/release.yml/badge.svg)
![GitHub branch checks state](https://img.shields.io/github/checks-status/proxfield/Proxfield.Extensions.Caching.SQLite/main)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/proxfield/Proxfield.Extensions.Caching.SQLite)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Brokenegg.DotIni.svg)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite)

* [Installation](#installation)
* [Usage](#usage)
    * [Common initialization](#common-initialization)  
    * [Dependency Injection](#dependency-injection-initialization)
* [Contributing](#contributing)
* [Roadmap](#roadmap)
* [License](#license)

## Installing the package

## Importing the library

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

The caching can be recorded as a simple string
```csharp
cache.SetAsString("users/1", "jose");
```
Or either as a complex object:
```csharp
cache.SetAsObject<User>("users/1", new User() { Name = "Jose" });
```
```csharp
cache.GetAsString("test1");
cache.GetAsObject<User>("test1");
```
