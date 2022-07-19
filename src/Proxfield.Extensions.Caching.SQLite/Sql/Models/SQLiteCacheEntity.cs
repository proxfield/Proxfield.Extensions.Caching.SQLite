namespace Proxfield.Extensions.Caching.SQLite.Sql.Models
{
    public class SQLiteCacheEntity : BaseModel
    {
        public string Key { get; set; }
        public byte[] Value { get; set; }

        internal override string _table => "TBL_SQLITE_CACHE";
    }
}
