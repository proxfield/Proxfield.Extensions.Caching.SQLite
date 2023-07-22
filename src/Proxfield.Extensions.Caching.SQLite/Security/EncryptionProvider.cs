using System;

namespace Proxfield.Extensions.Caching.SQLite.Security
{
    /// <summary>
    /// Encryption Provider to standarize the encryption classes
    /// </summary>
    public abstract class EncryptionProvider
    {
        /// <summary>
        /// Encryption key
        /// </summary>
        public abstract string? Key { get; set; }
        /// <summary>
        /// Instanciates a new provider with key
        /// </summary>
        /// <param name="key"></param>
        public EncryptionProvider(string? key)
        {
            Key = key;
        }
        /// <summary>
        /// Encrypts a text with the mapped key
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public abstract string Encrypt(string text);
        /// <summary>
        /// Decrypts a text with the mapped key
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public abstract string Decrypt(string cipherText);
        /// <summary>
        /// Generates a custom encryption key
        /// </summary>
        /// <returns></returns>
        public static string GenerateEncryptKey() => $"{Environment.UserName}{Guid.NewGuid()}";
    }
}
