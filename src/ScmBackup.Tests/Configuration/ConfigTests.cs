using ScmBackup.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScmBackup.Tests.Configuration
{
    public class ConfigTests
    {
        private Config sut;

        public ConfigTests()
        {
            this.sut = new Config();

            // options
            sut.Options = new Dictionary<string, Dictionary<string, object>>();

            var foo = new Dictionary<string, object>();
            foo.Add("test_bool", true);
            foo.Add("test_string", "test");
            foo.Add("test_int", 42);
            sut.Options.Add("foo", foo);
        }

        [Theory]
        [InlineData("foo", "test_string", "test")]
        [InlineData("foo", "test_int", "42")]
        [InlineData("foo", "bar", null)]                // bar doesn't exist
        public void GetOptions_StringTests(string key1, string key2, string expectedResult)
        {
            var result = this.sut.GetOption<string>(key1, key2);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("foo", "test_int", 42)]
        [InlineData("foo", "test_bool", 1)]
        [InlineData("foo", "test_string", 0)]           // wrong type
        [InlineData("foo", "bar", 0)]                   // bar doesn't exist
        public void GetOptions_IntTests(string key1, string key2, int expectedResult)
        {
            var result = this.sut.GetOption<int>(key1, key2);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("foo", "test_bool", true)]
        [InlineData("foo", "test_int", true)]
        [InlineData("foo", "test_string", false)]       // wrong type
        [InlineData("foo", "bar", false)]               // bar doesn't exist
        public void GetOptions_BoolTests(string key1, string key2, bool expectedResult)
        {
            var result = this.sut.GetOption<bool>(key1, key2);

            Assert.Equal(expectedResult, result);
        }
    }
}
