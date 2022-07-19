namespace Proxfield.Extensions.Caching.SQLite.Sql.Models
{
    public class SQLiteCacheIndex : BaseModel
    {
        public string Name { get; set; }
        public long Sequence { get; set; }

        internal override string _table => "TBL_SQLITE_INDEX";
    }
}
