using Proxfield.Extensions.Caching.SQLite.Serialization;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Proxfield.Extensions.Caching.SQLite.Tests.Serialization
{
    public class BynaryContentSerializerTest
    {
        [Fact]
        public void StringToBytes_ShouldBeTheSame()
        {
            //Arrange
            const string text = "be good";
            var expected = Encoding.ASCII.GetBytes(text);
            //Act
            var result = BynaryContentSerializer.StringToBytes(text);
            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BytesToString_ShouldBeTheSame()
        {
            //Arrange
            const string expected = "be good";
            var bytes = Encoding.ASCII.GetBytes(expected);
            //Act
            var result = BynaryContentSerializer.BytesToString(bytes);
            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ObjectFromBytes_ShouldBeTheSame()
        {
            //Arrange
            var bytes = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(new Example() { Value = "test" }));
            var expected = JsonContentSerializer.Deserialize<Example>(Encoding.ASCII.GetString(bytes));
            //Act
            var result = BynaryContentSerializer.ObjectFromBytes<Example>(bytes);
            //Assert
            Assert.Equal(expected?.Value, result?.Value);
        }

        [Fact]
        public void ObjectFromBytes_WhenContentIsEmpty_ShouldReturnDefaultValue()
        {
            //Arrange
            var bytes = Encoding.ASCII.GetBytes("");
            //Act
            var result = BynaryContentSerializer.ObjectFromBytes<Example>(bytes);
            //Assert
            Assert.Equal(default(Example)?.Value, result?.Value);
        }

        [Fact]
        public void BytesFromObject_ShouldBeTheSame()
        {
            //Arrange
            var bytes = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(new Example() { Value = "test" }));
            var expected = Encoding.ASCII.GetBytes(JsonContentSerializer.Serialize(bytes));
            //Act
            var result = BynaryContentSerializer.BytesFromObject(bytes);
            //Assert
            Assert.Equal(expected, result);
        }
    }

    class Example
    {
        public string Value { get; set; }
    }
}
