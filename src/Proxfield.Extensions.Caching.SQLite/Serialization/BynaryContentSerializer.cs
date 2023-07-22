using Proxfield.Extensions.Caching.SQLite.Security;
using System;
using System.Text;

namespace Proxfield.Extensions.Caching.SQLite.Serialization
{
    /// <summary>
    /// Serializes and deserializes text to and from byte array
    /// </summary>
    public class BynaryContentSerializer
    {
        /// <summary>
        /// Converts a byte array to string
        /// </summary>
        /// <param name="bytes">byte array</param>
        /// <param name="provider">encryption provider</param>
        /// <returns></returns>
        public static string BytesToString(byte[]? bytes, EncryptionProvider? provider = null)
        {
            if (bytes == null) 
                return string.Empty;

            return provider?.Decrypt(Encoding.UTF8.GetString(bytes)) ?? Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Converts a string to byte array
        /// </summary>
        /// <param name="text">text as string</param>
        /// <param name="provider">encryption provider</param>
        /// <returns></returns>
        public static byte[] StringToBytes(string text, EncryptionProvider? provider = null) 
            => Encoding.ASCII.GetBytes(provider?.Encrypt(text) ?? text);

        /// <summary>
        /// converts a byte array to an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static T ObjectFromBytes<T>(byte[]? bytes, EncryptionProvider? provider = null)
        {
            if(bytes == null)
                return default!;

            var content = Encoding.ASCII.GetString(bytes);
            
            if(string.IsNullOrEmpty(content))
                return default!;

            if(provider != null)
                content = provider.Decrypt(content);

            return JsonContentSerializer.Deserialize<T>(content)!;
        }

        /// <summary>
        /// converts object to byte array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static byte[] BytesFromObject<T>(T obj, EncryptionProvider? provider = null)
        {
            var result = provider?.Encrypt(JsonContentSerializer.Serialize(obj)) ?? JsonContentSerializer.Serialize(obj);
            return Encoding.ASCII.GetBytes(result!);
        }
    }
}
