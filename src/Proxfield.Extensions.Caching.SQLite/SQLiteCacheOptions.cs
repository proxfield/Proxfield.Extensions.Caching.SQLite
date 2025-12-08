using Proxfield.Extensions.Caching.SQLite.Security;

namespace Proxfield.Extensions.Caching.SQLite
{
    /// <summary>
    /// SQLite Cache Options
    /// </summary>
    public class SQLiteCacheOptions
    {
        /// <summary>
        /// Database file location
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// Sets to use encryption
        /// </summary>
        public bool UseEncryption { get; set; } = false;
        /// <summary>
        /// Sets the encryption key
        /// </summary>
        public string? EncryptionKey { get; set; }
    }
}
