using Moq;
using Proxfield.Extensions.Caching.SQLite.Data;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proxfield.Extensions.Caching.SQLite.Tests
{
    public class SQLiteCacheTests
    {
        private readonly Mock<DbCacheOperations> _mockSqliteHelper;
        private readonly SQLiteCache _cache;
        public SQLiteCacheTests()
        {
            var repository = new MockRepository(MockBehavior.Strict);
            
            _mockSqliteHelper = repository.Create<DbCacheOperations>(string.Empty);
            _mockSqliteHelper.Setup(x => x.Initialize()).Verifiable();
            _mockSqliteHelper.Setup(x => x.CreateIfNotExists()).Verifiable();

            _cache = new SQLiteCache(_mockSqliteHelper.Object);
        }

        [Fact]
        public void SetAsByteArray_ShouldNotThrownException()
        {
            //Arrange
            const string content = "just a string";
            const string key = "d1";
            var contentBinary = Encoding.ASCII.GetBytes(content);
            _mockSqliteHelper.Setup(x => x.InsertCache(key, contentBinary)).Returns(true).Verifiable();
            //Act
            var exception = Record.Exception(() => _cache.Set(key, contentBinary));
            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task SetAsByteArrayAsync_ShouldNotThrownException()
        {
            //Arrange
            const string content = "just a string";
            const string key = "d1";
            var contentBinary = Encoding.ASCII.GetBytes(content);
            _mockSqliteHelper.Setup(x => x.InsertCache(key, contentBinary)).Returns(true).Verifiable();
            //Act
            var exception = await Record.ExceptionAsync(() => _cache.SetAsync(key, contentBinary));
            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetAsByteArray_DocumentExists_ShouldNotThrownException()
        {
            //Arrange
            const string content = "just a string";
            const string key = "d1";
            var contentBinary = Encoding.ASCII.GetBytes(content);

            _mockSqliteHelper.Setup(x => x.GetCache(key)).Returns(contentBinary).Verifiable();
            //Act
            var exception = Record.Exception(() => _cache.Get(key));
            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetAsByteArray_DocumentDoesNotExist_ShouldReturnByteArray()
        {
            //Arrange
            const string content = "just a string";
            const string key = "d1";
            var contentBinary = Encoding.ASCII.GetBytes(content);
            _mockSqliteHelper.Setup(x => x.GetCache(key)).Returns(contentBinary).Verifiable();
            //Act & Assert
            Assert.Equal(contentBinary, _cache.Get(key));
        }

        [Fact]
        public void GetAsByteArray_DocumentDoesNotExist_ShouldThrownException()
        {
            //Arrange
            const string key = "d1";
            _mockSqliteHelper.Setup(x => x.GetCache(key)).Throws(new System.Exception("Could not found the document by key")).Verifiable();
            //Act
            var exception = Record.Exception(() => _cache.Get(key));
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void Delete_DocumentExists_ShouldNotThrownException()
        {
            //Arrange
            const string key = "d1";
            _mockSqliteHelper.Setup(x => x.Delete(key)).Returns(true).Verifiable();
            //Act
            var exception = Record.Exception(() => _cache.Remove(key));
            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Delete_DocumentDoesNotExist_ShouldThrownException()
        {
            //Arrange
            const string key = "d1";
            _mockSqliteHelper.Setup(x => x.Delete(key)).Throws(new System.Exception("Could not found the document by key")).Verifiable();
            //Act
            var exception = Record.Exception(() => _cache.Remove(key));
            //Assert
            Assert.NotNull(exception);
        }
    }
}