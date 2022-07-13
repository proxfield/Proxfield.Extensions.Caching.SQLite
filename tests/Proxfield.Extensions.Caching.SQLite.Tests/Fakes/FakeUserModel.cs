namespace Proxfield.Extensions.Caching.SQLite.Tests.Mocks
{
    public class FakeUserModel : Cacheable
    {
        public FakeUserModel(ISQLiteCache cache) : base(cache)
        {
        }
    }
}
