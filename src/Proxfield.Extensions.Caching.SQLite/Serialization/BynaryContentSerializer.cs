using Proxfield.Extensions.Caching.SQLite.Security;
using System;
using System.Text;

namespace Proxfield.Extensions.Caching.SQLite.Serialization
{
    public class BynaryContentSerializer
    {
        public static string BytesToString(byte[]? bytes, EncryptionProvider? provider = null)
        {
            if (bytes == null) 
                return string.Empty;

            return provider?.Decrypt(Encoding.UTF8.GetString(bytes)) ?? Encoding.UTF8.GetString(bytes);
        }

        public static byte[] StringToBytes(string text, EncryptionProvider? provider = null) 
            => Encoding.ASCII.GetBytes(provider?.Encrypt(text) ?? text);

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

        public static byte[] BytesFromObject<T>(T obj, EncryptionProvider? provider = null)
        {
            var result = provider?.Encrypt(JsonContentSerializer.Serialize(obj) ?? JsonContentSerializer.Serialize(obj));
            return Encoding.ASCII.GetBytes(result!);
        }
    }
}
