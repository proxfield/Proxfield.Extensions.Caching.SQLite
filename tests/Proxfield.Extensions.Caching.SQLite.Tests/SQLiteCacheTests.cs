using Moq;
using Proxfield.Extensions.Caching.SQLite.Data;
using System.Text;

namespace Proxfield.Extensions.Caching.SQLite.Tests
{
    public class SQLiteCacheTests
    {
        private readonly Mock<SQLiteHelper> _mockSqliteHelper;
        private readonly SQLiteCache _cache;
        public SQLiteCacheTests()
        {
            var repository = new MockRepository(MockBehavior.Strict);
            _mockSqliteHelper = repository.Create<SQLiteHelper>(MockBehavior.Loose);

            _cache = new SQLiteCache(options => 
            {
                options.Location = @"c:\helper\data";
            });
        }

        [Fact]
        public void SetAsByteArray_ShouldNotThrownException()
        {
            //Arrange
            const string content = "just a string";
            const string key = "d1";
            var contentBinary = Encoding.ASCII.GetBytes(content);

            _mockSqliteHelper.Setup(x => x.CreateIfNotExists()).Verifiable();
            _mockSqliteHelper.Setup(x => x.Initialize()).Verifiable();
            _mockSqliteHelper.Setup(x => x.Insert(key, contentBinary)).Returns(true).Verifiable();

            //Act
            var exception = Record.Exception(() => _cache.Set(key, contentBinary));
            //Assert
            Assert.Null(exception);
        }
    }
}