using Moq;
using Proxfield.Extensions.Caching.SQLite.Data;
using Proxfield.Extensions.Caching.SQLite.Tests.Mocks;
using Xunit;

namespace Proxfield.Extensions.Caching.SQLite.Tests
{
    public class CacheableTest
    {
        private readonly Mock<SQLiteCache> _mockSQLiteCache;
        private readonly FakeUserModel _model;
        public CacheableTest()
        {
            var repository = new MockRepository(MockBehavior.Strict);

            var mockSqliteHelper = repository.Create<SQLiteHelper>(string.Empty);

            mockSqliteHelper.Setup(x => x.Initialize()).Verifiable();
            mockSqliteHelper.Setup(x => x.CreateIfNotExists()).Verifiable();

            _mockSQLiteCache = repository.Create<SQLiteCache>(mockSqliteHelper.Object);
            _model = new FakeUserModel(_mockSQLiteCache.Object);
        }

        [Fact]
        public void Save_WhenModelOk_ShouldNotThrowException()
        {
            const string key = "users/1";

            _mockSQLiteCache.Setup(x => x.SetAsObject(It.IsAny<string>(), It.IsAny<object>()));

            _model.Save(key);
        }
    }
}
