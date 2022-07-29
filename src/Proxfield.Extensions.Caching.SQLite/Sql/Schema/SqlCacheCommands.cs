namespace Proxfield.Extensions.Caching.SQLite.Sql.Schema
{
    public class SqlCacheCommands
    {
        /*
         * Caching table
         */
        private static readonly string TABLE_CACHE_NAME = "TBL_SQLITE_CACHE";
        public static string CREATE_CACHE_TABLE_COMMAND => $"CREATE TABLE if not exists {TABLE_CACHE_NAME} (KEY TEXT PRIMARY KEY, VALUE BLOB NOT NULL, EXPIRES_AT DATETIME)";
        public static string SELECT_CACHE_COMMAND => $"SELECT KEY, VALUE FROM {TABLE_CACHE_NAME} WHERE KEY = $id";
        public static string SELECT_STARTS_WITH_CACHE_COMMAND => $"SELECT KEY, VALUE FROM {TABLE_CACHE_NAME} WHERE KEY LIKE '$id%' LIMIT $pageSize OFFSET $start";
        public static string DELETE_CACHE_COMMAND => $"DELETE FROM {TABLE_CACHE_NAME} WHERE KEY = $id";
        public static string INSERT_CACHE_COMMAND => $"INSERT INTO {TABLE_CACHE_NAME} (KEY, VALUE) VALUES ($id, $value)";
        public static string UPDATE_CACHE_COMMAND => $"UPDATE {TABLE_CACHE_NAME} SET VALUE = $value WHERE KEY = $id";
        public static string TRUNCATE_COMMAND => $"DELETE FROM {TABLE_CACHE_NAME}";
    }
}
