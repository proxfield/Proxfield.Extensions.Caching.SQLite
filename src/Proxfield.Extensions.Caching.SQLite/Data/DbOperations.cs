using Microsoft.Data.Sqlite;
using Proxfield.Extensions.Caching.SQLite.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proxfield.Extensions.Caching.SQLite.Data
{
    /// <summary>
    /// Database Operations
    /// </summary>
    public class DbOperations : IDisposable
    {
        private readonly SqliteConnection _sqliteConnection;
        private readonly string _location;
        public DbOperations(string location, SqliteConnection? connection = null)
        {
            _location = location;
            _sqliteConnection = connection ?? new SqliteConnection($"Data Source={_location};");
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            this.Initialize();
        }

        /// <summary>
        /// Initializes the database 
        /// </summary>
        public virtual void Initialize()
        {
            if (_sqliteConnection.State != System.Data.ConnectionState.Open)
            {
                _sqliteConnection.Open();
            }
        }
        /// <summary>
        /// Returns the operations connection
        /// </summary>
        /// <returns></returns>
        public SqliteConnection GetConnection() => this._sqliteConnection;
        /// <summary>
        /// Returns the operations location
        /// </summary>
        /// <returns></returns>
        public string GetLocation() => this._location;
        /// <summary>
        /// Runs a command based on SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int RunNonQueryCommand(string sql) => 
            RunNonQueryCommand(new List<KeyValuePair<string, object>>(), sql);

        /// <summary>
        ///  Runs a command based on SQL and KeyParValue
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int RunNonQueryCommand(List<KeyValuePair<string, object>> where, string sql)
        {
            var command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;
            where.ForEach(p => command.Parameters.AddWithValue($"${p.Key}", p.Value));
            return command.ExecuteNonQuery();
        }
        /// <summary>
        /// Runs a query by using replace instead of parameter value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected List<T> RunQueryLikeCommand<T>(List<KeyValuePair<string, object>> where, string sql)
        {
            where.ForEach(p => sql = sql.Replace($"${p.Key}", p.Value?.ToString().RemoveSpecialChars()));
            return RunQueryCommand<T>(new List<KeyValuePair<string, object>>(), sql);
        }
        /// <summary>
        /// Runs a command based on SQL and KeyParValue and return a list of objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected List<T> RunQueryCommand<T>(List<KeyValuePair<string, object>> where, string sql)
        {
            var command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;
            where.ForEach(p => command.Parameters.AddWithValue($"${p.Key}", p.Value));

            var items = new List<T>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   var instance = Activator.CreateInstance<T>();
                    instance?
                        .GetType()
                        .GetProperties()
                        .ToList()
                        .ForEach(p =>
                        {
                            p.SetValue(instance, Convert.ChangeType(reader[p.Name], p.PropertyType), null);
                        });
                    if(instance != null) items.Add(instance);
                }
            }

            return items;
        }

        /// <summary>
        /// Disposes the connection
        /// </summary>
        public void Dispose()
        {
            _sqliteConnection.Dispose();
        }
    }
}
