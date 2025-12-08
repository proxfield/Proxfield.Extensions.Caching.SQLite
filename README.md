<p align="center">
<img src="https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/assets/7343019/967483a9-c62e-4730-99a3-5f1b1aa0e358" alt="Proxfield.Extensions.Caching.SQLite Logo" />
</p>

# Proxfield.Extensions.Caching.SQLite

The SQLite Caching Library is a layer for caching data on SQLite to be used as a secondary database in case of failures and network inconsistencies.

This library is based on `Microsoft.Extensions.Caching.StackExchangeRedis` for memory caching, but uses SQLite as the data store. It serves as a persistent cache layer, leveraging `Microsoft.Data.Sqlite`.

![GitHub License](https://img.shields.io/github/license/proxfield/Proxfield.Extensions.Caching.SQLite)
![Actions](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/build.yml/badge.svg)
[![Nuget](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/release.yml/badge.svg)](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/release.yml)
![GitHub branch checks state](https://img.shields.io/github/checks-status/proxfield/Proxfield.Extensions.Caching.SQLite/main)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/proxfield/Proxfield.Extensions.Caching.SQLite)

## Packages

Packages and versions available at the Nuget Gallery.

| Package | Version | Downloads |
| - | - | - |
| **Proxfield.Extensions.Caching.SQLite** | [![Nuget version](https://img.shields.io/nuget/v/Proxfield.Extensions.Caching.SQLite)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite/) | [![Nuget downloads](https://img.shields.io/nuget/dt/Proxfield.Extensions.Caching.SQLite)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite/) |
| **Proxfield.Extensions.Caching.SQLite.DependencyInjection** | [![Nuget version](https://img.shields.io/nuget/v/Proxfield.Extensions.Caching.SQLite)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite.DependencyInjection/) | [![Nuget downloads](https://img.shields.io/nuget/dt/Proxfield.Extensions.Caching.SQLite.DependencyInjection)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite.DependencyInjection/) |

## Installation

```bash
PM> Install-Package Proxfield.Extensions.Caching.SQLite
```

For applications using `Microsoft.Extensions.DependencyInjection`, use the DI package:

```bash
PM> Install-Package Proxfield.Extensions.Caching.SQLite.DependencyInjection
```

Visit the [Nuget Repository Page](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite) to learn more.

## Usage

Initialization can be done via standard instantiation or through Dependency Injection.

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

### Configuration

#### Database File Location

If `options.Location` is not provided, the database will be stored in the same folder as the running application.

#### Encryption

To enable data encryption, set `UseEncryption` to `true` and provide an `EncryptionKey`:

```csharp
services.AddSQLiteCache(options => {
    options.UseEncryption = true;
    options.EncryptionKey = "d5644e8105ad77c3c3324ba693e83d8fffd54950";
});
```

## Caching Methods

Data can be stored/retrieved as simple strings or complex objects.

### Basic Usage

```csharp
// Store as string
this.cache.SetAsString("users/1", "jose");
var userName = this.cache.GetAsString("users/1");

// Store as object
this.cache.SetAsObject<User>("users/1", new User() { Name = "Jose" });
var user = this.cache.GetAsObject<User>("users/1");
```

### Available Methods

| Method | Description |
| - | - |
| `byte[] Get(string key)` | Retrieves a cached resource from the database. |
| `Task<byte[]> GetAsync(string key)` | Retrieves a cached resource asynchronously. |
| `void Set(string key, byte[] value)` | Sets a cached resource in the database. |
| `Task SetAsync(string key, byte[] value)` | Sets a cached resource asynchronously. |
| `void Remove(string key)` | Removes a cached resource from the database. |
| `Task RemoveAsync(string key)` | Removes a cached resource asynchronously. |
| `void SetAsString(string key, string value)` | Sets a string in the database. |
| `void SetAsObject<T>(string key, T value)` | Sets an object in the database. |
| `string GetAsString(string key)` | Retrieves a string from the database. |
| `T? GetAsObject<T>(string key)` | Retrieves an object from the database. |
| `List<T> GetAsObjectStartsWith<T>(this ISQLiteCache cache, string key)` | Gets a list of objects where the key starts with the specified string. |
| `List<string> GetAsStringStartsWith(this ISQLiteCache cache, string key)` | Gets a list of strings where the key starts with the specified string. |

## Collections and Indexes

You can index cached objects/strings. For example, saving an object with key **vehicles/1**:

```csharp
cache.SetAsObject("vehicles/1", new { Name = "bicycle" });
```

This makes it possible to query multiple objects at once (e.g., every document in a collection):

```csharp
cache.GetAsObjectStartsWith<Vehicle>("vehicles");
```

### Index Management Methods

Accessed via `cache.Maintenance`.

| Method | Description |
| - | - |
| `List<SQLiteCacheIndex> GetAllIndexes()` | Returns all indexes in the database. |
| `SQLiteCacheIndex? GetIndex(string name)` | Returns a specific index. |
| `void ClearAllIndexers()` | Purges all indexes from the database. |
| `void ResetIndex(string name, long? value = null)` | Resets an index to a specific value. |

## Platform Support

SQLite Caching is compiled for:

- [x] .NET 6
- [x] .NET 5
- [x] .NET Core 3.1

## License

![GitHub License](https://img.shields.io/github/license/proxfield/Proxfield.Extensions.Caching.SQLite)

The MIT License ([MIT](LICENSE.md)) - Copyright (c) 2022-2023 Proxfield Consulting Group and its affiliates.
