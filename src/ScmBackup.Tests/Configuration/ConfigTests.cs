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
        [InlineData(typeof(bool), "foo", "test_bool", true)]
        [InlineData(typeof(string), "foo", "test_string", "test")]
        [InlineData(typeof(int), "foo", "test_int", 42)]
        [InlineData(typeof(string), "foo", "bar", null)]                // bar doesn't exist
        [InlineData(typeof(bool), "bar", "test_bool", null)]            // bar doesn't exist
        public void GetOptions_Check(Type type, string key1, string key2, object expectedResult)
        {
            var result = this.sut.GetOption(type, key1, key2);

            Assert.Equal(expectedResult, result);
            if (result != null)
            {
                Assert.IsType(type, result);
            }
        }
    }
}
