namespace Proxfield.Extensions.Caching.SQLite.Sql.Schema
{
    public class SqlIndexCommands
    {
        /*
       * Indexing table
       */
        private static readonly string TABLE_NAME = "TBL_SQLITE_INDEX";
        public static string CREATE_TABLE_COMMAND => $"CREATE TABLE if not exists {TABLE_NAME} (NAME TEXT PRIMARY KEY, SEQ_INDEX INT NOT NULL)";
        public static string SELECT_COMMAND => $"SELECT NAME, SEQ_INDEX FROM {TABLE_NAME} WHERE NAME = $name";
        public static string SELECT_ALL_INDEXES_COMMAND => $"SELECT NAME, SEQ_INDEX FROM {TABLE_NAME}";
        public static string DELETE_COMMAND => $"DELETE FROM {TABLE_NAME} WHERE NAME = $name";
        public static string DELETE_ALL_INDEXES_COMMAND => $"DELETE FROM {TABLE_NAME}";
        public static string INSERT_COMMAND => $"INSERT INTO {TABLE_NAME} (NAME, SEQ_INDEX) VALUES ($name, $index)";
        public static string UPDATE_COMMAND => $"UPDATE {TABLE_NAME} SET SEQ_INDEX = $index WHERE NAME = $name";
    }
}
