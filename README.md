
![cache](https://user-images.githubusercontent.com/7343019/186032840-de9be07f-8b41-448e-825b-f0f60e14e0f2.png)


The SQLite Caching Library is layer for caching data on SQLite to be used as a secondary database in case of failures and network inconsistences.

![GitHub License](https://img.shields.io/github/license/proxfield/Proxfield.Extensions.Caching.SQLite)
![Actions](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/build.yml/badge.svg)
[![Nuget](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/release.yml/badge.svg)](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/release.yml)
![GitHub branch checks state](https://img.shields.io/github/checks-status/proxfield/Proxfield.Extensions.Caching.SQLite/main)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/proxfield/Proxfield.Extensions.Caching.SQLite)

## Packages

Packages and versions available at the Nuget Galery.


| Package | Version | Downloads |
| - | - | - |
| <b>Proxfield.Extensions.Caching.SQLite</b> | [![Nuget version](https://img.shields.io/nuget/v/Proxfield.Extensions.Caching.SQLite)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite/) | [![Nuget downloads](https://img.shields.io/nuget/dt/Proxfield.Extensions.Caching.SQLite)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite/) |
| <b>Proxfield.Extensions.Caching.SQLite.DependencyInjection</b> | [![Nuget version](https://img.shields.io/nuget/v/Proxfield.Extensions.Caching.SQLite)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite.DependencyInjection/) | [![Nuget downloads](https://img.shields.io/nuget/dt/Proxfield.Extensions.Caching.SQLite.DependencyInjection)](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite.DependencyInjection/) |

## Nuget

```bash
PM> Install-Package Proxfield.Extensions.Caching.SQLite
```

For application who uses Microsoft.Extensions.DependencyInjection there is a package available for using the library with DI:

```bash
PM> Install-Package Proxfield.Extensions.Caching.SQLite.DependencyInjection
```

Visit out project at the [Nuget Repository Page](https://www.nuget.org/packages/Proxfield.Extensions.Caching.SQLite) to know more.

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

## Cache Methods available

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

The following list constains all caching methods avaliable currently on the library.


| Method | Description |
| - | - |
| byte[] Get(string key); | Retrieves a cached resource from the database |
| Task<byte[]> GetAsync(string key); | Retrieves a cached resource from the database as async |
| void Set(string key, byte[] value); | Sets a cached resource to the database |
| Task SetAsync(string key, byte[] value); | Sets a cached resource to the database async |
| void Remove(string key); | Removes a cached resource to the database |
| Task RemoveAsync(string key); | Removes a cached resource to the database as async |
| void SetAsString(string key, string value); | Sets an string into the the database |
| void SetAsObject<T>(string key, T value); | Sets an object into the the database |
| string GetAsString(string key); | Retrieves a string from the database |
| T? GetAsObject<T>(string key); | Retrieves an object from the database |
| List\<T\> GetAsObjectStartsWith<T>(this ISQLiteCache cache, string key) | Get a list of objects when the key starts with something |
| List\<string\> GetAsStringStartsWith(this ISQLiteCache cache, string key) | Get a list of strings when the key starts with something |

## Collections and Indexes

It is now possible to cache objects/strings by using an index, for example, the following code on a newly created database would save the object with the key as being <strong>vehicles/1</strong>.

```csharp
 cache.SetAsObject("vehicles|", new { Name = "bycicle" }) ;
```

Making possible to query more than one object at once, every document on a collection.

```csharp
cache.GetAsObjectStartsWith<Vehicle>("vehicles");
```

The following list constains all indexing methods avaliable currently on the library. They can be acessed by the Maintenance property of cache (<strong>cache.Maintenance.</strong>)


| Method | Description |
| - | - |
| List\<SQLiteCacheIndex\> GetAllIndexes() | Returns all indexes on the database |
| SQLiteCacheIndex? GetIndex(string name | Returns an index from the database |
| void ClearAllIndexers() | Purge all indexes from the database |
| void ResetIndex(string name, long? value = null) | Reset an index to an specific value |

## Platform Support

SQLite Caching is compiled for DotNet 6, soon there will versions available for other plataforms.

- [X] DotNet 6
- [ ] DotNet 5

## License
![GitHub License](https://img.shields.io/github/license/proxfield/Proxfield.Extensions.Caching.SQLite)

The MIT License ([MIT](LICENSE.md)) - Copyright (c) 2022 Proxfield
