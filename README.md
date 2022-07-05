# Proxfield.Extensions.Caching.SQLite

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
