namespace Proxfield.Extensions.Caching.SQLite.Constants
{
    public static class SQLCommands
    {
        private static readonly string TABLE_NAME = "TBL_SQLITE_CACHE";
        public static string CREATE_DATABASE_COMMAND => "CREATE DATABASE SQLITE_CACHE;";
        public static string CREATE_TABLE_COMMAND => $"CREATE TABLE if not exists {TABLE_NAME} (KEY_ID TEXT PRIMARY KEY, VALUE BLOB NOT NULL, EXPIRES_AT DATETIME)";
        public static string SELECT_COMMAND => $"SELECT KEY_ID, VALUE FROM {TABLE_NAME} WHERE KEY_ID = $id";
        public static string DELETE_COMMAND => $"DELETE FROM {TABLE_NAME} WHERE KEY_ID = $id";
        public static string INSERT_COMMAND => $"INSERT INTO {TABLE_NAME} (KEY_ID, VALUE) VALUES ($id, $value)";
        public static string UPDATE_COMMAND => $"UPDATE {TABLE_NAME} SET VALUE = $value WHERE KEY_ID = $id";
    }
}
