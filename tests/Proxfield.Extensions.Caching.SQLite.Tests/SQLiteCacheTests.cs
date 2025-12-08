using Moq;
using Proxfield.Extensions.Caching.SQLite.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Proxfield.Extensions.Caching.SQLite.Tests
{
    public class SQLiteCacheTests
    {
        private readonly Mock<DbCacheOperations> _mockDbCache;
        private readonly Mock<Maintenance> _mockMaintenance;
        private readonly Mock<SQLiteCacheOptions> _mockOptions;

        private readonly SQLiteCache _cache;
        public SQLiteCacheTests()
        {
            var repository = new MockRepository(MockBehavior.Strict);
            
            _mockOptions = repository.Create<SQLiteCacheOptions>();
            _mockDbCache = repository.Create<DbCacheOperations>(_mockOptions.Object.Location);
            _mockMaintenance = repository.Create<Maintenance>(_mockOptions.Object);

            _mockDbCache.Setup(x => x.Initialize()).Verifiable();
            _mockDbCache.Setup(x => x.CreateIfNotExists()).Verifiable();

            _cache = new SQLiteCache(_mockOptions.Object, _mockDbCache.Object,_mockMaintenance.Object);
        }

        [Fact]
        public void SetAsByteArray_ShouldNotThrownException()
        {
            //Arrange
            const string content = "just a string";
            const string key = "d1";
            var contentBinary = Encoding.ASCII.GetBytes(content);
            _mockDbCache.Setup(x => x.InsertCache(key, contentBinary)).Returns(true).Verifiable();
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

            CancellationToken cancellationToken = new CancellationToken();

            var contentBinary = Encoding.ASCII.GetBytes(content);
            _mockDbCache.Setup(x => x.InsertCacheAsync(key, contentBinary, It.IsAny<CancellationToken>())).ReturnsAsync(true).Verifiable();
            //Act
            var exception = await Record.ExceptionAsync(() => _cache.SetAsync(key, contentBinary, cancellationToken));
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

            _mockDbCache.Setup(x => x.GetCache(key)).Returns(contentBinary).Verifiable();
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
            _mockDbCache.Setup(x => x.GetCache(key)).Returns(contentBinary).Verifiable();
            //Act & Assert
            Assert.Equal(contentBinary, _cache.Get(key));
        }

        [Fact]
        public void GetAsByteArray_DocumentDoesNotExist_ShouldThrownException()
        {
            //Arrange
            const string key = "d1";
            _mockDbCache.Setup(x => x.GetCache(key)).Throws(new System.Exception("Could not found the document by key")).Verifiable();
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
            _mockDbCache.Setup(x => x.Delete(key)).Returns(true).Verifiable();
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
            _mockDbCache.Setup(x => x.Delete(key)).Throws(new System.Exception("Could not found the document by key")).Verifiable();
            //Act
            var exception = Record.Exception(() => _cache.Remove(key));
            //Assert
            Assert.NotNull(exception);
        }
    }
}