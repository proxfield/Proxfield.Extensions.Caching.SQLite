using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Proxfield.Extensions.Caching.SQLite.DependencyInjection.Tests
{
    public class SQLiteCacheExtensionTests
    {
        [Fact]
        public void SQLiteCacheExtension_WhenAddSQLiteCache_ShouldReturnServices()
        {
            var services = new ServiceCollection();
            services.AddSQLiteCache();

            Assert.Contains(services, x => x.ServiceType == typeof(ISQLiteCache));
        }
    }
}
