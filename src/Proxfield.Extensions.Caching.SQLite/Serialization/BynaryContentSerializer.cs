using System.Text;

namespace Proxfield.Extensions.Caching.SQLite.Serialization
{
    public class BynaryContentSerializer
    {
        public static string BytesToString(byte[] bytes)
            => Encoding.UTF8.GetString(bytes);

        public static byte[] StringToBytes(string text) 
            => Encoding.ASCII.GetBytes(text);

        public static T? ObjectFromBytes<T>(byte[] bytes)
            => JsonContentSerializer.Deserialize<T>(Encoding.ASCII.GetString(bytes));

        public static byte[] BytesFromObject<T>(T obj)
            => Encoding.ASCII.GetBytes(JsonContentSerializer.Serialize(obj));
    }
}
