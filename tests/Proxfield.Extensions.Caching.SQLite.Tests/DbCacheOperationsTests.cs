using Microsoft.Data.Sqlite;
using Proxfield.Extensions.Caching.SQLite.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proxfield.Extensions.Caching.SQLite.Tests
{
    public class DbCacheOperationsTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbCacheOperations _operations;

        public DbCacheOperationsTests()
        {
            // Use shared in-memory database
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            
            // Pass the open connection to operations
            _operations = new DbCacheOperations(":memory:", _connection);
        }

        [Fact]
        public void Initialize_ShouldCreateTable()
        {
            // Act - occurs in constructor
            
            // Assert
            // Try to query the table to ensure it exists
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='TBL_SQLITE_CACHE';";
            var result = command.ExecuteScalar();
            Assert.Equal("TBL_SQLITE_CACHE", result);
        }

        [Fact]
        public void InsertCache_ShouldInsertItem()
        {
            // Arrange
            var key = "test_key";
            var value = Encoding.UTF8.GetBytes("test_value");

            // Act
            var result = _operations.InsertCache(key, value);

            // Assert
            Assert.True(result);
            var retrieved = _operations.GetCache(key);
            Assert.Equal(value, retrieved);
        }

        [Fact]
        public async Task InsertCacheAsync_ShouldInsertItem()
        {
            // Arrange
            var key = "async_test_key";
            var value = Encoding.UTF8.GetBytes("async_test_value");

            // Act
            var result = await _operations.InsertCacheAsync(key, value);

            // Assert
            Assert.True(result);
            var retrieved = await _operations.GetCacheAsync(key);
            Assert.Equal(value, retrieved);
        }

        [Fact]
        public void Delete_ShouldRemoveItem()
        {
            // Arrange
            var key = "delete_key";
            var value = Encoding.UTF8.GetBytes("value");
            _operations.InsertCache(key, value);

            // Act
            var deleteResult = _operations.Delete(key);

            // Assert
            Assert.True(deleteResult);
            var retrieved = _operations.GetCache(key);
            Assert.Empty(retrieved);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveItem()
        {
            // Arrange
            var key = "async_delete_key";
            var value = Encoding.UTF8.GetBytes("value");
            await _operations.InsertCacheAsync(key, value);

            // Act
            var deleteResult = await _operations.DeleteAsync(key);

            // Assert
            Assert.True(deleteResult);
            var retrieved = await _operations.GetCacheAsync(key);
            Assert.Empty(retrieved);
        }

        [Fact]
        public void GetStartsWith_ShouldReturnMatchingItems()
        {
            // Arrange
            _operations.InsertCache("prefix/1", Encoding.UTF8.GetBytes("1"));
            _operations.InsertCache("prefix/2", Encoding.UTF8.GetBytes("2"));
            _operations.InsertCache("other/1", Encoding.UTF8.GetBytes("3"));

            // Act
            var results = _operations.GetStartsWithCache("prefix");

            // Assert
            Assert.Equal(2, results.Count);
        }

        [Fact]
        public async Task GetStartsWithAsync_ShouldReturnMatchingItems()
        {
            // Arrange
            await _operations.InsertCacheAsync("async_prefix/1", Encoding.UTF8.GetBytes("1"));
            await _operations.InsertCacheAsync("async_prefix/2", Encoding.UTF8.GetBytes("2"));
            await _operations.InsertCacheAsync("async_other/1", Encoding.UTF8.GetBytes("3"));

            // Act
            var results = await _operations.GetStartsWithCacheAsync("async_prefix");

            // Assert
            Assert.Equal(2, results.Count);
        }

         [Fact]
        public void Truncate_ShouldClearAllItems()
        {
            // Arrange
            _operations.InsertCache("k1", Encoding.UTF8.GetBytes("v1"));
            _operations.InsertCache("k2", Encoding.UTF8.GetBytes("v2"));

            // Act
            _operations.Truncate();

            // Assert
            Assert.Empty(_operations.GetCache("k1"));
            Assert.Empty(_operations.GetCache("k2"));
        }

        public void Dispose()
        {
            _operations.Dispose(); // This disposes the connection too
        }
    }
}
