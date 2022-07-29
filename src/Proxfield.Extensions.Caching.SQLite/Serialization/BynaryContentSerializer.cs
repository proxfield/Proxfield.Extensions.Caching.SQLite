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
        {
            var content = Encoding.ASCII.GetString(bytes);
            if(string.IsNullOrEmpty(content))
                return default;

            return JsonContentSerializer.Deserialize<T>(content);
        }

        public static byte[] BytesFromObject<T>(T obj) =>
            Encoding.ASCII.GetBytes(JsonContentSerializer.Serialize(obj));
    }
}
