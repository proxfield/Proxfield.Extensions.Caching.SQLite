namespace Proxfield.Extensions.Caching.SQLite.Sql.Schema
{
    public class SqlIndexCommands
    {
        /*
       * Indexing table
       */
        private static readonly string TABLE_NAME = "TBL_SQLITE_INDEX";
        public static string CREATE_TABLE_COMMAND => $"CREATE TABLE if not exists {TABLE_NAME} (NAME TEXT PRIMARY KEY, SEQUENCE INT NOT NULL)";
        public static string SELECT_COMMAND => $"SELECT NAME, SEQUENCE FROM {TABLE_NAME} WHERE NAME = $name";
        public static string SELECT_ALL_INDEXES_COMMAND => $"SELECT NAME, SEQUENCE FROM {TABLE_NAME}";
        public static string DELETE_COMMAND => $"DELETE FROM {TABLE_NAME} WHERE NAME = $name";
        public static string DELETE_ALL_INDEXES_COMMAND => $"DELETE FROM {TABLE_NAME}";
        public static string INSERT_COMMAND => $"INSERT INTO {TABLE_NAME} (NAME, SEQUENCE) VALUES ($name, $index)";
        public static string UPDATE_COMMAND => $"UPDATE {TABLE_NAME} SET SEQUENCE = $index WHERE NAME = $name";
    }
}
