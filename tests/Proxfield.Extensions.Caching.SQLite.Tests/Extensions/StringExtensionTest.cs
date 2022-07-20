using Proxfield.Extensions.Caching.SQLite.Extensions;
using Xunit;

namespace Proxfield.Extensions.Caching.SQLite.Tests.Extensions
{
    public class StringExtensionTest
    {
        [Fact]
        public void RemoveSpecialChars_Pipe_ShouldBeWithouPipe()
        {
            //Arrange
            const string text = "$name";
            const string expected = "name";
            //Act
            var result = StringExtension.RemoveSpecialChars(text);
            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void RemoveSpecialChars_DollarSign_ShouldBeWithouDollarSign()
        {
            //Arrange
            const string text = "users|";
            const string expected = "users";
            //Act
            var result = StringExtension.RemoveSpecialChars(text);
            //Assert
            Assert.Equal(expected, result);
        }
    }
}
